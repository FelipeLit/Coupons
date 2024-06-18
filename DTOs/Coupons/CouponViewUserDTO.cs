namespace Coupons.Models
{
    public class CouponViewUserDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DiscountType { get; set; }
        public decimal MinPurchaseAmount { get; set; }
        public decimal MaxPurchaseAmount { get; set; }
        public string? Status { get; set; }

    }
}