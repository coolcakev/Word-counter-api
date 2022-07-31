using Microsoft.Extensions.Caching.Memory;

namespace Word_counter_api.Helpers
{
    public static class CacheHelper
    {
        public static T GetItemFromCacheMemory<T>(string cacheKey, IMemoryCache cache)
        {
            if (cache.TryGetValue(cacheKey, out T item))
                return item;

            return default(T);
        }
        public static void SetItemInCacheMemory<T>(T item, string cacheKey, IMemoryCache cache)
        {
            cache.Set(cacheKey, item);
        }
        public static void Remove(string cacheKey, IMemoryCache cache)
        {
            cache.Remove(cacheKey);
        }
    }
}
