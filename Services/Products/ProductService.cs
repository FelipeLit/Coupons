using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly CouponsContext _context;
        public ProductService(CouponsContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> ChangeStatus(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    throw new ValidationException($"product with ID: {id} not found.");
                }

                if (product.Status == "Inactive")
                {
                    throw new ValidationException($"product with ID: {id} is already inactive.");
                }

                product.Status = "Inactive";
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the product. Please try again later." + ex.Message);
            }
        }

        public async Task<ProductEntity> CreateProduct(ProductDto productDto)
        {
           try
           {
            var Product = new ProductEntity
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
            };
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
              
            
            return Product;
           }
           catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the product: "+ex.Message);
            }
        }

        public async Task<ICollection<ProductEntity>> GetAllProductsRemove()
        {
            var products = await _context.Products.Where(p=>p.Status == "Inactive").ToListAsync();
            if (products != null)
            {
                return products;
            }
            else 
            {
                return null;
            }
        }

        public async Task<ProductEntity> RestoreStatus(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    throw new ValidationException($"product with ID: {id} not found.");
                }

                if (product.Status == "Active")
                {
                    throw new ValidationException($"product with ID: {id} is already active.");
                }

                product.Status = "Active";
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the product. Please try again later." + ex.Message);
            }
        }
    }
}