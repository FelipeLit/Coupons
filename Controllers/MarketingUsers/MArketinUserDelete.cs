using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Services.MarketingUsers;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.MarketingUsers
{
    public class MArketinUserDelete : ControllerBase
    {
        private readonly IMarketingUserService _marketingUserService;
        public MArketinUserDelete(IMarketingUserService marketingUserService)
        {
            _marketingUserService = marketingUserService;
        }
        [HttpPut]
        [Route("marketinguser/delete/{id}")]
        public async Task<IActionResult> DeleteMarketingUser(int id)
        {
            try
            {
                var marketinguser = await _marketingUserService.ChangeStatus(id);
                if (marketinguser!= null)
                {
                    return Ok($"marketinguser with ID: {marketinguser.Id} was deleted");
                }
                else
                {
                    return NotFound("marketinguser does not exist");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut]
        [Route("marketinguser/restore/{id}")]
        public async Task<IActionResult> RestoreMarketinguser(int id)
        {
            try
            {
                var marketinguser = await _marketingUserService.RestoreStatus(id);
                if (marketinguser!= null)
                {
                    return StatusCode (201,$"marketinguser with ID: {marketinguser.Id} was restore");
                }
                else
                {
                    return NotFound("marketinguser does not exist");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}