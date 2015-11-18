using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPFF.Entities;

namespace KPFF.Web.Model
{
    public class UserSession
    {
        #region constants
        public const string USER_SESSION_KEY = "KPFFSessionKey";
        #endregion

        #region private variables
        private static UserSession _current;
        private Employee _employee;
        #endregion

        #region public properties
        public int EmployeeId { get; set; }
        public Week FirstWeek { get; set; }
        public Week LastWeek { get; set; }

        public UserSession Current
        {
            get
            {
                if (_current == null)
                {
                    var sessionObj = HttpContext.Current.Session[USER_SESSION_KEY];

                    if (sessionObj != null && sessionObj is UserSession)
                    {
                        _current = sessionObj as UserSession;
                    }
                    else
                    {
                        _current = new UserSession();
                        HttpContext.Current.Session[USER_SESSION_KEY] = _current;
                    }
                }

                return _current;
            }
            
            set
            {
                if (value is UserSession)
                {
                    HttpContext.Current.Session[USER_SESSION_KEY] = value;
                }
            }
        }

        [NonSerialized]
        public Employee Employee
        {
            get
            {
                if (_employee == null && EmployeeId > 0)
                {
                    _employee = KPFF.Business.Employee.GetById(EmployeeId);
                }

                return _employee;
            }
        }
        #endregion

        #region constructors
        private UserSession() { }
        #endregion
    }
}