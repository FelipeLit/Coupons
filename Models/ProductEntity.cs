using System.Text.Json.Serialization;

namespace Coupons.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? Status { get; set; } 
        public ICollection<PurchaseEntity>? Purchases { get; set; }
        public CategoryEntity? Category { get; set; }

    }
}