using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Entities;
using System.Data.SqlClient;
using System.Web;
using System.Web.Caching;
using KPFF.Data;

namespace KPFF.Data
{
    public class WeekData
    {
        public static WeekList GetWeeksData()
        {
            WeekList weeks = null;

            using (var db = new DataBase("sp_GetWeekData", null))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    weeks = new WeekList();

                    while (reader.Read())
                    {
                        var week = new Week();

                        week.Id = reader.GetValueOrDefault<int>("ID");
                        week.YearWeekNumber = reader.GetValueOrDefault<int>("WeekNo");
                        week.Year = reader.GetValueOrDefault<int>("WeekNoYear");
                        week.Month = reader.GetValueOrDefault<int>("WeekNoMonth");
                        week.Day = reader.GetValueOrDefault<int>("WeekNoDay");
                        week.MondayDate = reader.GetValueOrDefault<DateTime>("WeekNoStartDate");

                        weeks.Add(week);
                    }
                }
            }

            return weeks;
        }
    }
}
