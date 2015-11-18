using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPFF.PMP.Web.Entities
{
    public static class ExtensionMethods
    {
        public static DateTime GetMondayDate(this DateTime date)
        {
            return date.AddDays(1 - (int)date.DayOfWeek);
        }
    }
}