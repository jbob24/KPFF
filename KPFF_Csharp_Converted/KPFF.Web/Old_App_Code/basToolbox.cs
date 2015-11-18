
using Microsoft.VisualBasic;
using System;

namespace KPFF.PMP
{
    //public static class basToolbox
    //{
        //public static object Nz(object objValue, object objNullValue)
        //{
        //    if ((objValue == DBNull.Value))
        //    {
        //        return objNullValue;
        //    }
        //    else
        //    {
        //        return objValue;
        //    }
        //}

        //public static object Zz(object objValue, object objZeroValue)
        //{
        //    if ((objValue == ""))
        //    {
        //        return objZeroValue;
        //    }
        //    else
        //    {
        //        return objValue;
        //    }
        //}

        //public static string FormatDate(DateTime dtmDate)
        //{
        //    string strDate = "";
        //    if ((dtmDate == null))
        //    {
        //        return "";
        //    }
        //    strDate = dtmDate.ToString("MM/dd/yyyy");
        //    return strDate;
        //}

        //public static string CleanString(string strTextIn)
        //{
        //    strTextIn = strTextIn.Replace("\'", "\'\'");
        //    return strTextIn;
        //}



        //public static int Nz(string objValue, int objNullValue)
        //{
        //    if (object.ReferenceEquals(objValue, DBNull.Value))
        //    {
        //        return objNullValue;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(objValue);
        //    }
        //}

        //public static int Zz(string objValue, int objZeroValue)
        //{
        //    if (string.IsNullOrEmpty(objValue))
        //    {
        //        return objZeroValue;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(objValue);
        //    }
        //}

        //public static string FormatDate(System.DateTime dtmDate)
        //{
        //    string strDate = "";
        //    if (dtmDate == null)
        //        return "";

        //    strDate = dtmDate.ToString("MM/dd/yyyy");
        //    return strDate;
        //}

        //public static object CleanString(string strTextIn)
        //{
        //    return strTextIn.Replace("'", "''");
        //}
    //}

    //public static class ObjectExtensions
    //{
    //    public static T GetValue<T>(this object obj)
    //    {
    //        return (T)Convert.ChangeType(obj, typeof(T));
    //    }

    //    public static T GetValueOrDefault<T>(this object obj)
    //    {
    //        T value;

    //        if (obj.TryGetValue<T>(out value))
    //        {
    //            return value;
    //        }
    //        else
    //        {
    //            return default(T);
    //        }
    //    }

    //    public static bool TryGetValue<T>(this object obj, out T output)
    //    {
    //        try
    //        {
    //            output = obj.GetValue<T>();
    //            return true;
    //        }

    //        catch (Exception ex)
    //        {
    //            if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
    //            {
    //                output = default(T);
    //                return false;
    //            }

    //            else
    //                throw;
    //        }
    //    }
    //}
}
