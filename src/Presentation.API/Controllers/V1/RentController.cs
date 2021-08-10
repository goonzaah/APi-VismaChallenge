using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Handlers.Rent.GetPrice;
using Presentation.API.Handlers.Rent.GetPrice.Models;
using Presentation.API.Handlers.Rent.GetProducts;
using Presentation.API.Handlers.Rent.GetProducts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.API.Controllers.V1
{
    [Route("v{version:apiVersion}/rent")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RentController : Controller
    {
        private readonly IMediator mediator;

        public RentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("getProducts")]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var result = await mediator.Send(new GetProductsRequest());
            return Ok(result);
        }

        [HttpPost("getPrice")]
        public async Task<ActionResult<GetPriceResponse>> PostTopSecretSplit([FromBody] GetPriceRequest request)
        {
            var result = await mediator.Send(request);
            
            return Ok(result);
            
        }

    }
}

