using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KPFF.Data.Entities;

namespace KPFF.Data
{
    public class WeekDate
    {
        public List<int> WeekIds { get; set; }
        public List<int> Weeks { get; set; }
        public List<int> Years { get; set; }
        public List<DateTime> MondayDates { get; set; }
        public List<string> WeekLabels { get; set; }
        public WeekYear WYFirst { get; set; }
        public WeekYear WYLast { get; set; }

        public WeekDate(WeekYear wyFirst, WeekYear wyLast)
        {
            WYFirst = wyFirst;
            WYLast = wyLast;

            WeekIds = new List<int>();
            Weeks = new List<int>();
            Years = new List<int>();
            MondayDates = new List<DateTime>();
            WeekLabels = new List<string>();

            SetWeekDateArrays();
        }

        private void SetWeekDateArrays()
        {
            SqlConnection conn = new SqlConnection(Configuration.ConnectionString);
            SqlDataReader dr;
            int intI = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM tblWeeks ");
            sql.Append("WHERE WeekNoStartDate ");
            sql.Append(("BETWEEN \'"
                            + (WYFirst.MondayDate + "\' ")));
            sql.Append(("AND \'"
                            + (WYLast.MondayDate + "\' ")));
            sql.Append("ORDER BY WeekNoStartDate");
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                var weekId = 0;
                if (int.TryParse(dr["ID"].ToString(), out weekId) && weekId > 0)
                {
                    WeekIds.Add(weekId);
                    Weeks.Add(dr.GetValueOrDefault<int>("WeekNo"));
                    Years.Add(dr.GetValueOrDefault<int>("WeekNoYear"));
                    MondayDates.Add(dr.GetValueOrDefault<DateTime>("WeekNoStartDate"));
                    WeekLabels.Add(string.Format("{0}/{1}", dr.GetValueOrDefault<int>("WeekNoMonth").ToString(), dr.GetValueOrDefault<int>("WeekNoDay").ToString().PadLeft(2,'0')));
                }
            }
            dr.Close();
            dr = null;
            conn = null;
        }
    }
}
