using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KPFF.PMP.Entities;

namespace PMP.Test
{
    [TestClass]
    public class EngineerGroupsTest
    {
        [TestMethod]
        public void GetAllGroups()
        {
            var groups = KPFF.PMP.Entities.EngineerGroup.GetAllGroups();
            Assert.AreNotEqual(null, groups, "groups is null");
            Assert.AreNotEqual(0, groups.Count(), "groups count is 0");
        }

        [TestMethod]
        public void GetActiveGroups()
        {
            var active = KPFF.PMP.Entities.EngineerGroup.GetAllActive();
            Assert.AreNotEqual(null, active, "active is null");
            Assert.AreNotEqual(0, active.Count(), "active count is 0");
        }

        [TestMethod]
        public void GetGroupByID()
        {
            var group = KPFF.PMP.Entities.EngineerGroup.GetById(1);
            Assert.AreNotEqual(null, group, "group is null");
        }

        [TestMethod]
        public void InsertGroup()
        {
            var group = new EngineerGroup(0, "My 2nd Group", "This is another test group", true);
            group.Insert();

            Assert.AreNotEqual(0, group.GroupId, "the groupid is still 0");
        }

        [TestMethod]
        public void UpdateGroup()
        {
            var group = EngineerGroup.GetById(2);

            group.IsActive = false;
            group.Update();

            group = EngineerGroup.GetById(2);

            Assert.AreEqual(false, group.IsActive, "the group is still active");
        }

        [TestMethod]
        public void AddMember()
        {
            var group = EngineerGroup.GetById(4);
            var newMember = new EngineerGroupMember(21, "Mark", true);

            group.AddMember(newMember);

            var groupMembers = EngineerGroupMember.GetByGroupId(4);

            Assert.AreNotEqual(0, groupMembers.Count());
            
        }

        [TestMethod]
        public void DeactivateMember()
        {
            var group = EngineerGroup.GetById(4);
            var members = EngineerGroupMember.GetByGroupId(4);

            var mark = members.FirstOrDefault(m => m.EmployeeId == 21);

            mark.IsActive = false;
            group.UpdateMember(mark);

            var active = EngineerGroupMember.GetActiveByGroupId(4);

            Assert.AreEqual(0, active.Count());
        }
    }
}
