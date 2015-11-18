using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Business;

namespace KPFF.Test
{
    public class TestCache : ICache
    {
        private Dictionary<string, object> _cache;

        public object GetItem(string key)
        {
            if (_cache == null || !_cache.ContainsKey(key))
            {
                return null;
            }

            return _cache[key];
        }

        public void InsertItem(string key, object value)
        {
            if (_cache == null)
            {
                _cache = new Dictionary<string, object>();
            }

            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
            }
            else
            {
                _cache.Add(key, value);
            }
        }
    }
}
