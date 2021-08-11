using Core.Data.Repositories;
using MediatR;
using Presentation.API.Handlers.Rent.GetProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.API.Handlers.Rent.GetProducts
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, List<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsRequestHandler(
            IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));       }

        public async Task<List<ProductResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAll();

            return products?.Select(x => new ProductResponse() { Id = x.Id, Name = x.Name }).ToList();
        }

    }
}
