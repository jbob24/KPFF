
using Microsoft.VisualBasic;
using System.Configuration;

public class Configuration
{

	public static string ConnectionString = ConfigurationManager.ConnectionStrings["KPFFSchedulingConnectionString"].ConnectionString;// ConfigurationManager.AppSettings["ConnectionString"];
	private static string _chartDimensions;
	public static string ChartDimensions {
		get {
			if (string.IsNullOrEmpty(_chartDimensions)) {
				string height = ConfigurationManager.AppSettings["ChartDimensionsHeight"];
				string width = ConfigurationManager.AppSettings["ChartDimensionsWidth"];

				if (!string.IsNullOrEmpty(height) & !string.IsNullOrEmpty(width)) {
					_chartDimensions = string.Format("{0}x{1}", width, height);
				} else {
					_chartDimensions = "1000x300";
				}
			}
			return _chartDimensions;
		}
		set { _chartDimensions = value; }
	}


	public static string ChartHeight = ConfigurationManager.AppSettings[""];
	public static string ChartWidth = ConfigurationManager.AppSettings[""];
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik, @toddanglin
//Facebook: facebook.com/telerik
//=======================================================
