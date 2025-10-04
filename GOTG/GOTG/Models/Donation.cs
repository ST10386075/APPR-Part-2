using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOTG.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DonationType DonationType { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public decimal? Amount { get; set; }
        public int? Quantity { get; set; }

        [StringLength(100)]
        public string ItemCategory { get; set; } // Food, Clothing, Medical, etc.

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
        public DonationStatus Status { get; set; } = DonationStatus.Pending;

        [StringLength(500)]
        public string DeliveryAddress { get; set; }

        public int? IncidentId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("IncidentId")]
        public virtual IncidentReport Incident { get; set; }
    }

    public enum DonationType
    {
        Monetary,
        Goods,
        Services
    }

    public enum DonationStatus
    {
        Pending,
        Received,
        InTransit,
        Delivered,
        Cancelled
    }
}