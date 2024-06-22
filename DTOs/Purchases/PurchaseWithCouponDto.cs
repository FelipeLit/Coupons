using Coupons.Models;

namespace Coupons.Dto
{
    public class PurchaseWithCouponDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int MarketplaceUserId { get; set; }
        public int PurchaseId { get; set; }
        public ICollection<PurchaseCouponDTO>? PurchaseCoupons { get; set; }
    }
}