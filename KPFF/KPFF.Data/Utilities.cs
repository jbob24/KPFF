using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Data
{
    public class Utilities
    {
        public static object Nz(object objValue, object objNullValue)
        {
            if (objValue == DBNull.Value)
            {
                return objNullValue;
            }
            else
            {
                return objValue;
            }
        }

        public static object Zz(object objValue, object objZeroValue)
        {
            if (objValue == string.Empty)
            {
                return objZeroValue;
            }
            else
            {
                return objValue;
            }
        }

        public static string FormatDate(DateTime dtmDate)
        {
            return dtmDate.ToString("MM/dd/yyyy");
        }

        public static string CleanString(string strTextIn)
        {
            return strTextIn.Replace("'", "''");
        }
    }
}
