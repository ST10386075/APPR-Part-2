using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GOTG.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<IncidentReport> IncidentReports { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Volunteer> VolunteerRegistrations { get; set; }
    }
}