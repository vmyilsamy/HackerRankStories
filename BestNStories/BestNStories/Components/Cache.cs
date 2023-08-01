using System;
using System.Threading;
using System.Threading.Tasks;
using BestNStories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BestNStories.Components
{
    public class Cache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(object cacheKey)
        {
            var item = _memoryCache.Get<T>(cacheKey);

            return item;
        }

        public async Task Add<T>(object cacheKey, T item)
        {
            try
            {
                await semaphoreSlim.WaitAsync();

                _memoryCache.Set(cacheKey, item, DateTimeOffset.UtcNow.AddHours(24));
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}