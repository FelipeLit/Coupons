namespace Coupons.Models
{
    public class PurchaseEntity
    {
        public int Id { get; set; }
        public int MarketplaceUserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public MarketplaceUserEntity? MarketplaceUser { get; set; } 
        public ProductEntity? Product { get; set; }
        public ICollection<PurchaseCouponEntity>? PurchaseCoupons { get; set; }
    }
}