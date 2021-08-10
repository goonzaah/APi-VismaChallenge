using Core.Data.Models;
using System.Collections.Generic;

namespace Presentation.API.Shared.Models
{
    public class PriceResponse
    {
        public double TotalPrice { get; set; }
        public double FinalPrice { get; set; }
        public List<Discount> Discounts { get; set; }
    }
    
}
