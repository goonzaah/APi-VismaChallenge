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


    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public void Delete(string key)
        {
            cache.Remove(key);
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan? expriationTime = null)
        {
            if (CacheKeyCompatibility.AvailableKey(key))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions();

                if (expriationTime != null)
                    cacheEntryOptions.SetAbsoluteExpiration((TimeSpan)expriationTime);

                cache.Set(key, value, cacheEntryOptions);
            }
        }
    }
}
