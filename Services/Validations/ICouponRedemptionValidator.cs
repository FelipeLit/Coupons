using System.Threading.Tasks;
using Coupons.Models;

namespace Coupons.Services.Validations
{
    public interface ICouponRedemptionValidator
    {
        Task<string> ValidateRedemption(string codeCoupon, string username);
    }
}
