using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Dto;
using Coupons.Services.MarketingUsers;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.MarketingUsers
{
    public class MarketingCreateController : ControllerBase
    {
        private readonly IMarketingUserService _marketingUserService;
        public MarketingCreateController(IMarketingUserService marketingUserService)
        {
            _marketingUserService = marketingUserService;
        }

        [HttpPost]
        [Route("marketinguser/create")]
        public async Task<IActionResult> CreateMarketingUser([FromBody] MarketingUserDto marketingUserDto)
        {
         try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var marketingUser = await _marketingUserService.CreateMarketingUser(marketingUserDto);
                return Ok("marketing user was created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}