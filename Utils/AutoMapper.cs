using AutoMapper;
using Coupons.Models;

namespace SolutionVets.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponEntity ,CouponEntityUserDTO>();

        }
    }
}