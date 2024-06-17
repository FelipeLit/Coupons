using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class ProductPutDTO
    {
        // Name is required and should not exceed 255 characters
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters.")]
        public string? Name { get; set; }

        // Price is required and should be a positive number
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        // CategoryId is required and should be a positive integer
        [Required(ErrorMessage = "Category ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive integer.")]
        public int CategoryId { get; set; }
        
    }
}