using System.ComponentModel.DataAnnotations;
using GOTG.Models;

namespace GOTG.ViewModels
{
    public class IncidentReportViewModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title must be less than 200 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters")]
        public string Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Location must be less than 100 characters")]
        public string Location { get; set; }

        [Required]
        public DisasterType DisasterType { get; set; }

        [Required]
        public DateTime IncidentDate { get; set; } = DateTime.Now;

        [Range(1, 5, ErrorMessage = "Severity level must be between 1 and 5")]
        public int SeverityLevel { get; set; } = 1;

        [Range(0, int.MaxValue, ErrorMessage = "Number of people affected must be positive")]
        public int PeopleAffected { get; set; }
    }
}