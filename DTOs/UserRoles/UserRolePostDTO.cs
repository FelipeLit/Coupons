using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class UserRolePostDTO
    {
        // MarketingUserId is required and should be a positive integer
        [Required(ErrorMessage = "Marketing User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Marketing User ID must be a positive integer.")]
        public int MarketingUserId { get; set; }

        // RoleId is required and should be a positive integer
        [Required(ErrorMessage = "Role ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Role ID must be a positive integer.")]
        public int RoleId { get; set; }

        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; }
    }
}