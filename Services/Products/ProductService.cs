using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Products
{
    public class ProductService : IProductService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public ProductService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<ProductGetDTO>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            // Returns a list of all products from the database
            return _mapper.Map<ICollection<ProductGetDTO>>(products);
        }

        public async Task<ProductGetDTO> GetProductById(int id)
        {
            // Find the product by ID
            var products = await _context.Products.FindAsync(id);

            // Return the product entity user DTO.
            return _mapper.Map<ProductGetDTO>(products);
        }

        public async Task<bool> UpdateProduct(int id, ProductPutDTO ProductPutDTO)
        {
            // Find the product by ID
            var productSearch = await _context.Products.FindAsync(id);

            // If product not found, return false
            if (productSearch == null)
            {
                return false;
            }

            var productCategoryId = _context.Categories.Any(c => c.Id == ProductPutDTO.CategoryId);

            if (!productCategoryId)
            {
               throw new Exception("Product category ID not found.");
            }

            // Update product properties
            _mapper.Map(ProductPutDTO, productSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
    }
}