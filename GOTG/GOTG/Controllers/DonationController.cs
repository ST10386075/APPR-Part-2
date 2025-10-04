using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GOTG.Models;
using GOTG.Data;
using GOTG.ViewModels;

namespace GOTG.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DonationController> _logger;

        public DonationController(ApplicationDbContext context, ILogger<DonationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new DonationViewModel
            {
                AvailableIncidents = await _context.IncidentReports
                    .Where(i => i.Status == IncidentStatus.Verified || i.Status == IncidentStatus.InProgress)
                    .Select(i => new { i.IncidentId, i.Title })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

                var donation = new Donation
                {
                    UserId = userId,
                    DonationType = model.DonationType,
                    Description = model.Description,
                    Amount = model.Amount,
                    Quantity = model.Quantity,
                    ItemCategory = model.ItemCategory,
                    DeliveryAddress = model.DeliveryAddress,
                    IncidentId = model.IncidentId,
                    Status = DonationStatus.Pending
                };

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New donation created: {DonationId}", donation.DonationId);

                return RedirectToAction("Confirmation", new { id = donation.DonationId });
            }

            model.AvailableIncidents = await _context.IncidentReports
                .Where(i => i.Status == IncidentStatus.Verified || i.Status == IncidentStatus.InProgress)
                .Select(i => new { i.IncidentId, i.Title })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.Incident)
                .FirstOrDefaultAsync(d => d.DonationId == id);

            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        [HttpGet]
        public async Task<IActionResult> MyDonations()
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            var donations = await _context.Donations
                .Include(d => d.Incident)
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();

            return View(donations);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateDonationStatus(int id, DonationStatus status)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            donation.Status = status;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Donation {DonationId} status updated to {Status}", id, status);

            return RedirectToAction("DonationManagement");
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        public async Task<IActionResult> DonationManagement()
        {
            var donations = await _context.Donations
                .Include(d => d.User)
                .Include(d => d.Incident)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();

            return View(donations);
        }
    }
}