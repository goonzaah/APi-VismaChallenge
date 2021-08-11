using Core.Data.Helpers;
using Core.Data.Models;
using Presentation.API.Helpers;
using Presentation.API.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.API
{
    public class DiscountHelperTests
    {
        [Fact]
        public async Task MainHandler_ShouldReturn_PriceResponse_LessThanMinimunTime()
        {
            var response = new PriceResponse() { FinalPrice = 100, TotalPrice = 100};
            response = DiscountHelper.ApplyDiscuountToResponse(response, DiscountEnum.Quantity);

            var correctResponse = new PriceResponse()
            {
                FinalPrice = 70,
                TotalPrice = 100,
                Discounts = new List<Discount>() { MappingHelper.DiscountInfo[DiscountEnum.Quantity] }
            };

            Assert.Equal(correctResponse.FinalPrice, response.FinalPrice);
            Assert.Equal(correctResponse.TotalPrice, response.TotalPrice);
            Assert.Equal(correctResponse.Discounts, response.Discounts);
        }
    }
}
