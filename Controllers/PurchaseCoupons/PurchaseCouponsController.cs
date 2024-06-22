using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.PurchaseWithCouponsDto
{
    public class PurchaseWithCouponsDtoController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public PurchaseWithCouponsDtoController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        [Route("purchased/coupons")]
        public async Task<IActionResult> GetPurchasedCoupons()
        {
            try
            {
                var purchasedCoupons = await _couponService.GetAllCouponsPurchased();
                if (purchasedCoupons == null || purchasedCoupons.Count == 0)
                {
                    return NotFound("No purchased coupons found.");
                }
                return Ok(purchasedCoupons);
            }
            catch (ValidationException ve)
            {
                return BadRequest(new { Message = ve.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = e.Message });
            }
        }
    }
}