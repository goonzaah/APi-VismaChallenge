using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public interface IRentPriceRepository
    {
        Task<List<RentPrice>> GetAll();
        Task<List<RentPrice>> GetByProductId(Guid id);
    }
}