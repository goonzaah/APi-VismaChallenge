using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data.Cache.Models;

namespace Core.Data.Cache.Repositories
{
    public interface ISateliteCacheService
    {
        void ResetSatelitesCache();
        IList<SatelliteCacheModel> GetSatelites();
        Task<IList<SatelliteCacheModel>> GetSatelitesAsync();
        Task<SatelliteCacheModel> GetSateliteByNameAsync(string name);
    }
}
