using Coupons.Models;

namespace Coupons.Services.Products
{
    public interface IProductService
    {
        // Asynchronous method that returns a task completed with a collection of product entities.
        Task<ICollection<ProductGetDTO>> GetAllProducts();
        // Asynchronous method that returns a task completed with a product entity based on the provided ID.
        Task<ProductGetDTO> GetProductById(int id);
        // Asynchronous method that returns a task completed with a boolean value indicating if the product update based on an ID was successful.
        Task<bool> UpdateProduct(int id, ProductGetDTO ProductGetDTO);

    }
}