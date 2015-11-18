using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPFF.Web.Model;

namespace KPFF.Web.Models.ViewModels
{
    public class ProjectSummaryViewModel : ViewModelBase
    {
        public KPFF.Entities.Project Project { get; set; }
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
}