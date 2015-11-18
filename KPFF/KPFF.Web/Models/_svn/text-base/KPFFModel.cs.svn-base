using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPFF.Web.Model;

namespace KPFF.Web.Models
{
    public class KPFFModel
    {
        public static List<ProjectModel> GetEmployeeProjectScheduleByDateRange(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var schedule = new List<ProjectModel>();

            var projects = KPFF.Business.Employee.GetEmployeeProjects(employeeId);
            var projectSchedules = KPFF.Business.Project.GetProjectSchedules(projects, fromDate, toDate);
            var weekData = KPFF.Business.Week.GetWeeks(KPFF.Business.KPFFCache.Current).Where(w => w.MondayDate >= fromDate && w.MondayDate <= toDate).OrderBy(w => w.MondayDate).ToList();
            
            foreach (var project in projects)
            {
                var projectSchedule = new ProjectModel(project.Id, project.ProjectNumber, project.Name, project.PICCode, project.PM1Code);

                foreach (var week in weekData)
                {
                    projectSchedule.Schedule.Add(new ScheduleWeek(week.Id, week.MondayDate, projectSchedules.Where(s => s.ProjectId == project.Id && s.WeekId == week.Id).Sum(s => s.Hours)));
                }

                //var scheduleWeeks = (from s in projectSchedules
                //                    where s.ProjectId == project.Id
                //                    group s by s.WeekId into weeks
                //                    select new ScheduleWeek
                //                    {
                //                        WeekId = weeks.Key,
                //                        Hours = weeks.Sum( w => w.Hours),
                //                        MondayDate = weekData.ContainsKey(weeks.Key) ? weekData[weeks.Key].MondayDate : DateTime.MinValue,
                //                    }).ToList();


                //projectSchedule.Schedule = scheduleWeeks;
                schedule.Add(projectSchedule);
            }

            return schedule;
        }

        public static List<EngineerModel> GetProjectEmployeeScheduleByDateRange(int projectId, DateTime fromDate, DateTime toDate)
        {
            var schedule = new List<EngineerModel>();
            var project = KPFF.Business.Project.GetById(projectId);
            var engineers = KPFF.Business.Project.GetProjectEngineers(projectId);
            var projectSchedules = KPFF.Business.Project.GetProjectSchedules(new KPFF.Entities.ProjectList { project }, fromDate, toDate);
            var weekData = KPFF.Business.Week.GetWeeks(KPFF.Business.KPFFCache.Current).Where(w => w.MondayDate >= fromDate && w.MondayDate <= toDate).OrderBy(w => w.MondayDate).ToList();
            

            foreach (var engineer in engineers)
            {
                var empSchedule = new EngineerModel(engineer.Id, engineer.Name, GetEngineerRole(project, engineer));


                foreach (var week in weekData)
                {
                    empSchedule.Schedule.Add(new ScheduleWeek(week.Id, week.MondayDate, projectSchedules.Where(s => s.EmployeeId == engineer.Id && s.WeekId == week.Id).Sum(s => s.Hours)));
                }

                schedule.Add(empSchedule);
            }

            return schedule;
        }

        private static string GetEngineerRole(KPFF.Entities.Project project, KPFF.Entities.Employee engineer)
        {
            if (project.PICId == engineer.Id)
            {
                return "PIC";
            }

            if (project.PM1Id == engineer.Id)
            {
                return "Project Manager";
            }

            return "Engineer";
        }
    }
}