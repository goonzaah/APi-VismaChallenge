using Core.Data.Cache;
using Core.Data.Cache.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Cache.Mappings
{
    public static class CacheKeyCompatibility
    {
        private static Dictionary<Type, string> _keyNames
            => new Dictionary<Type, string>
                {
                    { typeof(SatelliteCacheModel), "satelliteList" },
                };

        private static Dictionary<(Type, string), string> _columnName
            => new Dictionary<(Type, string), string>();

        public static string GetCacheKey(Type type)
            => _keyNames.ContainsKey(type) ? _keyNames[type] : type.Name;

        public static bool AvailableKey(string key)
            => _keyNames.All(x=>x.Value != key);
    }
}
