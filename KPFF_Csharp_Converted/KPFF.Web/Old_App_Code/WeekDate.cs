
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    public class WeekDate
    {
        public int[] WeekIDs = new int[20];
        public int[] Weeks = new int[20];
        public int[] Years = new int[20];
        public string[] MondayDates = new string[20];
        public string[] WeekLabels = new string[20];
        public WeekYear WYFirst;

        public WeekYear WYLast;
        //Public intWeekFirst As Integer = WYFirst.Week
        //Public intYearFirst As Integer = WYFirst.Year
        //Public strDateFirst As String = WYFirst.MondayDate
        //Public intWeekLast As Integer = WYLast.Week
        //Public intYearLast As Integer = WYLast.Year
        //Public strDateLast As String = WYLast.MondayDate


        public WeekDate(WeekYear wyFirst, WeekYear wyLast)
        {
            this.WYFirst = wyFirst;
            this.WYLast = wyLast;

            SetWeekDateArrays();
        }



        private void SetWeekDateArrays()
        {
            SqlConnection conn = new SqlConnection(Configuration.ConnectionString);
            SqlDataReader dr = default(SqlDataReader);
            int intI = 0;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tblWeeks ");
            sql.Append("WHERE WeekNoStartDate ");
            sql.Append("BETWEEN '" + WYFirst.MondayDate + "' ");
            sql.Append("AND '" + WYLast.MondayDate + "' ");
            sql.Append("ORDER BY WeekNoStartDate");

            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                WeekIDs[intI] = dr.GetValueOrDefault<int>("ID");
                Weeks[intI] = dr.GetValueOrDefault<int>("WeekNo");
                Years[intI] = dr.GetValueOrDefault<int>("WeekNoYear");
                MondayDates[intI] = dr.GetValueOrDefault<DateTime>("WeekNoStartDate").ToShortDateString();
                WeekLabels[intI] = dr.GetValueOrDefault<int>("WeekNoMonth") + "/" + dr.GetValueOrDefault<int>("WeekNoDay").ToString("00");
                intI += 1;
            }
            dr.Close();
            dr = null;
            conn = null;
        }
    }
}