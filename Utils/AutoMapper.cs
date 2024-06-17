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
            
            CreateMap<MarketplaceUserEntity, MarketplaceUserForUserDTO>();
            CreateMap<MarketplaceUserEntity, MarketplaceUserForUserDTO>().ReverseMap();

            CreateMap<MarketingUserEntity, MarketingForLoginDTO>();

            CreateMap<MarketingUserEntity, MarketingUserGetDTO>();
            CreateMap<MarketingUserEntity, MarketingUserPutDTO>().ReverseMap();

            CreateMap<CouponUsageEntity, CouponUsageForCouponsDTO>();

            CreateMap<UserRoleEntity, UserRoleGetDTO>();
            CreateMap<UserRoleEntity, UserRolePostDTO>().ReverseMap();

            CreateMap<RoleEntity, RoleGetDTO>();

        }
    }
}