using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GOTG.Models;
using GOTG.Data;
using GOTG.ViewModels;

namespace GOTG.Controllers
{
    [Authorize]
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IncidentController> _logger;

        public IncidentController(ApplicationDbContext context, ILogger<IncidentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(IncidentReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

                var incident = new IncidentReport
                {
                    UserId = userId,
                    Title = model.Title,
                    Description = model.Description,
                    Location = model.Location,
                    DisasterType = model.DisasterType,
                    IncidentDate = model.IncidentDate,
                    SeverityLevel = model.SeverityLevel,
                    PeopleAffected = model.PeopleAffected,
                    Status = IncidentStatus.Pending
                };

                _context.IncidentReports.Add(incident);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New incident reported: {IncidentId}", incident.IncidentId);

                return RedirectToAction("Details", new { id = incident.IncidentId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var incidents = await _context.IncidentReports
                .Include(i => i.User)
                .OrderByDescending(i => i.ReportedAt)
                .ToListAsync();

            return View(incidents);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var incident = await _context.IncidentReports
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.IncidentId == id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, IncidentStatus status)
        {
            var incident = await _context.IncidentReports.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            incident.Status = status;
            incident.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Incident {IncidentId} status updated to {Status}", id, status);

            return RedirectToAction("Details", new { id });
        }
    }
}