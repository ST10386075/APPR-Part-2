using System.ComponentModel.DataAnnotations;
using DisasterAlleviationFoundation.Models;

namespace GOTG.ViewModels
{
    public class DonationViewModel
    {
        [Required]
        public DonationType DonationType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal? Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int? Quantity { get; set; }

        [StringLength(100)]
        public string ItemCategory { get; set; }

        [Required]
        [StringLength(500)]
        public string DeliveryAddress { get; set; }

        public int? IncidentId { get; set; }

        public dynamic AvailableIncidents { get; set; }
    }
}