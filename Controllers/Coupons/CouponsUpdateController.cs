using System.Security.Claims;
using Coupons.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Marketing")]
        // Endpoint to update a coupon
        [HttpPut("api/coupons/update/{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] CouponPutDTO CouponPutDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Get the claims of the current user
                var userRolesClaims = User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();

                // Check if the user's claims do not exist or are empty
                if (userRolesClaims == null || !userRolesClaims.Any())
                {
                    return Unauthorized("Could not get user information.");
                }

                // Check if the user has the "Marketing" role
                if (!userRolesClaims.Contains("Marketing"))
                {
                    return Unauthorized("You don't have permissions (Only Marketing).");
                }

                // Check if id is valid
                if (id <= 0)
                {
                    return BadRequest(new { Message = "Invalid coupon ID. ID must be greater than zero.", StatusCode = 400, CouponId = id, CurrentDate = DateTime.Now});
                }
                
                // Try to update the coupon
                var result = await _service.UpdateCoupon(id, CouponPutDTO);
                
                if (!result)
                {
                    return NotFound(new { Message = $"Coupon with ID {id} not found.", StatusCode = 404, CurrentDate = DateTime.Now,  });
                }

                return Ok(new { Message = "Coupon updated successfully.", StatusCode = 200, CouponId = id, CurrentDate = DateTime.Now});
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the coupon.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}