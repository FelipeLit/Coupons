using AutoMapper;
using Coupons.Models;

namespace SolutionVets.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponEntity, CouponForUserDTO>();
            CreateMap<CouponEntity, CouponForUserDTO>().ReverseMap();

            CreateMap<ProductEntity, ProductForUserDTO>();
            CreateMap<ProductEntity, ProductForUserDTO>().ReverseMap();
            
            CreateMap<MarketplaceUserEntity, MarketplaceForUserDTO>();
            CreateMap<MarketingUserEntity, MarketingForLoginDTO>();
            CreateMap<CouponUsageEntity, CouponUsageForCouponsDTO>();

        }
    }
}