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
using Xunit;

namespace Test.API
{
    
    public class GetPriceRequestHandlerTests
    {
        private readonly Mock<IRentPriceRepository> _rentPriceRepositoryMock = new Mock<IRentPriceRepository>();
        private GetPriceRequestHandler _sut;

        public GetPriceRequestHandlerTests()
        {
            
        }

        [Fact]
        public async Task MainHandler_ShouldReturnPriceResponse()
        {
            var productId = Guid.NewGuid();
            var rentPrices = new List<RentPrice>()
            {
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(0,30,0),
                    Value = 100
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(1,0,0),
                    Value = 150
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(5,0,0),
                    Value = 600
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(24,0,0),
                    Value = 2000
                }
            }.AsEnumerable();

            var discountQuantityModel = MappingHelper.DiscountInfo[DiscountEnum.Quantity];

            _rentPriceRepositoryMock.Setup(x => x.GetByProductId(productId)).Returns(Task.FromResult(rentPrices));

            _sut = new GetPriceRequestHandler(_rentPriceRepositoryMock.Object);

            var response = await _sut.Handle(new GetPriceRequest() {ProductId = Guid.NewGuid().ToString(), Quantity = 5, Time = TimeSpan.FromHours(2) }, default);

            var correctResponse = new GetPriceResponse()
            {
                Discounts = new List<Discount>()
                {
                   discountQuantityModel
                },
                FinalPrice = DiscountHelper.ApplyDiscuount(300, discountQuantityModel.Percentage),
                TotalPrice = 300
            };

            Assert.Equal<GetPriceResponse>(response, correctResponse);
        }

        [Fact]
        public async Task MainHandler_Invalid_ProductIdGuid()
        {
            var productId = Guid.NewGuid();
            var rentPrices = new List<RentPrice>()
            {
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(0,30,0),
                    Value = 100
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(1,0,0),
                    Value = 150
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(5,0,0),
                    Value = 600
                },
                new RentPrice()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Time = new TimeSpan(24,0,0),
                    Value = 2000
                }
            }.AsEnumerable();

            var discountQuantityModel = MappingHelper.DiscountInfo[DiscountEnum.Quantity];

            _rentPriceRepositoryMock.Setup(x => x.GetByProductId(productId)).Returns(Task.FromResult(rentPrices));

            var response = await _sut.Handle(new GetPriceRequest() { ProductId = "invalid GUID :(", Quantity = 5, Time = TimeSpan.FromHours(2) }, default);

            var correctResponse = new GetPriceResponse()
            {
                Discounts = new List<Discount>()
                {
                   discountQuantityModel
                },
                FinalPrice = DiscountHelper.ApplyDiscuount(300, discountQuantityModel.Percentage),
                TotalPrice = 300
            };

            Assert.Equal<GetPriceResponse>(response, correctResponse);
        }


    }
}
