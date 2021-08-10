using Core.Data.Cache.Models;
using System;

namespace Core.Data.Cache.Repositories
{
    public interface ICacheService
    {
        void Delete(string key);

        T Get<T>(string key);

        void Set<T>(string key, T value, TimeSpan? expiry = null);

    }
}
