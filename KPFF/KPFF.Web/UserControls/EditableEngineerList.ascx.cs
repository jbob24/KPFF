using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KPFF.Web.Model;

namespace KPFF.Web.UserControls
{
    public partial class EditableEngineerList : ControlBase
    {
        public decimal week1HoursTotal { get; set; }
        public decimal week2HoursTotal { get; set; }
        public decimal week3HoursTotal { get; set; }
        public decimal week4HoursTotal { get; set; }
        public decimal week5HoursTotal { get; set; }
        public decimal week6HoursTotal { get; set; }
        public decimal week7HoursTotal { get; set; }
        public decimal week8HoursTotal { get; set; }
        public decimal week9HoursTotal { get; set; }
        public decimal week10HoursTotal { get; set; }
        public decimal week11HoursTotal { get; set; }
        public decimal week12HoursTotal { get; set; }
        public decimal week13HoursTotal { get; set; }
        public decimal week14HoursTotal { get; set; }
        public decimal week15HoursTotal { get; set; }
        public decimal week16HoursTotal { get; set; }
        public decimal week17HoursTotal { get; set; }
        public decimal week18HoursTotal { get; set; }
        public decimal week19HoursTotal { get; set; }
        public decimal week20HoursTotal { get; set; }

        public ProjectSchedule ScheduleData { get; set; }

        public DataTable EmployeeData { get; set; }

        private string _selectedEmployees;
        private string SelectedEmployees
        {
            get
            {
                _selectedEmployees = ViewState["SelectedEmployees"].ToString();
                return _selectedEmployees;
            }
            set
            {
                _selectedEmployees = value;
                ViewState["SelectedEmployees"] = _selectedEmployees;
            }
        }

        public void BindGrid()
        {
            if (EmployeeData == null)
            {
                return;
            }
            CalculateColumnTotals(EmployeeData);

            gridHours.DataSource = ScheduleData;

            //gridHours.DataSource = EmployeeData.DefaultView;
            //gridHours.DataMember = EmployeeData.TableName;
            gridHours.DataBind();
            //if (gridHours.Rows.Count > 0)
            if (ScheduleData.Engineers.Count > 0)
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void CalculateColumnTotals(DataTable employeeData)
        {
            week1HoursTotal = ScheduleData.WeekTotals.Count > 0 ? ScheduleData.WeekTotals[0].Hours : 0;
            week2HoursTotal = ScheduleData.WeekTotals.Count > 1 ? ScheduleData.WeekTotals[1].Hours : 0;
            week3HoursTotal = ScheduleData.WeekTotals.Count > 2 ? ScheduleData.WeekTotals[2].Hours : 0;
            week4HoursTotal = ScheduleData.WeekTotals.Count > 3 ? ScheduleData.WeekTotals[3].Hours : 0;
            week5HoursTotal = ScheduleData.WeekTotals.Count > 4 ? ScheduleData.WeekTotals[4].Hours : 0;
            week6HoursTotal = ScheduleData.WeekTotals.Count > 5 ? ScheduleData.WeekTotals[5].Hours : 0;
            week7HoursTotal = ScheduleData.WeekTotals.Count > 6 ? ScheduleData.WeekTotals[6].Hours : 0;
            week8HoursTotal = ScheduleData.WeekTotals.Count > 7 ? ScheduleData.WeekTotals[7].Hours : 0;
            week9HoursTotal = ScheduleData.WeekTotals.Count > 8 ? ScheduleData.WeekTotals[8].Hours : 0;
            week10HoursTotal = ScheduleData.WeekTotals.Count > 9 ? ScheduleData.WeekTotals[9].Hours : 0;
            week11HoursTotal = ScheduleData.WeekTotals.Count > 10 ? ScheduleData.WeekTotals[10].Hours : 0;
            week12HoursTotal = ScheduleData.WeekTotals.Count > 11 ? ScheduleData.WeekTotals[11].Hours : 0;
            week13HoursTotal = ScheduleData.WeekTotals.Count > 12 ? ScheduleData.WeekTotals[12].Hours : 0;
            week14HoursTotal = ScheduleData.WeekTotals.Count > 13 ? ScheduleData.WeekTotals[13].Hours : 0;
            week15HoursTotal = ScheduleData.WeekTotals.Count > 14 ? ScheduleData.WeekTotals[14].Hours : 0;
            week16HoursTotal = ScheduleData.WeekTotals.Count > 15 ? ScheduleData.WeekTotals[15].Hours : 0;
            week17HoursTotal = ScheduleData.WeekTotals.Count > 16 ? ScheduleData.WeekTotals[16].Hours : 0;
            week18HoursTotal = ScheduleData.WeekTotals.Count > 17 ? ScheduleData.WeekTotals[17].Hours : 0;
            week19HoursTotal = ScheduleData.WeekTotals.Count > 18 ? ScheduleData.WeekTotals[18].Hours : 0;
            week20HoursTotal = ScheduleData.WeekTotals.Count > 19 ? ScheduleData.WeekTotals[19].Hours : 0; 
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            var theGrid = sender as GridView;

            int colNum = 1;
            HourBox weekBox;
            var defaultStyles = "hours ui-droppable ui-draggable ";
            string style;
            int empID;
            int weekID;
            foreach (GridViewRow row in theGrid.Rows)
            {
                empID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[0].ToString());
                foreach (TableCell cell in row.Cells)
                {
                    if ((colNum >= 4))
                    {
                        weekID = this.Weeks[colNum - 4].Id;
                        weekBox = (HourBox)(row.FindControl(string.Format("week{0}Hours",(colNum - 3).ToString())));
                        style = GridControlHelpers.GetCellStyle(Engineer.GetEmployeeWeekTotalHours(empID, weekID), Engineer.HoursPerWeek);
                        cell.CssClass = (defaultStyles + style);
                        weekBox.StyleClass = style;
                    }
                    colNum = (colNum + 1);
                }
                colNum = 1;
            }
        }

        protected string GetWeekHeader(int weekNum)
        {
            return GridControlHelpers.GetWeekHeader(weekNum, this.Weeks, this.CurrentWeek, "headerCurrentWeek");
        }

        public void UpdateData(int projectID, int modifiedByEmpID)
        {
            //foreach (GridViewRow row in gridHours.Rows)
            //{
            //    var employeeID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[0].ToString());
            //    HourBox weekBox;
            //    Decimal weekHours;
            //    string origHoursStr;
            //    Decimal origHours;
            //    for (int intI = 0; (intI <= ClsSchedule.cWeekSpan); intI++)
            //    {
            //        weekBox = (HourBox)(row.FindControl(string.Format("week{0}Hours", (intI + 1).ToString())));
            //        weekHours = weekBox.NumVal;
            //        origHoursStr = weekBox.OriginalValue;
            //        origHours = 0;
            //        if (!Decimal.TryParse(origHoursStr, out origHours) || (weekHours != origHours))
            //        {
            //            Engineer.UpdateSchedule(employeeID, projectID, this.WeekDate.WeekIds[intI], weekHours, modifiedByEmpID);
            //        }
            //    }
            //}
        }

        public void ReassignProjects(int assignToEmpID, int projectID, int reassignedByEmpID, string strFromDate, string strToDate)
        {
            //foreach (GridViewRow row in gridHours.Rows)
            //{
            //    var reassignFromEmpID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[0].ToString());
            //    CheckBox chkEmp;
            //    chkEmp = (CheckBox)(row.FindControl("selectEmp"));
            //    if (chkEmp != null && chkEmp.Checked)
            //    {
            //        if ((strFromDate == ""))
            //        {
            //            Engineer.ReassignProjectAllWeeks(reassignFromEmpID, projectID, assignToEmpID, reassignedByEmpID);
            //        }
            //        else
            //        {
            //            DateTime fromDate = new DateTime();
            //            DateTime toDate = new DateTime();
            //            DateTime.TryParse(strFromDate, out fromDate);
            //            DateTime.TryParse(strToDate, out toDate);
            //            Engineer.ReassignProjectByDateRange(reassignFromEmpID, projectID, assignToEmpID, fromDate, toDate, reassignedByEmpID);
            //        }
            //    }
            //}
        }

        public bool UnassignProjects(int projectID, int unassignedByEmpID)
        {
        //    bool cantUnassign = false;
        //    foreach (GridViewRow row in gridHours.Rows)
        //    {
        //        var empID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[0].ToString());
        //        CheckBox chkEmp;
        //        // For intI = 0 To clsSchedule.cWeekSpan
        //        chkEmp = ((CheckBox)(row.FindControl("selectEmp")));
        //        if ((!(chkEmp == null)
        //                    && chkEmp.Checked))
        //        {
        //            if (!Engineer.UnAssignProject(empID, projectID, unassignedByEmpID))
        //            {
        //                cantUnassign = true;
        //            }
        //        }
        //    }
        //    return !cantUnassign;
        //}
    }
}