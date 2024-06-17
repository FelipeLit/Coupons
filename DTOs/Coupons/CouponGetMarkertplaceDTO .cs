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
        public string? Status { get; set; }
        public ICollection<CouponUsageGetDTO>? CouponUsages { get; set; }
    }
}