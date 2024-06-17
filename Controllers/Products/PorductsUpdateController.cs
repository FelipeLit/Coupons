using Microsoft.AspNetCore.Mvc;
using Coupons.Services.Products;
using Coupons.Models;

namespace Products
{
    public class ProductsUpdateController : ControllerBase
    {
        // Declare a read-only variable for the product service
        public readonly IProductService _service;

        // Constructor that receives the product service as a parameter
        public ProductsUpdateController(IProductService service)
        {
            _service = service;
        }

         // Endpoint to update a product
        [HttpPut("api/products/update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductForUserDTO productForUserDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if id is valid
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid product ID. ID must be greater than zero.", StatusCode = 400});
            }

            try
            {
                // Try to update the product
                var result = await _service.UpdateProduct(id, productForUserDTO);
                
                if (!result)
                {
                    return NotFound(new { Message = $"Product with ID {id} not found.", StatusCode = 404, });
                }

                return Ok(new { Message = "Product updated successfully.", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the product.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}