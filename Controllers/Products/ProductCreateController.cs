using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Dto;
using Coupons.Models;
using Coupons.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Products
{
    public class ProductCreateController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductCreateController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> Create([FromBody] ProductPutDTO product)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _productService.CreateProduct(product);
                return StatusCode(201, "Product add");
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