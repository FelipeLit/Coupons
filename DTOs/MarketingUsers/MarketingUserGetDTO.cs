using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class MarketingUserGetDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        // Status is required. Can be "Active" or "Inactive"
        [Required(ErrorMessage = "Status Type is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string? Status { get; set; }



        // public ICollection<CouponEntity>? Coupons { get; set; }
        // public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}