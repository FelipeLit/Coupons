using System.Text.Json.Serialization;

namespace Coupons.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<PurchaseEntity>? Purchases { get; set; }
    }
}