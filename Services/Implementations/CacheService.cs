using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    #region Part 3 Caching Repository & Caching Service
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedValueAsync(string key)
       => await cacheRepository.GetAsync(key);



        public Task SetCacheValueAsync(string key, object value, TimeSpan? duration)
        => cacheRepository.SetAsync(key, value, duration);
    } 
    #endregion
}
