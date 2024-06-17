using Coupons.Models;
using Coupons.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Coupons.Controllers.Validations
{
    public class ValidationsController : ControllerBase
    {
        private readonly ICouponRedemptionValidator _validator;

        public ValidationsController(ICouponRedemptionValidator validator)
        {
            _validator = validator;
        }

        // Endpoint for validating coupon redemption via HTTP GET
        [HttpGet]
        [Route("api/validations/coupon-redemption")]
        public async Task<IActionResult> ValidateCouponRedemption([FromBody] CouponRedemptionRequest request)
        {
            try
            {
                // Check if CodeCoupon or Username are empty
                if (string.IsNullOrEmpty(request.CodeCoupon) || string.IsNullOrEmpty(request.Username))
                {
                    return BadRequest(new { Message = "Coupon code and username cannot be empty.", StatusCode = 400, CurrentDate = DateTime.Now });
                }

                // Validate coupon redemption
                var validationResult = await _validator.ValidateRedemption(request.CodeCoupon, request.Username);

                // Respond based on validation result
                if (string.IsNullOrEmpty(validationResult))
                {
                    return Ok(new { Message = "Coupon is valid for redemption.", StatusCode = 200, CurrentDate = DateTime.Now });
                }
                else
                {
                    return BadRequest(new { Message = validationResult, StatusCode = 400, CurrentDate = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return BadRequest(new { Message = "Error validating coupon for redemption.", StatusCode = 500, Error = ex.Message });
            }
        }
    }
}
