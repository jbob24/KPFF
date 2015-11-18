using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KPFF.Data
{
    public class Configuration
    {
        private static string _chartDimensions;

        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public static string ChartDimensions
        {
            get
            {
                if (string.IsNullOrEmpty(_chartDimensions))
                {
                    string height = ConfigurationManager.AppSettings["ChartDimensionsHeight"];
                    string width = ConfigurationManager.AppSettings["CharDimensionsWidth"];

                    if (!string.IsNullOrEmpty(height) && !string.IsNullOrEmpty(width))
                    {
                        _chartDimensions = string.Format("{0}x{1}", width, height);
                    }
                    else
                    {
                        _chartDimensions = "1000x300";
                    }
                }

                return _chartDimensions;
            }
            set { _chartDimensions = value;}
        }
    }
}
