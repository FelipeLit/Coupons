namespace Coupons.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductEntity>? Products { get; set; }
    }
}