
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    public class IndexChart : DABase
    {

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private List<IndexSnapShot> _snapShots;
        public List<IndexSnapShot> SnapShots
        {
            get { return _snapShots; }
            set { _snapShots = value; }
        }

        public string GetChartUrl()
        {
            string chartType = "cht=lxy";
            string chartSize = string.Format("chs={0}", Configuration.ChartDimensions);
            // "chs=1000x300"
            string visibleAxes = "chxt=y,x";
            string axisLlabels = GetAxisLabels();
            string legendLabels = GetLegendLabels();
            string seriesColors = GetSeriesColors();
            string chartSeriesColors = string.Format("chco={0}", seriesColors);
            // "chco=3072F3,ff0000,00aaaa"
            string chartLegend = string.Format("chdl={0}", legendLabels);
            string axisRange = "chxr=0,0,200|1,0,100&chds=0,200";
            // need to dynamically set based on min max
            string axisLabels = string.Format("chxl=0:||.2|.4|.6|.8|1.0|1.2|1.4|1.6|1.8|2.0,|1:|0|{0}", axisLlabels);
            // need to set dynamically based on dates in first snapshot
            string lineMarkers = GetLineMarkers();
            string gridLines = "chg=0,10,1,3";

            //chxr=1,0,60&chds=0,6

            StringBuilder chartData = new StringBuilder();
            StringBuilder xData = new StringBuilder();
            StringBuilder yData = new StringBuilder();
            int i = 0;

            foreach (IndexSnapShot ss in SnapShots)
            {
                foreach (IndexItem index in ss.Indexes)
                {
                    int idx = 0;
                    int.TryParse(index.Index, out idx);
                    xData.Append(idx * 100);
                    //yData.Append(index.WeekDate.ToShortDateString())
                    yData.Append((i + 1) * 20);
                    i += 1;
                    if (i < ss.Indexes.Count)
                    {
                        xData.Append(",");
                        yData.Append(",");
                    }
                }
                chartData.Append(yData.ToString()).Append("|").Append(xData.ToString()).Append("|");
                xData = new StringBuilder();
                yData = new StringBuilder();
                i = 0;
            }

            string data = string.Format("chd=t:{0}", chartData.ToString().TrimEnd('|'));



            //https://chart.googleapis.com/chart?cht=lxy&chs=1000x300&chxt=x,y
            //&chd=t:0,10,20,30,40,50,60,80,90,100|119.310344827586,113.862068965517,111.793103448276,109.103448275862,105.275862068966,99.3448275862069,96.7931034482759,92.7931034482759,74.5172413793103,66.7931034482759|0,10,20,30,40,50,60,70,90|113.620689655172,107.689655172414,105.551724137931,103.275862068966,100.448275862069,98.1724137931035,94.7241379310345,90.7241379310345,54.1379310344828
            //&chco=3072F3,ff0000,00aaaa
            //&chls=2,4,1
            //&chm=s,000000,0,-1,5|s,000000,1,-1,5
            //&chdl=Ponies|Unicorns
            //&chdlp=t
            //&chxl=0:|3/7/2011|3/14/2011|3/21/2011|3/28/2011|4/4/2011|4/11/2011|4/18/2011|4/25/2011|5/2/2011|5/9/2011|1:||10|20|30|40|50|60|70|80|90|100|


            //http://code.google.com/apis/chart/docs/chart_params.html#gcharts_cht

            return string.Format("https://chart.googleapis.com/chart?{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}{8}&{9}", chartType, chartSize, visibleAxes, data, chartSeriesColors, chartLegend, axisRange, axisLabels, lineMarkers,
            gridLines);
        }

        private string GetSeriesColors()
        {
            StringBuilder colorStr = new StringBuilder();

            // SnapShots.Count
            for (int i = 1; i <= 10; i++)
            {
                colorStr.Append(GetIndexColor(i));

                //SnapShots.Count Then
                if (i < 10)
                {
                    colorStr.Append(",");
                }
            }

            return colorStr.ToString();
        }

        private string GetAxisLabels()
        {
            StringBuilder axisLabels = new StringBuilder();

            if (SnapShots.Count > 0)
            {
                foreach (IndexItem i in SnapShots[0].Indexes)
                {
                    axisLabels.Append(i.WeekDate.ToShortDateString());
                    axisLabels.Append("|");
                }
            }

            return axisLabels.ToString().TrimEnd(new char[] {'|'});
        }

        public string GetLineMarkers()
        {
            StringBuilder lineMarkers = new StringBuilder();

            lineMarkers.Append("&chm=");

            if (SnapShots.Count > 0)
            {
                int i = 0;
                foreach (IndexSnapShot s in SnapShots)
                {
                    lineMarkers.Append(string.Format("o,000000,{0},-1,3", i));
                    lineMarkers.Append("|");
                    i += 1;
                }
            }

            return lineMarkers.ToString().TrimEnd('|');
        }

        private string GetLegendLabels()
        {
            StringBuilder legendLabels = new StringBuilder();

            if (SnapShots.Count > 0)
            {
                foreach (IndexSnapShot s in SnapShots)
                {
                    legendLabels.Append(s.SnapShotDate.ToShortDateString());
                    legendLabels.Append("|");
                }
            }

            return legendLabels.ToString().TrimEnd(new char[] {'|'});
        }

        private string GetIndexColor(int i)
        {
            switch (i)
            {
                case 1:
                    return "000033";
                case 2:
                    return "ff0000";
                case 3:
                    return "ffff00";
                case 4:
                    return "99ffff";
                case 5:
                    return "009900";
                case 6:
                    return "ff0066";
                case 7:
                    return "0099ff";
                case 8:
                    return "660000";
                case 9:
                    return "66ff66";
                case 10:
                default:
                    return "000000";
            }
        }

        public IndexChart(DateTime startDate)
        {
            SnapShots = new List<IndexSnapShot>();
            this.StartDate = startDate;

            SqlConnection con = new SqlConnection(Configuration.ConnectionString);
            con.Open();

            SqlDataReader dr = GetIndexSnapshots(con, startDate);

            if (dr.HasRows)
            {
                IndexSnapShot snapShot = new IndexSnapShot();
                var snapShotDate = new DateTime();
                var curSnapShotDate = new DateTime();

                while ((dr.Read()))
                {
                    curSnapShotDate = dr.GetValueOrDefault<DateTime>("SnapShotDate");

                    if ((snapShotDate != curSnapShotDate))
                    {
                        snapShotDate = curSnapShotDate;

                        snapShot = new IndexSnapShot();
                        snapShot.SnapShotDate = snapShotDate;
                        SnapShots.Add(snapShot);
                    }

                    IndexItem index = new IndexItem();
                    index.WeekDate = dr.GetValueOrDefault<DateTime>("WeekDate");
                    index.Index = dr.GetValueOrDefault<string>("HoursIndex");

                    snapShot.Indexes.Add(index);
                }
            }

            con.Close();
            dr.Close();
            con = null;
            dr = null;
        }

        public SqlDataReader GetIndexSnapshots(SqlConnection con, DateTime startDate)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@StartDate", startDate.ToString());

            return GetDataReaderByStoredProcedure("sp_GetWeeklyAvailabilityIndexData", @params, con);
        }
    }

    /// <summary>
    /// Represents a collection of indexes for a single snapshot
    /// </summary>
    /// <remarks></remarks>
    public class IndexSnapShot
    {
        private DateTime _snapShotDate;
        public DateTime SnapShotDate
        {
            get { return _snapShotDate; }
            set { _snapShotDate = value; }
        }


        private List<IndexItem> _indexes;
        public List<IndexItem> Indexes
        {
            get { return _indexes; }
            set { _indexes = value; }
        }

        public IndexSnapShot()
        {
            Indexes = new List<IndexItem>();
        }

    }

    /// <summary>
    /// Represents one index
    /// </summary>
    /// <remarks></remarks>
    public class IndexItem
    {
        private DateTime _weekDate;
        public DateTime WeekDate
        {
            get { return _weekDate; }
            set { _weekDate = value; }
        }

        private string _index;
        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}