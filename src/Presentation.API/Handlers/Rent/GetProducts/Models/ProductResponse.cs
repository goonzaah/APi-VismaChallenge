using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.API.Handlers.Rent.GetProducts.Models
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
