using Microsoft.AspNetCore.Mvc;

namespace Coupons
{
    public class CouponController : ControllerBase
    {
        public readonly ICouponService _service;
        public CouponController(ICouponService service)
        {
            _service = service;
        }
        
        [HttpGet, Route("api/coupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
            try 
            {
                var coupons = await _service.GetAllCoupons();
                if (coupons == null || coupons.Count == 0)
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

        [Route("api/coupons/{id}")]
        public async Task<ActionResult> GetCouponById(int id)
        {
            try 
            {
                var coupons = await _service.GetCouponById(id);

                if (id <= 0)
                {
                    return NotFound(new { Message = "404 Couldn't find coupon", currentDate = DateTime.Now});
                }
                else if (coupons == null)
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
    }
}