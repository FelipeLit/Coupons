using Coupons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coupons
{
    public class CouponsUpdateController : ControllerBase
    {
        // Declare a read-only variable for the coupon service
        public readonly ICouponService _service;

        // Constructor that receives the coupon service as a parameter
        public CouponsUpdateController(ICouponService service)
        {
            _service = service;
        }

         // Endpoint to update a coupon
        [HttpPut("api/coupons/update/{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] CouponForUserDTO couponForUserDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if id is valid
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid coupon ID. ID must be greater than zero.", StatusCode = 400 });
            }

            try
            {
                // Try to update the coupon
                var result = await _service.UpdateCoupon(id, couponForUserDTO);
                
                if (!result)
                {
                    return NotFound(new { Message = $"Coupon with ID {id} not found.", StatusCode = 404, });
                }

                return Ok(new { Message = "Coupon updated successfully.", StatusCode = 200});
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the coupon.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}