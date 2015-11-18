using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace KPFF.Data.Entities
{
    [Serializable]
    public class User
    {
        public string UserLevel { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName
        {
            get { return string.Format("{0} {1}", EmployeeFirst, EmployeeLast); }
        }

        public string EmployeeFirst { get; set; }
        public string EmployeeLast { get; set; }
        public int HoursPerWeek { get; set; }
        public bool HasPMFiscalSummaryAccess { get; set; }
        public bool HasPICFiscalSummaryAccess { get; set; }
        

        public static User GetByUsernamePassword(string userName, string password)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@UserName", userName);
            parms.Add("@Password", password);
            var con = new SqlConnection(Configuration.ConnectionString);
            var da = new DABase();
            var dr = da.GetDataReaderByStoredProcedure("sp_GetUserByUserNamePassword", parms, con);

            if (dr.HasRows)
            {
                dr.Read();

                return new User()
                {
                    EmployeeId = dr.GetValueOrDefault<int>("EmployeeID"),
                    UserLevel = dr.GetValueOrDefault<string>("UserLevel"),
                    EmployeeFirst = dr.GetValueOrDefault<string>("EmployeeFirst"),
                    EmployeeLast = dr.GetValueOrDefault<string>("EmployeeLast"),
                    HoursPerWeek = dr.GetValueOrDefault<int>("HoursPerWeek"),
                    HasPICFiscalSummaryAccess = dr.GetValueOrDefault<bool>("PICFiscalSummary"),
                    HasPMFiscalSummaryAccess = dr.GetValueOrDefault<bool>("PMFiscalSummary"),
                };
            }

            return null;
        }
    }
}
