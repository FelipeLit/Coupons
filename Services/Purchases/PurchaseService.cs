using AutoMapper;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Purchases
{

    public class PurchaseService : IPurchaseService
    {
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;
        

        public PurchaseService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<PurchaseWithCouponDto>> GetAllCouponsPurchased()
        {

            var purchases = await _context.Purchases
                .Include(p => p.PurchaseCoupons) 
                    .ThenInclude(p => p.Coupon)
                .ToListAsync();

            return _mapper.Map<ICollection<PurchaseWithCouponDto>>(purchases);
        
        }


    }

}
