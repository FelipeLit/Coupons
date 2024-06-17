using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class MarketingUserPutDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, ErrorMessage = "Username can't be longer than 255 characters.")]
        public string? Username { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "Email can't be longer than 255 characters.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email format.")]
        public string? Email { get; set; }
    }
}