using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOTG.Models
{
    public class IncidentReport
    {
        [Key]
        public int IncidentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public DisasterType DisasterType { get; set; }

        public DateTime IncidentDate { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public IncidentStatus Status { get; set; } = IncidentStatus.Pending;

        public int SeverityLevel { get; set; } = 1; // 1-5 scale

        public int PeopleAffected { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    public enum DisasterType
    {
        Flood,
        Earthquake,
        Wildfire,
        Hurricane,
        Tornado,
        Drought,
        Other
    }

    public enum IncidentStatus
    {
        Pending,
        Verified,
        InProgress,
        Resolved,
        FalseReport
    }
}