using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOTG.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Skills { get; set; }

        [Required]
        public Availability Availability { get; set; }

        public string Experience { get; set; }

        public string Certifications { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public VolunteerStatus Status { get; set; } = VolunteerStatus.Active;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<VolunteerTask> Tasks { get; set; }
    }

    public class VolunteerTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RequiredVolunteers { get; set; }
        public int CurrentVolunteers { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Open;

        public int? IncidentId { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }
        public virtual IncidentReport Incident { get; set; }
    }

    public enum Availability
    {
        Weekdays,
        Weekends,
        Evenings,
        Anytime
    }

    public enum VolunteerStatus
    {
        Active,
        Inactive,
        Suspended
    }

    public enum TaskStatus
    {
        Open,
        Assigned,
        InProgress,
        Completed,
        Cancelled
    }
}