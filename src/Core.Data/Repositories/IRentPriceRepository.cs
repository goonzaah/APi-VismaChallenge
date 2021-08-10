using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public interface IRentPriceRepository
    {
        IEnumerable<RentPrice> GetAll();
        Task<IEnumerable<RentPrice>> GetByProductId(Guid id);
    }
}