using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class RolePostDTO
    {
        public string? Name { get; set; }
        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; }
    }
}