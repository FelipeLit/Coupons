using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class MarketplaceUserGetCouponDTO // Also used for PUT
    {
        // ID is required and should be a positive integer
        [Required(ErrorMessage = "ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
        public int Id { get; set; }

        // Username is required and should not exceed 255 characters
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, ErrorMessage = "Username can't be longer than 255 characters.")]
        public string? Username { get; set; }

        // Password is required and should meet complexity requirements
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        public string? Password { get; set; }
        public string? Status { get; set; }

        // Email is required and should be in a valid email format
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "Email can't be longer than 255 characters.")]
        public string? Email { get; set; }
        public ICollection<CouponUsageGetDTO>? CouponUsages { get; set; }
    }
}