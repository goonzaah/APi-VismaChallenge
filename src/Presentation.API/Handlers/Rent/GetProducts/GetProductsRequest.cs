using MediatR;
using Presentation.API.Handlers.Rent.GetProducts.Models;
using System.Collections.Generic;

namespace Presentation.API.Handlers.Rent.GetProducts
{
    public class GetProductsRequest : IRequest<List<ProductResponse>>
    {
    }
    
}
