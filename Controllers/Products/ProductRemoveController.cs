using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Products
{
    public class ProductRemoveController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductRemoveController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut]
        [Route("products/deleted/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var product = await _productService.ChangeStatus(id);
                if (product != null)
                {
                    return Ok($"product with ID: {product.Id} status was change");
                }
                else
                {
                    return NotFound("product does not exist");
                }
            }

           catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

         [HttpPut]
        [Route("products/restore/{id}")]
        public async Task<IActionResult> RestoreProduct(int id)
        {
            try
            {
                var product = await _productService.RestoreStatus(id);
                if (product != null)
                {
                    return Ok($"product with ID: {product.Id} status was change");
                }
                else
                {
                    return NotFound("product does not exist");
                }
            }

           catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
        
    }
}