using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using KPFF.Entities;

namespace KPFF.Web
{
    public class GridControlHelpers
    {
        public static Decimal GetFieldValue(DataRow row, string field)
        {
            try
            {
                var fieldValue = row.Field<object>(field);

                if (fieldValue == null)
                {
                    return 0;
                }

                decimal value = 0;

                decimal.TryParse(fieldValue.ToString(), out value);
                return value;
            }
            catch
            {
                return 0;
            }
        }

        public static double GetFieldValueAsDouble(DataRow row, string field)
        {
            try
            {
                var fieldValue = row.Field<string>(field);
                double value = 0;

                double.TryParse(fieldValue, out value);
                return value;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime ConvertToDateValue(string dateStr)
        {
            DateTime theDate;
            if (DateTime.TryParse(dateStr, out theDate))
            {
                return theDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static string GetCellStyle(Decimal hours, Decimal empHoursPerWeek)
        {
            // If hours > 50 Then
            //     Return "CellOver25Style"
            // End If
            // If hours >= 40 And hours <= 43 Then
            //     Return "CellScheduledStyle"
            // End If
            // If hours >= 44 And hours <= 50 Then
            //     Return "Cell1025Style"
            // End If
            if ((empHoursPerWeek <= 0))
            {
                empHoursPerWeek = 40;
            }
            Decimal hours25 = (empHoursPerWeek * 1.25m);
            Decimal hours10 = (empHoursPerWeek * 1.1m);
            if ((hours > hours25))
            {
                return "CellOver25Style";
            }
            if (((hours >= empHoursPerWeek)
                        && (hours < hours10)))
            {
                return "CellScheduledStyle";
            }
            if (((hours >= hours10)
                        && (hours <= hours25)))
            {
                return "Cell1025Style";
            }
            return "CellUnderSyle";
        }

        public static string GetCellStyle(DateTime dateValue)
        {
            var ts = (DateTime.Today - dateValue);
            var days = ts.Days;
            if (days <= 7)
            {
                return "Cell07Style";
            }
            if (days >= 8 && days <= 30)
            {
                return "Cell830Style";
            }
            if (days >= 31 && days <= 60)
            {
                return "Cell3160Style";
            }
            return "Cell60Style";
        }

        public static string GetCellStyle(string text, Decimal empHoursPerWeek)
        {
            Decimal value = 0;
            Decimal.TryParse(text, out value);
            return GetCellStyle(value, empHoursPerWeek);
        }

        public static string GetWeekHeader(int weekNum, List<Week> weeks, Week currentWeek, string currentWeekClass)
        {
            if ((weeks == null))
            {
                return "n/a";
            }
            StringBuilder header = new StringBuilder();
            header.Append("<div");
            string weekLabel = weeks[weekNum].WeekLabel;
            if (weekLabel == currentWeek.WeekLabel)
            {
                header.Append(" class=\'" + currentWeekClass + "\'>");
            }
            else
            {
                header.Append(">");
            }
            header.Append(weekLabel);
            header.Append("</div>");
            return header.ToString();
        }
    }
}