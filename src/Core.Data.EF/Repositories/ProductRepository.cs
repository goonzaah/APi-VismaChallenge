using Core.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Data.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext dataContext;

        public ProductRepository(DataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }


        public IEnumerable<Entities.Product> GetAll()
         => dataContext.Products;
        
    }
}
