using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class Week : EntityBase
    {
        public int YearWeekNumber { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateTime MondayDate { get; set; }
        public string WeekLabel 
        { 
            get
            {
                return GetWeekLabel(this);
            }
        }

        public Week() { }

        public Week(int id, int yearWeekNumber, int year, int month, int day, DateTime mondayDate)
        {
            this.Id = id;
            this.YearWeekNumber = yearWeekNumber;
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.MondayDate = mondayDate;
        }

        public static string GetWeekLabel(Week week)
        {
            return week.Day > 0 && week.Month > 0
                ? string.Format("{0}/{1}", week.Month.ToString(), week.Day.ToString("00"))
                : string.Empty;
        }
    }

    public class WeekList : List<Week> { }
}
