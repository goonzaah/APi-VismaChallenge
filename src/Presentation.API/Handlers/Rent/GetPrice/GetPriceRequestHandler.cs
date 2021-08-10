using Core.Data.Helpers;
using Core.Data.Repositories;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Presentation.API.Handlers.Rent.GetPrice.Models;
using Presentation.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.API.Handlers.Rent.GetPrice
{
    public class GetPriceRequestHandler : IRequestHandler<GetPriceRequest, GetPriceResponse>
    {
        private readonly IRentPriceRepository rentPriceRepository;

        public GetPriceRequestHandler(
            IRentPriceRepository rentPriceRepository)
        {
            this.rentPriceRepository = rentPriceRepository ?? throw new ArgumentNullException(nameof(rentPriceRepository));
        }

        public async Task<GetPriceResponse> Handle(GetPriceRequest request, CancellationToken cancellationToken)
        {
            var rentPrices = await rentPriceRepository.GetByProductId(Guid.Parse(request.ProductId));

            if (!rentPrices.Any()) throw new InvalidProductException();

            var unitPrice = GetUnitPrice(rentPrices, request.Time);

            return GetPriceResponse(request, rentPrices, unitPrice);
        }


        private double GetUnitPrice(IEnumerable<RentPrice> rentPrices, TimeSpan timeRequested)
        {
            var rentPricesDescendingByTime = rentPrices.OrderBy(x => x.Time);

            foreach (var rentInfo in rentPricesDescendingByTime)
            {
                if (rentInfo.Time < timeRequested)
                    return Math.Ceiling(rentInfo.Time / timeRequested) * rentInfo.Value;
            }

            return rentPricesDescendingByTime.FirstOrDefault().Value;
        }

        private GetPriceResponse GetPriceResponse(GetPriceRequest request, IEnumerable<RentPrice> rentPrices, double unitPrice)
        {
            var response = new GetPriceResponse();

            var finalPrice = unitPrice * request.Quantity;

            response.FinalPrice = response.TotalPrice = finalPrice;

            response = QuantityDiscount(response, request.Quantity);

            return response;
        }

        private GetPriceResponse QuantityDiscount(GetPriceResponse response, int quantity)
        {
            return quantity > 3 ? (GetPriceResponse)DiscountHelper.ApplyDiscuountToResponse(response, DiscountEnum.Quantity) : response;
        }
    }
}
