using System.Security.Claims;
using Coupons.Services.Slack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupons
{
    public class CouponController : ControllerBase
    {
        // Declare a read-only variable for the coupon service
        private readonly ICouponService _service;
        private readonly SlackService _slackService;

        // Constructor that receives the coupon service as a parameter
        public CouponController(ICouponService service,SlackService slackService)
        {
            _service = service;
            _slackService = slackService;
        }
        
        [Authorize(Roles = "Marketing")]
        // Endpoint to get all coupons
        [HttpGet, Route("api/coupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
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
                
                // Call the service to get all coupons
                var coupons = await _service.GetAllCoupons();

                // Check if the coupons list is null or empty
                if (coupons == null || coupons.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No coupons found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
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

        [Authorize(Roles = "Marketing")]
        // Endpoint to get a coupon by its ID
        [HttpGet, Route("api/coupons/{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            // Validate the ID is a positive integer
            if (id <= 0)
            {
                // Return a 400 Bad Request response with a message
                return BadRequest(new { Message = "Invalid coupon ID.", StatusCode = 400, CurrentDate = DateTime.Now });
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

                // Call the service to get the coupon by ID
                var coupon = await _service.GetCouponById(id);

                // Check if the coupon is null
                if (coupon == null)
                {
                    return NotFound(new { Message = "404 No coupons found in the database." , currentDate = DateTime.Now});
                }   

                return Ok(coupon);
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
            catch (Exception ex) 
            {
                await _slackService.SlackNotifier(ex.Message);
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now, error =ex.Message });
            }
        }

        [HttpGet]
        [Route("coupons/history")]
        public async Task<IActionResult> GetAllCouponsHistory()
        {
            try
            {
                var coupons = await _service.GetAllCouponHistory();
                if (coupons == null || coupons.Count == 0)
                {
                    return NotFound(new { Message = "404 No coupons found in the database." , currentDate = DateTime.Now});
                }   
                return Ok(coupons);
            }
            catch (Exception ex)
            {
                 await _slackService.SlackNotifier(ex.Message);
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now, error =ex.Message });
            }
        }

        [HttpPost]
        [Route("api/sendmessage")]
        public async Task<IActionResult> SendMessage()
        {
            try{
                throw new Exception("Error controlado");
            }
            catch (Exception ex)
            {
                await _slackService.SlackNotifier(ex.Message);
                return StatusCode (500, "Error");
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
        
    }
}
