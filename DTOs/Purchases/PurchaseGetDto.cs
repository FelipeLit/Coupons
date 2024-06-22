namespace Coupons.DTOs.Purchases
{
    public class PurchaseGetDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int MarketplaceUserId { get; set; }
        public int ProductId { get; set; }
    }
}