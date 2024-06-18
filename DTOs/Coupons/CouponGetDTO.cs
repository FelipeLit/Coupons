using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class CouponGetDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DiscountType { get; set; }
        public decimal MinPurchaseAmount { get; set; }
        public decimal MaxPurchaseAmount { get; set; }
        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; }

    }
}