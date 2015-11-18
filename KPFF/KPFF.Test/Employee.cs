using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KPFF.Test
{
    [TestClass]
    public class Employee
    {
        [TestMethod]
        public void GetMark()
        {
            var employee = KPFF.Business.Employee.GetById(21);
            Assert.AreEqual("Hershberg", employee.LastName);
        }

        [TestMethod]
        public void GetEmployeeProjects()
        {
            var projects = KPFF.Business.Employee.GetEmployeeProjects(21);

            Assert.IsNotNull(projects);
        }
    }
}
