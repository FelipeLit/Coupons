using AutoMapper;
using Coupons.Models;

namespace SolutionVets.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponEntity, CouponForUserDTO>();
            CreateMap<MarketplaceUserEntity, MarketplaceForUserDTO>();
            CreateMap<MarketingUserEntity, MarketingForLoginDTO>();
            CreateMap<CouponUsageEntity, CouponUsageForCouponsDTO>();

        }
    }
}