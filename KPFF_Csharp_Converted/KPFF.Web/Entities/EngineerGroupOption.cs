using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPFF.PMP.Entities
{
    public class EngineerGroupOption
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public EngineerGroupOption(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public static List<EngineerGroupOption> GetOptions()
        {
            var groups = EngineerGroup.GetAllActive().OrderBy(g => g.Name);
            var options = new List<EngineerGroupOption>();

            foreach(var group in groups)
            {
                options.Add(new EngineerGroupOption(group.Name, group.GroupId.ToString()));
            }

            return options;
        }
    }
}