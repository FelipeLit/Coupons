
using Coupons.Dto;

using Coupons.Models;

namespace Coupons.Services.Products
{
    public interface IProductService
    {

         
        // Asynchronous method that returns a task completed with the created product entity.
        Task<ProductEntity> CreateProduct(ProductDto productDto);
        //Change status of product Active to Inactive
        Task<ProductEntity> ChangeStatus(int id);
        //Restore status of product Inactive to active
        Task<ProductEntity> RestoreStatus(int id);

        // Asynchronous method that returns a task completed with a collection of product entities.
        Task<ICollection<ProductForUserDTO>> GetAllProducts();
        // Asynchronous method that returns a task completed with a product entity based on the provided ID.
        Task<ProductForUserDTO> GetProductById(int id);
        // Asynchronous method that returns a task completed with a boolean value indicating if the product update based on an ID was successful.
        Task<bool> UpdateProduct(int id, ProductForUserDTO productForUserDTO);


    }
}