using Core.Data.Repositories;
using Core.Data.Cache.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared.CacheServices;
using Core.Data.Cache.Models;
using Core.Data.Cache.Mappings;

namespace Core.Data.Cache
{

    public class SateliteCacheService : BaseListCacheService, ISateliteCacheService
    {
        private readonly string cacheKey = CacheKeyCompatibility.GetCacheKey(typeof(SateliteCacheService));
        private readonly TimeSpan expirationTime;
        private readonly ISatelliteRepository sateliteRepository;

        public SateliteCacheService(IMemoryCache cache, TimeSpan expirationTime, ISatelliteRepository sateliteRepository)
            : base(cache)
        {
            this.expirationTime = expirationTime;
            this.sateliteRepository = sateliteRepository ?? throw new ArgumentNullException(nameof(sateliteRepository));
        }

        public void ResetSatelitesCache()
            => ResetCacheKey(cacheKey);

        public IList<SatelliteCacheModel> GetSatelites()
            => GetAllElementsCache<IList<SatelliteCacheModel>>(cacheKey, expirationTime, GetSatelitesCacheModels);

        public async Task<IList<SatelliteCacheModel>> GetSatelitesAsync()
            => await GetAllElementsCacheAsync<IList<SatelliteCacheModel>>(cacheKey, expirationTime, GetSatelitesCacheModels);

        public async Task<SatelliteCacheModel> GetSateliteByNameAsync(string name)
            => (await GetSatelitesAsync()).Where(x => x.Name == name).FirstOrDefault();

        private IList<SatelliteCacheModel> GetSatelitesCacheModels()
        {
            var satelites = sateliteRepository.GetAllSatellites();
            return satelites.Select(x => new SatelliteCacheModel()
            {
                Id = x.Id,
                Name = x.Name,
                X = x.X,
                Y = x.Y
            }).ToList();
        }
    }
}
