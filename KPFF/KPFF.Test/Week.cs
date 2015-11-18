using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KPFF.Test
{
    [TestClass]
    public class Week
    {
        [TestMethod]
        public void GetWeekData()
        {
            var cache = new TestCache();
            var weeks = KPFF.Business.Week.GetWeeks(cache);

            Assert.IsNotNull(weeks, "Weeks is null");
            Assert.AreEqual(677, weeks.Count(), "Weeks count is wrong");
        }
    }
}
