using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPFF.Web.Model
{
    public class ScheduleWeek
    {
        public int WeekId { get; set; }
        public DateTime MondayDate { get; set; }
        public decimal Hours { get; set; }

        public ScheduleWeek(int weekId, DateTime mondayDate, decimal hours)
        {
            this.WeekId = weekId;
            this.MondayDate = mondayDate;
            this.Hours = hours;
        }
    }

    public class ProjectSchedule
    {
        public List<EngineerModel> Engineers { get; set; }

        public List<ScheduleWeek> WeekTotals
        {
            get
            {
                var weeks = new Dictionary<int, ScheduleWeek>();

                foreach (var engineer in Engineers)
                {
                    foreach (var week in engineer.Schedule)
                    {
                        if (weeks.ContainsKey(week.WeekId))
                        {
                            weeks[week.WeekId].Hours += week.Hours;
                        }
                        else
                        {
                            var newWeek = new ScheduleWeek(week.WeekId, week.MondayDate, week.Hours);
                            weeks.Add(week.WeekId, newWeek);
                        }
                    }
                }

                return weeks.Select(w => w.Value).ToList();
            }
        }
    }

    public class EngineerSchedule
    {
        public List<ProjectModel> Projects { get; set; }

        public List<ScheduleWeek> WeekTotals
        {
            get
            {
                var weeks = new Dictionary<int, ScheduleWeek>();

                foreach (var project in Projects)
                {
                    foreach (var week in project.Schedule)
                    {
                        if (weeks.ContainsKey(week.WeekId))
                        {
                            weeks[week.WeekId].Hours += week.Hours;
                        }
                        else
                        {
                            var newWeek = new ScheduleWeek(week.WeekId, week.MondayDate, week.Hours);
                            weeks.Add(week.WeekId, newWeek);
                        }
                    }
                }

                return weeks.Select(w => w.Value).ToList();
            }
        }
    }
}