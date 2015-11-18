using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace KPFF.PMP.Entities
{
    public class EngineerGroupMember : DABase
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public EngineerGroupMember(int employeeId, string name, bool isActive)
        {
            this.EmployeeId = employeeId;
            this.Name = name;
            this.IsActive = isActive;
        }

        public static List<EngineerGroupMember> GetByGroupId(int groupId)
        {
            var members = new List<EngineerGroupMember>();

            using (var con = new SqlConnection(Configuration.ConnectionString))
            {
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@GroupID", groupId.ToString());

                var reader = GetDataReaderByStoredProcedure("sp_tblEngineerGroupMembers_GetByGroupId", @params, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        members.Add(new EngineerGroupMember(reader.GetValueOrDefault<int>("EmployeeID"), reader.GetValueOrDefault<string>("EmployeeName"), reader.GetValueOrDefault<bool>("Active")));
                    }
                }

                con.Close();
            }


            return members;
        }

        public static List<EngineerGroupMember> GetActiveByGroupId(int groupId)
        {
            return GetByGroupId(groupId).Where(m => m.IsActive == true).ToList();
        }
    }

    public class EngineerGroup : DABase
    {
        #region properties
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<EngineerGroupMember> Members { get; set; }
        #endregion

        #region contructors
        public EngineerGroup()
            : this(0, string.Empty, string.Empty, false)
        {
        }

        public EngineerGroup(int groupId, string name, string description, bool isactive)
        {
            this.GroupId = groupId;
            this.Name = name;
            this.Description = description;
            this.IsActive = isactive;

            Members = new List<EngineerGroupMember>();
        }
        #endregion

        #region data access methods
        public static List<EngineerGroup> GetAllGroups()
        {
            var groups = new List<EngineerGroup>();

            using (var con = new SqlConnection(Configuration.ConnectionString))
            {
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                var reader = GetDataReaderByStoredProcedure("sp_tblEngineerGroups_GetAll", @params, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        groups.Add(new EngineerGroup(reader.GetValueOrDefault<int>("ID"), reader.GetValueOrDefault<string>("Name"), reader.GetValueOrDefault<string>("Description"), reader.GetValueOrDefault<bool>("Active")));
                    }
                }

                con.Close();
            }
            return groups;
        }

        public static List<EngineerGroup> GetAllActive()
        {
            return GetAllGroups().Where(g => g.IsActive == true).ToList();
        }

        public static List<EngineerGroup> GetAllInactive()
        {
            return GetAllGroups().Where(g => g.IsActive == false).ToList();
        }

        public static EngineerGroup GetById(int groupId)
        {
            var group = new EngineerGroup();

            using (var con = new SqlConnection(Configuration.ConnectionString))
            {
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@GroupID", groupId.ToString());

                var reader = GetDataReaderByStoredProcedure("sp_tblEngineerGroup_GetById", @params, con);

                if (reader.HasRows)
                {
                    reader.Read();
                    group.GroupId = reader.GetValueOrDefault<int>("ID");
                    group.Name = reader.GetValueOrDefault<string>("Name");
                    group.Description = reader.GetValueOrDefault<string>("Description");
                    group.IsActive = reader.GetValueOrDefault<bool>("Active");

                    group.Members = EngineerGroupMember.GetByGroupId(groupId);
                }

                con.Close();
            }


            return group;
        }

        public void Insert()
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@Name", this.Name);
            @params.Add("@Description", this.Description);

            this.GroupId = ExecuteScalar("sp_tblEngineerGroups_Insert", @params).GetValueOrDefault<int>();
        }

        public bool Update()
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ID", this.GroupId.ToString());
            @params.Add("@Name", this.Name);
            @params.Add("@Description", this.Description);
            @params.Add("@Active", this.IsActive ? "1" : "0");

            return ExecuteProcedure("sp_tblEngineerGroups_Update", @params);
        }

        public bool AddMember(EngineerGroupMember member)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@EmployeeID", member.EmployeeId.ToString());
            @params.Add("@GroupID", this.GroupId.ToString());

            return ExecuteProcedure("sp_tblEngineerGroupMembers_Insert", @params);
        }

        public bool UpdateMember(EngineerGroupMember member)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@EmployeeID", member.EmployeeId.ToString());
            @params.Add("@GroupID", this.GroupId.ToString());
            @params.Add("@Active", member.IsActive ? "1" : "0");

            return ExecuteProcedure("sp_tblEngineerGroupMembers_Update", @params);
        }
        #endregion
    }
}