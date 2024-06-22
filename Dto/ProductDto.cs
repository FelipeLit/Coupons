using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.Dto
{
    public class ProductDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; } 
        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; } 
    }
}