using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Products
{
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Route("prducts/delete")]
        public async Task<IActionResult> GetAllProductssDelete()
        {
            try 
            {
                var products = await    _productService.GetAllProductsRemove();
                if (products == null || products.Count == 0)
                {
                    return NotFound(new { Message = "404 No products found in the database." , currentDate = DateTime.Now});
                }   
                return Ok(products);
            } 
            catch (Exception) 
            {
                return BadRequest(new { Message = "500 Internal Server Error", currentDate = DateTime.Now});
            }
        }
    }
}