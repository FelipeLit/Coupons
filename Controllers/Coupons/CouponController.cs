using Microsoft.AspNetCore.Mvc;

namespace Coupons
{
    public class CouponController : ControllerBase
    {
        // Declare a read-only variable for the coupon service
        public readonly ICouponService _service;

        // Constructor that receives the coupon service as a parameter
        public CouponController(ICouponService service)
        {
            _service = service;
        }
        
        // Endpoint to get all coupons
        [HttpGet, Route("api/coupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
            try 
            {
                // Call the service to get all coupons
                var coupons = await _service.GetAllCoupons();

                // Check if the coupons list is null or empty
                if (coupons == null || coupons.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "404 No coupons found in the database.", CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of coupons
                return Ok(coupons);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "500 Internal Server Error", CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }

        // Endpoint to get a coupon by its ID
        [HttpGet, Route("api/coupons/{id}")]
        public async Task<ActionResult> GetCouponById(int id)
        {
            try 
            {
                // Validate the ID is a positive integer
                if (id <= 0)
                {
                    // Return a 400 Bad Request response with a message
                    return BadRequest(new { Message = "400 Invalid coupon ID.", CurrentDate = DateTime.Now });
                }

                // Call the service to get the coupon by ID
                var coupon = await _service.GetCouponById(id);

                // Check if the coupon is null
                if (coupon == null)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "404 Coupon not found in the database.", CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the coupon
                return Ok(coupon);
            }
            catch (Exception) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "500 Internal Server Error", CurrentDate = DateTime.Now });
            }
        }
    }
}
