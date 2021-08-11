using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Data.Helpers;
using Core.Data.Models;
using Core.Data.Repositories;
using Core.Entities;
using MediatR;
using Moq;
using Presentation.API.Handlers.Rent.GetPrice;
using Presentation.API.Handlers.Rent.GetPrice.Models;
using Presentation.API.Helpers;
using Test.API.Stubs;
using Xunit;

namespace Test.API
{
    
    public class GetPriceRequestHandlerTests
    {
        private readonly Mock<IRentPriceRepository> _rentPriceRepositoryMock = new Mock<IRentPriceRepository>();
        private GetPriceRequestHandler _sut;

        public GetPriceRequestHandlerTests(){}

        [Fact]
        public async Task MainHandler_ShouldReturn_PriceResponse_WithoutDiscount()
        {
            var productId = Guid.NewGuid();
            var rentPrices = RentPriceStub.rentPrice_01;

            var discountQuantityModel = MappingHelper.DiscountInfo[DiscountEnum.Quantity];

            _rentPriceRepositoryMock.Setup(x => x.GetByProductId(productId)).ReturnsAsync(rentPrices);

            _sut = new GetPriceRequestHandler(_rentPriceRepositoryMock.Object);

            var response = await _sut.Handle(new GetPriceRequest() { ProductId = productId.ToString(), Quantity = 2, Hours = 10 }, default);

            var correctResponse = new GetPriceResponse()
            {
                FinalPrice = 2400,
                TotalPrice = 2400
            };

            Assert.Equal(correctResponse.FinalPrice, response.FinalPrice);
            Assert.Equal(correctResponse.TotalPrice, response.TotalPrice);
            Assert.Equal(correctResponse.Discounts, response.Discounts);
        }

        [Fact]
        public async Task MainHandler_ShouldReturn_PriceResponse_WithDiscount()
        {
            var productId = Guid.NewGuid();
            var rentPrices = RentPriceStub.rentPrice_01;

            var discountQuantityModel = MappingHelper.DiscountInfo[DiscountEnum.Quantity];

            _rentPriceRepositoryMock.Setup(x => x.GetByProductId(productId)).ReturnsAsync(rentPrices);

            _sut = new GetPriceRequestHandler(_rentPriceRepositoryMock.Object);

            var response = await _sut.Handle(new GetPriceRequest() {ProductId = productId.ToString(), Quantity = 5, Hours = 2 }, default);

            var correctResponse = new GetPriceResponse()
            {
                Discounts = new List<Discount>()
                {
                   discountQuantityModel
                },
                FinalPrice = DiscountHelper.ApplyDiscuount(1500, discountQuantityModel.Percentage),
                TotalPrice = 1500
            };

            Assert.Equal(correctResponse.FinalPrice, response.FinalPrice);
            Assert.Equal(correctResponse.TotalPrice, response.TotalPrice);
            Assert.Equal(correctResponse.Discounts, response.Discounts);
        }

        [Fact]
        public async Task MainHandler_ShouldReturn_PriceResponse_LessThanMinimunTime()
        {
            var productId = Guid.NewGuid();
            var rentPrices = RentPriceStub.rentPrice_01;

            var discountQuantityModel = MappingHelper.DiscountInfo[DiscountEnum.Quantity];

            _rentPriceRepositoryMock.Setup(x => x.GetByProductId(productId)).ReturnsAsync(rentPrices);

            _sut = new GetPriceRequestHandler(_rentPriceRepositoryMock.Object);

            var response = await _sut.Handle(new GetPriceRequest() { ProductId = productId.ToString(), Quantity = 1, Hours = TimeSpan.FromMinutes(10).TotalHours }, default);

            var correctResponse = new GetPriceResponse()
            {
                FinalPrice = 100,
                TotalPrice = 100
            };

            Assert.Equal(correctResponse.FinalPrice, response.FinalPrice);
            Assert.Equal(correctResponse.TotalPrice, response.TotalPrice);
            Assert.Equal(correctResponse.Discounts, response.Discounts);
        }

    }
}
