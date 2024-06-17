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
                    return NotFound(new { Message = "No coupons found in the database.", StatusCode = 500, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of coupons
                return Ok(coupons);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
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
                    return BadRequest(new { Message = "Invalid coupon ID.", StatusCode = 400, CurrentDate = DateTime.Now });
                }

                // Call the service to get the coupon by ID
                var coupon = await _service.GetCouponById(id);

                // Check if the coupon is null
                if (coupon == null)
                {
                    return NotFound(new { Message = "404 No coupons found in the database." , currentDate = DateTime.Now});
                }   

                return Ok(coupons);
            }
            catch (Exception) 
            {
                return BadRequest(new { Message = "500 Internal Server Error", currentDate = DateTime.Now});
            }
        }

         [HttpGet, Route("coupons/delete")]
        public async Task<IActionResult> GetAllCouponsDelete()
        {
            try 
            {
                var coupons = await _service.GetAllCouponsRemove();
                if (coupons == null || coupons.Count == 0)
                {
                    return NotFound(new { Message = "404 No coupons found in the database." , currentDate = DateTime.Now});
                }   
                return Ok(coupons);
            } 
            catch (Exception) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now });
            }
        }

        // Method to obtain the coupons created by the authenticated marketing
        // [HttpGet, Route("api/mycoupons")]
        // public IActionResult GetMyCoupons()
        // {
        //     var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized("No se pudo obtener la información del usuario.");
        //     }

        //     var userId = int.Parse(userIdClaim);
        //     var coupons = _service.GetCreatedCoupons(userId);
        //     return Ok(coupons);
        // }

        // This defines a GET endpoint at "api/coupon-usages".
        [HttpGet, Route("api/coupon-usages")]
        public async Task<IActionResult> GetUsersWithCouponsAsync()
        {
            try 
            {
                // Call the service to get users with their coupons.
                var coupons = await _service.GetUsersWithCouponsAsync();

                // Check if the coupons list is null or empty
                if (coupons == null || coupons.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No coupons found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }
                // Return the result as a 200 OK response.
                return Ok(coupons);        
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }
    }
}
