using AutoMapper;
using Coupons.Dto;
using Coupons.Models;

namespace SolutionVets.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CouponEntity, CouponViewUserDTO>();
            CreateMap<CouponEntity, CouponsDto>();
            CreateMap<CouponEntity, CouponPutDTO>().ReverseMap();

            CreateMap<ProductEntity, ProductGetDTO>();
            CreateMap<ProductEntity, ProductPutDTO>().ReverseMap();
            
            CreateMap<MarketplaceUserEntity, MarketplaceUserGetCouponDTO>();
            CreateMap<MarketplaceUserEntity, MarketplaceGetDTO>();
            CreateMap<MarketplaceUserEntity, MarketplaceGetDTO>().ReverseMap();

            CreateMap<MarketingUserEntity, MarketingForLoginDTO>();
            CreateMap<MarketingUserEntity, MarketingUserGetDTO>();
            CreateMap<MarketingUserEntity, MarketingUserPutDTO>().ReverseMap();

            CreateMap<CouponUsageEntity, CouponUsageGetDTO>();
            CreateMap<CouponUsageEntity, CouponUsageRedeemDTO>();

            CreateMap<UserRoleEntity, UserRoleGetDTO>();
            CreateMap<UserRoleEntity, UserRolePostDTO>().ReverseMap();

            CreateMap<RoleEntity, RoleGetDTO>();

        }
    }
}