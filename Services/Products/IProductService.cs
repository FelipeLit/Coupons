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
        Task<ICollection<ProductEntity>> GetAllProductsRemove();

    }
}