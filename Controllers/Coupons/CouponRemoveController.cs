using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Coupons
{
    public class CouponRemoveController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponRemoveController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPut]
        [Route("coupons/deleted/{id}")]
        public async Task<IActionResult> RemoveCoupon(int id)
        {
            try
            {
                var coupon = await _couponService.ChangeStatus(id);
                if (coupon != null)
                {
                    return Ok($"Coupon with ID: {coupon.Id} status was change");
                }
                else
                {
                    return NotFound("Coupon does not exist");
                }
            }

           catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut]
        [Route("coupons/restore/{id}")]
        public async Task<IActionResult> RestoreCoupon(int id)
        {
            try
            {
                var coupon = await _couponService.RestoreStatus(id);
                if (coupon != null)
                {
                    return Ok($"Coupon with ID: {coupon.Id} status was change");
                }
                else
                {
                    return NotFound("Coupon does not exist");
                }
            }

           catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
        
    }
}