using System.ComponentModel.DataAnnotations;
using GOTG.Models;

namespace GOTG.ViewModels
{
    public class VolunteerRegistrationViewModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "Skills description must be less than 500 characters")]
        public string Skills { get; set; }

        [Required]
        public Availability Availability { get; set; }

        [StringLength(1000, ErrorMessage = "Experience description must be less than 1000 characters")]
        public string Experience { get; set; }

        [StringLength(500, ErrorMessage = "Certifications description must be less than 500 characters")]
        public string Certifications { get; set; }
    }

    public class VolunteerDashboardViewModel
    {
        public Volunteer Volunteer { get; set; }
        public List<VolunteerTask> AvailableTasks { get; set; }
    }

    public class VolunteerTaskViewModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(1, 1000, ErrorMessage = "Required volunteers must be at least 1")]
        public int RequiredVolunteers { get; set; }

        public int? IncidentId { get; set; }
    }
}