using MediatR;
using Presentation.API.Handlers.Rent.GetPrice.Models;
using System;

namespace Presentation.API.Handlers.Rent.GetPrice
{
    public class GetPriceRequest : IRequest<GetPriceResponse>
    {
        public string ProductId { get; set; }
        public TimeSpan Time { get; set; }
        public int Quantity { get; set; }
    }
    
}
