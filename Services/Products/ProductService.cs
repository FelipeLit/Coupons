
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using AutoMapper;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;

using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public ProductService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                throw new Exception("An error occurred while creating the product: " + ex.Message);
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
        // Private variable to hold the database context

        public async Task<ICollection<ProductForUserDTO>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            // Returns a list of all products from the database
            return _mapper.Map<ICollection<ProductForUserDTO>>(products);
        }

        public async Task<ProductForUserDTO> GetProductById(int id)
        {
            // Find the product by ID
            var products = await _context.Products.FindAsync(id);

            // Return the product entity user DTO.
            return _mapper.Map<ProductForUserDTO>(products);
        }

        public async Task<bool> UpdateProduct(int id, ProductForUserDTO productForUserDTO)
        {
            // Find the product by ID
            var productSearch = await _context.Products.FindAsync(id);

            // If product not found, return false
            if (productSearch == null)
            {
                return false;
            }

            var productCategoryId = _context.Categories.Any(c => c.Id == productForUserDTO.CategoryId);

            if (!productCategoryId)
            {
                throw new Exception("Product category ID not found.");
            }

            // Update product properties
            _mapper.Map(productForUserDTO, productSearch);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;

        }
    }

}
