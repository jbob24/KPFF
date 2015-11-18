
using Microsoft.VisualBasic;
using System.Data;
using System.Text;
using System;

namespace KPFF.PMP
{
    public class GridControlHelpers
    {

        public static decimal GetFieldValue(DataRow row, string field)
        {
            try
            {
                return row.Field<decimal>(field);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static double GetFieldValueAsDouble(DataRow row, string field)
        {
            try
            {
                return row.Field<double>(field);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static DateTime ConvertToDateValue(string dateStr)
        {
            DateTime theDate = default(DateTime);
            if (DateTime.TryParse(dateStr, out theDate))
            {
                return theDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static string GetCellStyle(decimal hours, decimal empHoursPerWeek)
        {
            //If hours > 50 Then
            //    Return "CellOver25Style"
            //End If

            //If hours >= 40 And hours <= 43 Then
            //    Return "CellScheduledStyle"
            //End If

            //If hours >= 44 And hours <= 50 Then
            //    Return "Cell1025Style"
            //End If

            if ((empHoursPerWeek <= 0))
            {
                empHoursPerWeek = 40;
            }

            decimal hours25 = empHoursPerWeek * 1.25m;
            decimal hours10 = empHoursPerWeek * 1.1m;

            if (hours > (hours25))
            {
                return "CellOver25Style";
            }

            if (hours >= (empHoursPerWeek) & hours < (hours10))
            {
                return "CellScheduledStyle";
            }

            if (hours >= (hours10) & hours <= (hours25))
            {
                return "Cell1025Style";
            }

            return "CellUnderSyle";
        }

        public static string GetCellStyle(DateTime dateValue)
        {
            var ts = DateTime.Today - dateValue;
            var days = ts.Days;

            if (days <= 7)
            {
                return "Cell07Style";
            }

            if (days >= 8 & days <= 30)
            {
                return "Cell830Style";
            }

            if (days >= 31 & days <= 60)
            {
                return "Cell3160Style";
            }

            return "Cell60Style";
        }

        public static string GetCellStyle(string text, decimal empHoursPerWeek)
        {
            decimal value = 0;
            decimal.TryParse(text, out value);

            return GetCellStyle(value, empHoursPerWeek);
        }

        public static string GetWeekHeader(int weekNum, WeekDate weekdate, clsSchedule schedule, string curWeekClass)
        {

            if (weekdate == null)
            {
                return "n/a";
            }

            StringBuilder header = new StringBuilder();

            header.Append("<div");

            string weekLabel = weekdate.WeekLabels[weekNum];

            if ((weekLabel == schedule.GetCurrentWeekLabel()))
            {
                header.Append(" class='" + curWeekClass + "'>");
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