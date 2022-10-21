using ClientDocumentation.Web.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace ClientDocumentation.Web.Business.Services
{
    public class CachingService
    {
        ObjectCache cache = MemoryCache.Default;
        
        public CacheItemPolicy CreatePolicy(double timeSpan) 
        {
            CacheItemPolicy policy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(timeSpan)
            };
            return policy;
        }
        public T GetCache<T>(string cacheKey) where T : class
        {
            if (!cache.Contains(cacheKey)) 
            {
                return null;
            }
            if (cache[cacheKey] is T)
                return (T)cache[cacheKey];

            return null;
        }

        public void SetOrUpdateCache(string cacheKey, object cacheItem) 
        {
            CacheItemPolicy policy = CreatePolicy(10.0);
            if(string.IsNullOrEmpty(cacheKey))  
                return;

            if (cache[cacheKey] != null) 
            {
                cache.Set(cacheKey, cacheItem, policy);
                return;
            }
            cache.Add(cacheKey, cacheItem, policy);
        }
        public void RemoveCache(string cacheKey) 
        { 
            if(cache.Contains(cacheKey))
                cache.Remove(cacheKey);
        }

        //remove
        //update
    }
}