using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace KPFF.PMP.Entities
{
    public static class Extensions
    {
        public static T GetValue<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T GetValueOrDefault<T>(this object obj)
        {
            T value;

            if (obj.TryGetValue<T>(out value))
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }

        public static bool TryGetValue<T>(this object obj, out T output)
        {
            try
            {
                output = obj.GetValue<T>();
                return true;
            }

            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                {
                    output = default(T);
                    return false;
                }

                else
                    throw;
            }
        }

        public static string ToJSON(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}