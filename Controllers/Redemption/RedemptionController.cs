using Coupons.Services.Redemptions;
using Coupons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Redemption
{
    public class RedemptionController : ControllerBase
    {
        private readonly IRedemptionService _service;

        public RedemptionController(IRedemptionService service)
        {
            _service = service;
        }

        // Method to redeem a coupon
        [HttpPost, Route("api/coupon/redeem")]
        public async Task<IActionResult> RedeemCoupon([FromBody] CouponRedemptionRequest request)
        {
            // Validate the incoming request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                // Ensure the CodeCoupon and Username are not null or empty
                if (string.IsNullOrEmpty(request.CodeCoupon) || string.IsNullOrEmpty(request.Username))
                {
                    return BadRequest(new { message = "Coupon code and username cannot be null or empty.", StatusCode = 500,  CurrentDate = DateTime.Now });
                }

                // We call the method to redeem the coupon and get the result
                var result = await _service.RedeemCoupon(request.CodeCoupon, request.Username);

                // Return the DTO if redemption is successful
                if (result != null)
                {
                    return Ok(new {result, StatusCode = 200});
                }

                // Return a bad request if something went wrong
                return BadRequest(new { Message = "Error rediming coupon", StatusCode = 500, CurrentDate = DateTime.Now});
            }
            catch (Exception ex)
            {
                // In case of exception, we return an internal server error along with the details
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 400, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }
    }
}
