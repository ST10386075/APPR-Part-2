using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GOTG.Models;
using GOTG.Data;
using GOTG.ViewModels;

namespace GOTG.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VolunteerController> _logger;

        public VolunteerController(ApplicationDbContext context, ILogger<VolunteerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VolunteerRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

                var volunteer = new Volunteer
                {
                    UserId = userId,
                    Skills = model.Skills,
                    Availability = model.Availability,
                    Experience = model.Experience,
                    Certifications = model.Certifications,
                    Status = VolunteerStatus.Active
                };

                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New volunteer registered: {VolunteerId}", volunteer.VolunteerId);

                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;

            var volunteer = await _context.Volunteers
                .Include(v => v.Tasks)
                .FirstOrDefaultAsync(v => v.UserId == userId);

            var availableTasks = await _context.VolunteerTasks
                .Where(t => t.Status == TaskStatus.Open && t.CurrentVolunteers < t.RequiredVolunteers)
                .Include(t => t.Incident)
                .ToListAsync();

            var viewModel = new VolunteerDashboardViewModel
            {
                Volunteer = volunteer,
                AvailableTasks = availableTasks
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignToTask(int taskId)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.UserId == userId);
            var task = await _context.VolunteerTasks.FindAsync(taskId);

            if (volunteer == null || task == null)
            {
                return NotFound();
            }

            // Add volunteer to task
            if (volunteer.Tasks == null)
            {
                volunteer.Tasks = new List<VolunteerTask>();
            }

            volunteer.Tasks.Add(task);
            task.CurrentVolunteers++;

            if (task.CurrentVolunteers >= task.RequiredVolunteers)
            {
                task.Status = TaskStatus.Assigned;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Volunteer {VolunteerId} assigned to task {TaskId}", volunteer.VolunteerId, taskId);

            return RedirectToAction("Dashboard");
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(VolunteerTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = new VolunteerTask
                {
                    Title = model.Title,
                    Description = model.Description,
                    Location = model.Location,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    RequiredVolunteers = model.RequiredVolunteers,
                    CurrentVolunteers = 0,
                    Status = TaskStatus.Open,
                    IncidentId = model.IncidentId
                };

                _context.VolunteerTasks.Add(task);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New volunteer task created: {TaskId}", task.TaskId);

                return RedirectToAction("TaskManagement");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> TaskManagement()
        {
            var tasks = await _context.VolunteerTasks
                .Include(t => t.Incident)
                .Include(t => t.Volunteers)
                .ThenInclude(v => v.User)
                .OrderByDescending(t => t.StartDate)
                .ToListAsync();

            return View(tasks);
        }
    }
}