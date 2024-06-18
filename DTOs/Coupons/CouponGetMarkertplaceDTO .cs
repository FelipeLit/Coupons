using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class CouponGetMarkertplaceDTO 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DiscountType { get; set; }
        public bool? IsLimited { get; set; }
        public int UsageLimit { get; set; }
        public int AmountUses { get; set; }
        public decimal MinPurchaseAmount { get; set; }
        public decimal MaxPurchaseAmount { get; set; }
        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; }
        public ICollection<CouponUsageGetDTO>? CouponUsages { get; set; }
    }
}