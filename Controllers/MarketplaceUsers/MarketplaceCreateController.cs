using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Dto;
using Coupons.Services.MarketplaceUsers;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.MarketplaceUsers
{
    public class MarketplaceCreateController : ControllerBase
    {
        private readonly IMarketplaceUserService _marketplaceUserService;
        public MarketplaceCreateController(IMarketplaceUserService marketplaceUserService)
        {
            _marketplaceUserService = marketplaceUserService;
        }
        [HttpPost]
        [Route("marketplaceuser")]
        public async Task<IActionResult> CreateMarketplaceuser([FromBody] MarketplaceUserDto marketplaceUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var marketplaceUser = await _marketplaceUserService.CreateMarketplaceUser(marketplaceUserDto);
                return Ok("marketplaceUser was created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}