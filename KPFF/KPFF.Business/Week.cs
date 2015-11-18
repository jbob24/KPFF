using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Entities;
using System.Web;
using System.Web.Caching;
using KPFF.Data;

namespace KPFF.Business
{
    public class Week
    {
        private const string WEEK_DATA_KEY = "WeekData";

        public static WeekList GetWeeks(ICache cache)
        {
            var weekDataCache = cache.GetItem(WEEK_DATA_KEY); // HttpContext.Current.Cache[WEEK_DATA_KEY];
            WeekList weeksData = null;

            if (weekDataCache != null)
            {
                weeksData = (WeekList)weekDataCache;
            }

            if (weeksData == null)
            {
                weeksData = WeekData.GetWeeksData();
                cache.InsertItem(WEEK_DATA_KEY, weeksData);
                //HttpContext.Current.Cache.Insert(WEEK_DATA_KEY, weeksData, null, KPFFCache.NoAbsoluteExpiration, TimeSpan.FromMinutes(2));
            }

            return weeksData;
        }
    }
}
