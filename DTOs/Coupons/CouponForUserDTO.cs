using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class CouponForUserDTO
    {
        // ID is required. Should throw an error if not provided or if already exists during update
        [Required(ErrorMessage = "ID is required.")]
        public int Id { get; set; }

        // Name is required. Should not be null or empty
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters.")]
        public string? Name { get; set; }

        // Description is required. Should not be null or empty
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string? Description { get; set; }

        // StartDate is required and should be in the future
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format for Start Date.")]
        public DateTime StartDate { get; set; }

        // EndDate is required and should be after StartDate
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format for End Date.")]
        public DateTime EndDate { get; set; }

        // DiscountType is required. Can be "Percentage" or "Net"
        [Required(ErrorMessage = "Discount Type is required.")]
        [RegularExpression("Percentage|Net", ErrorMessage = "Discount Type must be 'Percentage' or 'Net'.")]
        public string? DiscountType { get; set; }

        // IsLimited is required. Should be a boolean
        [Required(ErrorMessage = "IsLimited is required.")]
        public bool? IsLimited { get; set; }

        // UsageLimit is required and should be a positive number
        [Required(ErrorMessage = "Usage Limit is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Usage Limit must be a non-negative number.")]
        public int UsageLimit { get; set; }

        // AmountUses is required and should be a non-negative number
        [Required(ErrorMessage = "Amount Uses is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount Uses must be a non-negative number.")]
        public int AmountUses { get; set; }

        // MinPurchaseAmount is required and should be a non-negative decimal
        [Required(ErrorMessage = "Minimum Purchase Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum Purchase Amount must be greater than zero.")]
        public decimal MinPurchaseAmount { get; set; }

        // MaxPurchaseAmount is required and should be a non-negative decimal and greater than MinPurchaseAmount
        [Required(ErrorMessage = "Maximum Purchase Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Maximum Purchase Amount must be greater than zero.")]
        public decimal MaxPurchaseAmount { get; set; }

        // Status is required. Can be "Inactive" or "Active"
        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Inactive|Active", ErrorMessage = "Status must be 'Inactive' or 'Active'.")]
        public string? Status { get; set; }

        // MarketingUserId is required. Should be a positive integer
        [Required(ErrorMessage = "Marketing User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Marketing User ID must be a positive integer.")]
        public int MarketingUserId { get; set; }
    }
}