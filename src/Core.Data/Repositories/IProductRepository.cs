using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
    }
}