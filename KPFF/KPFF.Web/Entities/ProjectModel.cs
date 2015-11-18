using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPFF.Web.Model
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public decimal ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string PIC { get; set; }
        public string PM { get; set; }
        public List<ScheduleWeek> Schedule { get; set; }

        public ProjectModel(int projectId, decimal projectNumber, string projectName, string pic, string pm)
        {
            this.ProjectId = projectId;
            this.ProjectNumber = projectNumber;
            this.ProjectName = projectName;
            this.PIC = pic;
            this.PM = pm;
        }
    }
}