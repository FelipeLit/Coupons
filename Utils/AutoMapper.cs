using AutoMapper;
using Coupons.Models;

namespace SolutionVets.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponEntity, CouponGetDTO>();
            CreateMap<CouponEntity, CouponGetDTO>().ReverseMap();

            CreateMap<ProductEntity, ProductGetDTO>();
            CreateMap<ProductEntity, ProductGetDTO>().ReverseMap();
            
            CreateMap<MarketplaceUserEntity, MarketplaceGetDTO>();
            CreateMap<MarketplaceUserEntity, MarketplaceGetDTO>().ReverseMap();

            CreateMap<MarketingUserEntity, MarketingForLoginDTO>();

            CreateMap<MarketplaceUserEntity, MarketplaceUserGetCoupon>();
            CreateMap<MarketingUserEntity, MarketingUserGetDTO>();
            CreateMap<MarketingUserEntity, MarketingUserPutDTO>().ReverseMap();

            CreateMap<CouponUsageEntity, CouponUsageGetDTO>();

            CreateMap<UserRoleEntity, UserRoleGetDTO>();
            CreateMap<UserRoleEntity, UserRolePostDTO>().ReverseMap();

            CreateMap<RoleEntity, RoleGetDTO>();

        }
    }
}