using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Entities;
using System.Data.SqlClient;

namespace KPFF.Data
{
    public class ScheduleData
    {
        public static ScheduleItemList GetProjectSchedules(ProjectList projects, DateTime startDate, DateTime endDate)
        {
            ScheduleItemList schedules = null;

            if (projects != null && projects.Count > 0)
            {
                var parms = new Dictionary<string, string>();
                parms.Add("@ProjectList", string.Join(",", (from p in projects select p.Id).ToArray()));
                parms.Add("@StartDate", startDate.ToShortDateString());
                parms.Add("@EndDate", endDate.ToShortDateString());

                using (var db = new DataBase("sp_GetProjectSchedules_NEW", parms))
                {
                    var reader = db.ExecuteReader();

                    if (reader.HasRows)
                    {
                        schedules = ConvertReaderDataToScheduleItemList(reader);
                    }
                }
            }
            return schedules;
        }

        private static ScheduleItemList ConvertReaderDataToScheduleItemList(SqlDataReader reader)
        {
            ScheduleItemList schedules = null;

            if (reader.HasRows)
            {
                schedules = new ScheduleItemList();
                while (reader.Read())
                {
                    schedules.Add(new ScheduleItem(reader.GetValueOrDefault<int>("ID"), 
                                                    reader.GetValueOrDefault<int>("ProjectID"), 
                                                    reader.GetValueOrDefault<int>("EmployeeID"), 
                                                    reader.GetValueOrDefault<int>("WeekID"), 
                                                    reader.GetValueOrDefault<decimal>("Hours")));
                }
            }

            return schedules;

        }
    }
}
