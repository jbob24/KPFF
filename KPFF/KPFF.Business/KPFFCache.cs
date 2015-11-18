using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace KPFF.Business
{
    public class KPFFCache : ICache
    {
        private static KPFFCache _current;

        private KPFFCache() { }

        public static KPFFCache Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new KPFFCache();
                }

                return _current;
            }
        }

        public object GetItem(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public void InsertItem(string key, object value)
        {
            HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(2));
        }
    }
}
