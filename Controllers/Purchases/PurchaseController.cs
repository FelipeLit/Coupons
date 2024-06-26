using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Services.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Purchases
{
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;

    }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchases()
        {
            try
        {
            var purchases = await _purchaseService.GetAllCouponsPurchased();
            if (purchases == null || purchases.Count == 0)
            {
                return NotFound("No purchases found.");
            }
            return Ok(purchases);
        }
        catch (Exception ex)
        {
           
            return BadRequest(new { Message = "An error occurred while updating the coupon.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
        }
        }

        
    }
}