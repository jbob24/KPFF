using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace KPFF.Data
{
    public static class ExtensionMethods
    {
        public static T GetValue<T>(this SqlDataReader reader, string name)
        {
            return (T)Convert.ChangeType(reader[name], typeof(T));
        }

        public static T GetValueOrDefault<T>(this SqlDataReader reader, string name)
        {
            T value;

            if (reader.TryGetValue<T>(name, out value))
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }

        public static bool TryGetValue<T>(this SqlDataReader reader, string name, out T output)
        {
            try
            {
                output = reader.GetValue<T>(name);
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
    }
}
