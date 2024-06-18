using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Services.MarketplaceUsers;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.MarketplaceUsers
{
    public class MarketplaceUserDeleteControlller : ControllerBase
    {
        private readonly IMarketplaceUserService _marketplaceUserService;
        public MarketplaceUserDeleteControlller(IMarketplaceUserService marketplaceUserService)
        {
            _marketplaceUserService = marketplaceUserService;
        }

        [HttpPut]
        [Route("marketplaceuser/delete/{id}")]
        public async Task<IActionResult> DeleteMarketplaceUser(int id)
        {
            try
            {
                var marketplaceUser = await _marketplaceUserService.ChangeStatus(id);
                if (marketplaceUser!= null)
                {
                    return Ok($"MarketplaceUser with ID: {marketplaceUser.Id} was deleted");
                }
                else
                {
                    return NotFound("MarketplaceUser does not exist");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut]
        [Route("marketplace/restore/{id}")]
        public async Task<IActionResult> RestoreMarketplaceUser(int id)
        {
            try
            {
                var marketplaceUser = await _marketplaceUserService.RestoreStatus(id);
                if (marketplaceUser!= null)
                {
                    return StatusCode (201,$"MarketplaceUser with ID: {marketplaceUser.Id} was restore");
                }
                else
                {
                    return NotFound("MarketplaceUser does not exist");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

    }
}