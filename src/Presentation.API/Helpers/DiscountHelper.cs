using Core.Data.Helpers;
using Presentation.API.Shared.Models;

namespace Presentation.API.Helpers
{
    public static class DiscountHelper
    {
        public static PriceResponse ApplyDiscuountToResponse(PriceResponse response, DiscountEnum discountEnum)
        {
            var discountInfo = MappingHelper.DiscountInfo[discountEnum];
            response.Discounts.Add(discountInfo);
            response.FinalPrice = ApplyDiscuount(response.FinalPrice, discountInfo.Percentage);
            return response;
        }

        public static double ApplyDiscuount(double price, double percentage)
        => price - (price * percentage / 100);
    }
}
