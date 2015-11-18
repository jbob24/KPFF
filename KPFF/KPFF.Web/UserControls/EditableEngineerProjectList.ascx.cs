using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPFF.Web.Model;
using KPFF.Entities;

namespace KPFF.Web.UserControls
{
    public partial class EditableEngineerProjectList : ControlBase
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

        public string week1HoursCellStyle { get; set; }
        public string week2HoursCellStyle { get; set; }
        public string week3HoursCellStyle { get; set; }
        public string week4HoursCellStyle { get; set; }
        public string week5HoursCellStyle { get; set; }
        public string week6HoursCellStyle { get; set; }
        public string week7HoursCellStyle { get; set; }
        public string week8HoursCellStyle { get; set; }
        public string week9HoursCellStyle { get; set; }
        public string week10HoursCellStyle { get; set; }
        public string week11HoursCellStyle { get; set; }
        public string week12HoursCellStyle { get; set; }
        public string week13HoursCellStyle { get; set; }
        public string week14HoursCellStyle { get; set; }
        public string week15HoursCellStyle { get; set; }
        public string week16HoursCellStyle { get; set; }
        public string week17HoursCellStyle { get; set; }
        public string week18HoursCellStyle { get; set; }
        public string week19HoursCellStyle { get; set; }
        public string week20HoursCellStyle { get; set; }

        public EngineerSchedule ScheduleData { get; set; }

        private string _selectedProjects;
        private string SelectedProjects
        {
            get
            {
                _selectedProjects = ViewState["SelectedProjects"].ToString();
                return _selectedProjects;
            }
            set
            {
                _selectedProjects = value;
                ViewState["SelectedProjects"] = _selectedProjects;
            }
        }

        public void BindGrid()
        {
            if (ScheduleData == null)
            {
                return;
            }

            CalculateColumnTotals();
            gridHours.DataSource = ScheduleData;

            gridHours.DataBind();

            if (ScheduleData.Projects.Count > 0)
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void CalculateColumnTotals()
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

            week1HoursCellStyle = GridControlHelpers.GetCellStyle(week1HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week2HoursCellStyle = GridControlHelpers.GetCellStyle(week2HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week3HoursCellStyle = GridControlHelpers.GetCellStyle(week3HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week4HoursCellStyle = GridControlHelpers.GetCellStyle(week4HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week5HoursCellStyle = GridControlHelpers.GetCellStyle(week5HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week6HoursCellStyle = GridControlHelpers.GetCellStyle(week6HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week7HoursCellStyle = GridControlHelpers.GetCellStyle(week7HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week8HoursCellStyle = GridControlHelpers.GetCellStyle(week8HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week9HoursCellStyle = GridControlHelpers.GetCellStyle(week9HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week10HoursCellStyle = GridControlHelpers.GetCellStyle(week10HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week11HoursCellStyle = GridControlHelpers.GetCellStyle(week11HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week12HoursCellStyle = GridControlHelpers.GetCellStyle(week12HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week13HoursCellStyle = GridControlHelpers.GetCellStyle(week13HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week14HoursCellStyle = GridControlHelpers.GetCellStyle(week14HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week15HoursCellStyle = GridControlHelpers.GetCellStyle(week15HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week16HoursCellStyle = GridControlHelpers.GetCellStyle(week16HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week17HoursCellStyle = GridControlHelpers.GetCellStyle(week17HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week18HoursCellStyle = GridControlHelpers.GetCellStyle(week18HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week19HoursCellStyle = GridControlHelpers.GetCellStyle(week19HoursTotal, this.UserSession.Employee.HoursPerWeek);
            week20HoursCellStyle = GridControlHelpers.GetCellStyle(week20HoursTotal, this.UserSession.Employee.HoursPerWeek);
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            var theGrid = sender as GridView;

            int colNum = 1;
            HourBox weekBox;
            var defaultStyles = "hours ui-droppable ui-draggable ";
            foreach (GridViewRow row in theGrid.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (colNum >= 4)
                    {
                        weekBox = (HourBox)(row.FindControl(string.Format("week{0}Hours", (colNum - 3).ToString())));

                        switch (colNum)
                        {
                            case 4:
                                cell.CssClass = (defaultStyles + week1HoursCellStyle);
                                weekBox.StyleClass = week1HoursCellStyle;
                                break;
                            case 5:
                                cell.CssClass = (defaultStyles + week2HoursCellStyle);
                                weekBox.StyleClass = week2HoursCellStyle;
                                break;
                            case 6:
                                cell.CssClass = (defaultStyles + week3HoursCellStyle);
                                weekBox.StyleClass = week3HoursCellStyle;
                                break;
                            case 7:
                                cell.CssClass = (defaultStyles + week4HoursCellStyle);
                                weekBox.StyleClass = week4HoursCellStyle;
                                break;
                            case 8:
                                cell.CssClass = (defaultStyles + week5HoursCellStyle);
                                weekBox.StyleClass = week5HoursCellStyle;
                                break;
                            case 9:
                                cell.CssClass = (defaultStyles + week6HoursCellStyle);
                                weekBox.StyleClass = week6HoursCellStyle;
                                break;
                            case 10:
                                cell.CssClass = (defaultStyles + week7HoursCellStyle);
                                weekBox.StyleClass = week7HoursCellStyle;
                                break;
                            case 11:
                                cell.CssClass = (defaultStyles + week8HoursCellStyle);
                                weekBox.StyleClass = week8HoursCellStyle;
                                break;
                            case 12:
                                cell.CssClass = (defaultStyles + week9HoursCellStyle);
                                weekBox.StyleClass = week9HoursCellStyle;
                                break;
                            case 13:
                                cell.CssClass = (defaultStyles + week10HoursCellStyle);
                                weekBox.StyleClass = week10HoursCellStyle;
                                break;
                            case 14:
                                cell.CssClass = (defaultStyles + week11HoursCellStyle);
                                weekBox.StyleClass = week11HoursCellStyle;
                                break;
                            case 15:
                                cell.CssClass = (defaultStyles + week12HoursCellStyle);
                                weekBox.StyleClass = week12HoursCellStyle;
                                break;
                            case 16:
                                cell.CssClass = (defaultStyles + week13HoursCellStyle);
                                weekBox.StyleClass = week13HoursCellStyle;
                                break;
                            case 17:
                                cell.CssClass = (defaultStyles + week14HoursCellStyle);
                                weekBox.StyleClass = week14HoursCellStyle;
                                break;
                            case 18:
                                cell.CssClass = (defaultStyles + week15HoursCellStyle);
                                weekBox.StyleClass = week15HoursCellStyle;
                                break;
                            case 19:
                                cell.CssClass = (defaultStyles + week16HoursCellStyle);
                                weekBox.StyleClass = week16HoursCellStyle;
                                break;
                            case 20:
                                cell.CssClass = (defaultStyles + week17HoursCellStyle);
                                weekBox.StyleClass = week17HoursCellStyle;
                                break;
                            case 21:
                                cell.CssClass = (defaultStyles + week18HoursCellStyle);
                                weekBox.StyleClass = week18HoursCellStyle;
                                break;
                            case 22:
                                cell.CssClass = (defaultStyles + week19HoursCellStyle);
                                weekBox.StyleClass = week19HoursCellStyle;
                                break;
                            case 23:
                                cell.CssClass = (defaultStyles + week20HoursCellStyle);
                                weekBox.StyleClass = week20HoursCellStyle;
                                break;
                        }
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

        public void UpdateData(int empID, int modifiedByEmpID)
        {
            //foreach (GridViewRow row in gridHours.Rows)
            //{
            //    var projectID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[1].ToString());
            //    string ID = gridHours.DataKeys[row.RowIndex].Values[0].ToString();
            //    HourBox weekBox;
            //    Decimal weekHours;
            //    string origHoursStr;
            //    Decimal origHours;

            //    for (int i = 0; i <= ClsSchedule.cWeekSpan; i++)
            //    {
            //        weekBox = (HourBox)(row.FindControl(string.Format("week{0}Hours", (i + 1).ToString())));
            //        weekHours = weekBox.NumVal;
            //        origHoursStr = weekBox.OriginalValue;
            //        origHours = 0;
            //        if (!Decimal.TryParse(origHoursStr, out origHours) || (weekHours != origHours))
            //        {
            //            Engineer.UpdateSchedule(empID, projectID, this.WeekDate.WeekIds[i], weekHours, modifiedByEmpID);
            //        }
            //    }
            //}
        }

        public void ReassignProjects(int assignToEmpID, int reassignFromEmpID, int reassignedByEmpID, string strFromDate, string strToDate)
        {
            //// Dim intProjectID As Integer = 0
            //foreach (GridViewRow row in gridHours.Rows)
            //{
            //    var projectID = Convert.ToInt32(gridHours.DataKeys[row.RowIndex].Values[1].ToString());
            //    string ID = gridHours.DataKeys[row.RowIndex].Values[0].ToString();
            //    CheckBox chkProj;

            //    chkProj = (CheckBox)(row.FindControl("selectProj"));

            //    if (chkProj != null && chkProj.Checked)
            //    {
            //        if (string.IsNullOrEmpty(strFromDate))
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

        public bool UnassignProjects(int empID, int unassignedByEmpID)
        {
            bool cantUnassign = false;
            //foreach (GridViewRow row in gridHours.Rows)
            //{
            //    var projectID = Convert.ToSByte(gridHours.DataKeys[row.RowIndex].Values[1].ToString());
            //    string ID = gridHours.DataKeys[row.RowIndex].Values[0].ToString();
            //    CheckBox chkProj;

            //    chkProj = (CheckBox)(row.FindControl("selectProj"));
            //    if (chkProj != null && chkProj.Checked)
            //    {
            //        if (!Engineer.UnAssignProject(empID, projectID, unassignedByEmpID))
            //        {
            //            cantUnassign = true;
            //        }
            //    }
            //}
            return !cantUnassign;
        }
    }
}