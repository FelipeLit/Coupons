using Coupons.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Products
{
    public class ProductController : ControllerBase
    {
        // Declare a read-only variable for the product service
        public readonly IProductService _service;

        // Constructor that receives the product service as a parameter
        public ProductController(IProductService service)
        {
            _service = service;
        }
        
        [AllowAnonymous]
        // Endpoint to get all products
        [HttpGet, Route("api/products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try 
            {
                // Call the service to get all products
                var products = await _service.GetAllProducts();

                // Check if the products list is null or empty
                if (products == null || products.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No products found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of products
                return Ok(products);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }
        
        [AllowAnonymous]
        // Endpoint to get a product by its ID
        [HttpGet, Route("api/products/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            try 
            {
                // Validate the ID is a positive integer
                if (id <= 0)
                {
                    // Return a 400 Bad Request response with a message
                    return BadRequest(new { Message = "Invalid product ID.", StatusCode = 400, CurrentDate = DateTime.Now });
                }

                // Call the service to get the product by ID
                var product = await _service.GetProductById(id);

                // Check if the product is null
                if (product == null)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "Product not found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the product
                return Ok(product);
            }
            catch (Exception) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = " Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now });
            }
        }

        // Method to obtain the products created by the authenticated marketing
        // [HttpGet, Route("api/myproducts")]
        // public IActionResult GetMyProducts()
        // {
        //     var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized("No se pudo obtener la información del usuario.");
        //     }

        //     var userId = int.Parse(userIdClaim);
        //     var products = _service.GetCreatedProducts(userId);
        //     return Ok(products);
        // }


    }
}
