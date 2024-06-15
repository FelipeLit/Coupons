using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coupons
{
    // Implementation of ICouponService interface for managing coupons
    public class CouponService : ICouponService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;

        // Constructor injecting the database context dependency
        public CouponService(CouponsContext context)
        {
            _context = context;
        }

        // Asynchronous method to retrieve all coupons
        public async Task<ICollection<CouponEntity>> GetAllCoupons()
        {
            // Returns a list of all coupons from the database
            return await _context.Coupons.ToListAsync();
        }

    }
}
