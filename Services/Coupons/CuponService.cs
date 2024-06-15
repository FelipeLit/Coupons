using AutoMapper;
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
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public CouponService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Asynchronous method to retrieve all coupons
        public async Task<ICollection<CouponEntityUserDTO>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponEntityUserDTO>>(coupons);
        }

        public async Task<CouponEntityUserDTO> GetCouponById(int id)
        {
             var coupons = await _context.Coupons.FindAsync(id);

            return _mapper.Map<CouponEntityUserDTO>(coupons);
        }

    }
}
