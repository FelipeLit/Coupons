using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Coupons
{
    public class CouponsCreateController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponsCreateController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        [Route("api/coupons/create")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponsDto coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _couponService.CreateCoupon(coupon);
                return Ok("New coupon add");
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

    }
}