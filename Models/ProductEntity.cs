namespace Coupons.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<PurchaseEntity>? Purchases { get; set; }
    }
}