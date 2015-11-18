
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.UserControls
{
    public partial class EditableEngineerProjectList_v2 : ControlBase
    {

        public decimal week1HoursTotal = 0;
        public decimal week2HoursTotal = 0;
        public decimal week3HoursTotal = 0;
        public decimal week4HoursTotal = 0;
        public decimal week5HoursTotal = 0;
        public decimal week6HoursTotal = 0;
        public decimal week7HoursTotal = 0;
        public decimal week8HoursTotal = 0;
        public decimal week9HoursTotal = 0;
        public decimal week10HoursTotal = 0;
        public decimal week11HoursTotal = 0;
        public decimal week12HoursTotal = 0;
        public decimal week13HoursTotal = 0;
        public decimal week14HoursTotal = 0;
        public decimal week15HoursTotal = 0;
        public decimal week16HoursTotal = 0;
        public decimal week17HoursTotal = 0;
        public decimal week18HoursTotal = 0;
        public decimal week19HoursTotal = 0;
        public decimal week20HoursTotal = 0;

        public string week1HoursCellStyle = "";
        public string week2HoursCellStyle = "";
        public string week3HoursCellStyle = "";
        public string week4HoursCellStyle = "";
        public string week5HoursCellStyle = "";
        public string week6HoursCellStyle = "";
        public string week7HoursCellStyle = "";
        public string week8HoursCellStyle = "";
        public string week9HoursCellStyle = "";
        public string week10HoursCellStyle = "";
        public string week11HoursCellStyle = "";
        public string week12HoursCellStyle = "";
        public string week13HoursCellStyle = "";
        public string week14HoursCellStyle = "";
        public string week15HoursCellStyle = "";
        public string week16HoursCellStyle = "";
        public string week17HoursCellStyle = "";
        public string week18HoursCellStyle = "";
        public string week19HoursCellStyle = "";
        public string week20HoursCellStyle = "";

        public DataTable ProjectData { get; set; }

        private string SelectedProjects
        {
            get
            {
                return ViewState["SelectedProjects"].GetValueOrDefault<string>();
            }
            set
            {
                ViewState["SelectedProjects"] = value;
            }
        }


        private decimal _hoursPerWeek;
        public decimal HoursPerWeek
        {
            get
            {
                if ((_hoursPerWeek <= 0))
                {
                    _hoursPerWeek = 40;
                }
                return _hoursPerWeek;
            }
            set { _hoursPerWeek = value; }
        }

        public int EmpId { get; set; }

        public void BindGrid()
        {
            if (ProjectData == null)
            {
                return;
            }

            CalculateColumnTotals(ProjectData);

            gridHours.DataSource = ProjectData.DefaultView;
            gridHours.DataMember = ProjectData.TableName;
            gridHours.DataBind();

            if ((gridHours.Rows.Count > 0))
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void CalculateColumnTotals(DataTable scheduleData)
        {
            foreach (DataRow row in scheduleData.Rows)
            {
                week1HoursTotal = week1HoursTotal + GridControlHelpers.GetFieldValue(row, "Week1");
                // row.Field(Of Decimal)("Week1")
                week2HoursTotal = week2HoursTotal + GridControlHelpers.GetFieldValue(row, "Week2");
                // row.Field(Of Decimal)("Week2")
                week3HoursTotal = week3HoursTotal + GridControlHelpers.GetFieldValue(row, "Week3");
                // row.Field(Of Decimal)("Week3")
                week4HoursTotal = week4HoursTotal + GridControlHelpers.GetFieldValue(row, "Week4");
                // row.Field(Of Decimal)("Week4")
                week5HoursTotal = week5HoursTotal + GridControlHelpers.GetFieldValue(row, "Week5");
                // row.Field(Of Decimal)("Week5")
                week6HoursTotal = week6HoursTotal + GridControlHelpers.GetFieldValue(row, "Week6");
                // row.Field(Of Decimal)("Week6")
                week7HoursTotal = week7HoursTotal + GridControlHelpers.GetFieldValue(row, "Week7");
                // row.Field(Of Decimal)("Week7")
                week8HoursTotal = week8HoursTotal + GridControlHelpers.GetFieldValue(row, "Week8");
                // row.Field(Of Decimal)("Week8")
                week9HoursTotal = week9HoursTotal + GridControlHelpers.GetFieldValue(row, "Week9");
                // row.Field(Of Decimal)("Week9")
                week10HoursTotal = week10HoursTotal + GridControlHelpers.GetFieldValue(row, "Week10");
                // row.Field(Of Decimal)("Week10")
                week11HoursTotal = week11HoursTotal + GridControlHelpers.GetFieldValue(row, "Week11");
                // row.Field(Of Decimal)("Week10")
                week12HoursTotal = week12HoursTotal + GridControlHelpers.GetFieldValue(row, "Week12");
                // row.Field(Of Decimal)("Week10")
                week13HoursTotal = week13HoursTotal + GridControlHelpers.GetFieldValue(row, "Week13");
                // row.Field(Of Decimal)("Week10")
                week14HoursTotal = week14HoursTotal + GridControlHelpers.GetFieldValue(row, "Week14");
                // row.Field(Of Decimal)("Week10")
                week15HoursTotal = week15HoursTotal + GridControlHelpers.GetFieldValue(row, "Week15");
                // row.Field(Of Decimal)("Week10")
                week16HoursTotal = week16HoursTotal + GridControlHelpers.GetFieldValue(row, "Week16");
                // row.Field(Of Decimal)("Week10")
                week17HoursTotal = week17HoursTotal + GridControlHelpers.GetFieldValue(row, "Week17");
                // row.Field(Of Decimal)("Week10")
                week18HoursTotal = week18HoursTotal + GridControlHelpers.GetFieldValue(row, "Week18");
                // row.Field(Of Decimal)("Week10")
                week19HoursTotal = week19HoursTotal + GridControlHelpers.GetFieldValue(row, "Week19");
                // row.Field(Of Decimal)("Week10")
                week20HoursTotal = week20HoursTotal + GridControlHelpers.GetFieldValue(row, "Week20");
                // row.Field(Of Decimal)("Week10")
            }

            week1HoursCellStyle = GridControlHelpers.GetCellStyle(week1HoursTotal, HoursPerWeek);
            week2HoursCellStyle = GridControlHelpers.GetCellStyle(week2HoursTotal, HoursPerWeek);
            week3HoursCellStyle = GridControlHelpers.GetCellStyle(week3HoursTotal, HoursPerWeek);
            week4HoursCellStyle = GridControlHelpers.GetCellStyle(week4HoursTotal, HoursPerWeek);
            week5HoursCellStyle = GridControlHelpers.GetCellStyle(week5HoursTotal, HoursPerWeek);
            week6HoursCellStyle = GridControlHelpers.GetCellStyle(week6HoursTotal, HoursPerWeek);
            week7HoursCellStyle = GridControlHelpers.GetCellStyle(week7HoursTotal, HoursPerWeek);
            week8HoursCellStyle = GridControlHelpers.GetCellStyle(week8HoursTotal, HoursPerWeek);
            week9HoursCellStyle = GridControlHelpers.GetCellStyle(week9HoursTotal, HoursPerWeek);
            week10HoursCellStyle = GridControlHelpers.GetCellStyle(week10HoursTotal, HoursPerWeek);
            week11HoursCellStyle = GridControlHelpers.GetCellStyle(week11HoursTotal, HoursPerWeek);
            week12HoursCellStyle = GridControlHelpers.GetCellStyle(week12HoursTotal, HoursPerWeek);
            week13HoursCellStyle = GridControlHelpers.GetCellStyle(week13HoursTotal, HoursPerWeek);
            week14HoursCellStyle = GridControlHelpers.GetCellStyle(week14HoursTotal, HoursPerWeek);
            week15HoursCellStyle = GridControlHelpers.GetCellStyle(week15HoursTotal, HoursPerWeek);
            week16HoursCellStyle = GridControlHelpers.GetCellStyle(week16HoursTotal, HoursPerWeek);
            week17HoursCellStyle = GridControlHelpers.GetCellStyle(week17HoursTotal, HoursPerWeek);
            week18HoursCellStyle = GridControlHelpers.GetCellStyle(week18HoursTotal, HoursPerWeek);
            week19HoursCellStyle = GridControlHelpers.GetCellStyle(week19HoursTotal, HoursPerWeek);
            week20HoursCellStyle = GridControlHelpers.GetCellStyle(week20HoursTotal, HoursPerWeek);
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            GridView theGrid = (GridView)sender;
            //Dim rowNum As Integer = 1
            int colNum = 1;
            HourBox weekBox = default(HourBox);
            object defaultStyles = "hours ui-droppable ui-draggable ";


            foreach (GridViewRow row in theGrid.Rows)
            {
                //If row.RowIndex Mod 2 = 0 Then
                //    row.CssClass = "alternateRow"
                //End If

                foreach (TableCell cell in row.Cells)
                {
                    string teststr = "";
                    //cell.CssClass = "test"
                    if (colNum >= 4)
                    {
                        //cell.Attributes.Add("onMouseover", "setDroppable(this);")
                        weekBox = (HourBox)row.FindControl("week" + (colNum - 3) + "Hours");
                        switch (colNum)
                        {
                            case 4:
                                cell.CssClass = defaultStyles + week1HoursCellStyle;
                                weekBox.StyleClass = week1HoursCellStyle;
                                break;
                            case 5:
                                cell.CssClass = defaultStyles + week2HoursCellStyle;
                                weekBox.StyleClass = week2HoursCellStyle;
                                break;
                            case 6:
                                cell.CssClass = defaultStyles + week3HoursCellStyle;
                                weekBox.StyleClass = week3HoursCellStyle;
                                break;
                            case 7:
                                cell.CssClass = defaultStyles + week4HoursCellStyle;
                                weekBox.StyleClass = week4HoursCellStyle;
                                break;
                            case 8:
                                cell.CssClass = defaultStyles + week5HoursCellStyle;
                                weekBox.StyleClass = week5HoursCellStyle;
                                break;
                            case 9:
                                cell.CssClass = defaultStyles + week6HoursCellStyle;
                                weekBox.StyleClass = week6HoursCellStyle;
                                break;
                            case 10:
                                cell.CssClass = defaultStyles + week7HoursCellStyle;
                                weekBox.StyleClass = week7HoursCellStyle;
                                break;
                            case 11:
                                cell.CssClass = defaultStyles + week8HoursCellStyle;
                                weekBox.StyleClass = week8HoursCellStyle;
                                break;
                            case 12:
                                cell.CssClass = defaultStyles + week9HoursCellStyle;
                                weekBox.StyleClass = week9HoursCellStyle;
                                break;
                            case 13:
                                cell.CssClass = defaultStyles + week10HoursCellStyle;
                                weekBox.StyleClass = week10HoursCellStyle;
                                break;
                            case 14:
                                cell.CssClass = defaultStyles + week11HoursCellStyle;
                                weekBox.StyleClass = week11HoursCellStyle;
                                break;
                            case 15:
                                cell.CssClass = defaultStyles + week12HoursCellStyle;
                                weekBox.StyleClass = week12HoursCellStyle;
                                break;
                            case 16:
                                cell.CssClass = defaultStyles + week13HoursCellStyle;
                                weekBox.StyleClass = week13HoursCellStyle;
                                break;
                            case 17:
                                cell.CssClass = defaultStyles + week14HoursCellStyle;
                                weekBox.StyleClass = week14HoursCellStyle;
                                break;
                            case 18:
                                cell.CssClass = defaultStyles + week15HoursCellStyle;
                                weekBox.StyleClass = week15HoursCellStyle;
                                break;
                            case 19:
                                cell.CssClass = defaultStyles + week16HoursCellStyle;
                                weekBox.StyleClass = week16HoursCellStyle;
                                break;
                            case 20:
                                cell.CssClass = defaultStyles + week17HoursCellStyle;
                                weekBox.StyleClass = week17HoursCellStyle;
                                break;
                            case 21:
                                cell.CssClass = defaultStyles + week18HoursCellStyle;
                                weekBox.StyleClass = week18HoursCellStyle;
                                break;
                            case 22:
                                cell.CssClass = defaultStyles + week19HoursCellStyle;
                                weekBox.StyleClass = week19HoursCellStyle;
                                break;
                            case 23:
                                cell.CssClass = defaultStyles + week20HoursCellStyle;
                                weekBox.StyleClass = week20HoursCellStyle;
                                break;
                        }

                    }
                    colNum = colNum + 1;
                }
                colNum = 1;
            }
        }

        protected string GetWeekHeader(int weekNum)
        {
            return GridControlHelpers.GetWeekHeader(weekNum, WeekDate, Schedule, "headerCurrentWeek");
        }

        public void UpdateData(int empID, int modifiedByEmpID)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                foreach (GridViewRow row in gridHours.Rows)
                {
                    var projectID = gridHours.DataKeys[row.RowIndex].Values[1].GetValueOrDefault<int>();
                    var ID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                    HourBox weekBox = null;
                    decimal weekHours = 0;
                    string origHoursStr = null;
                    decimal origHours = 0;

                    for (var intI = 0; intI <= clsSchedule.cWeekSpan; intI++)
                    {
                        weekBox = (HourBox)row.FindControl("week" + (intI + 1) + "Hours");

                        if (weekBox != null)
                        {
                            weekHours = weekBox.NumVal;
                            origHoursStr = weekBox.OriginalValue;
                            origHours = 0;
                            if ((!decimal.TryParse(origHoursStr, out origHours) || weekHours != origHours))
                            {
                                Engineer.UpdateSchedule(empID, projectID, WeekDate.WeekIDs[intI], weekHours, modifiedByEmpID);
                            }
                        }
                    }
                }
                scope.Complete();
            }
        }

        public void ReassignProjects(int assignToEmpID, int reassignFromEmpID, int reassignedByEmpID, string strFromDate, string strToDate)
        {
            //using (var scope = new System.Transactions.TransactionScope())
            //{
                foreach (GridViewRow row in gridHours.Rows)
                {
                    var projectID = gridHours.DataKeys[row.RowIndex].Values[1].GetValueOrDefault<int>();
                    var ID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                    CheckBox chkProj = default(CheckBox);

                    //For intI = 0 To clsSchedule.cWeekSpan
                    chkProj = (CheckBox)row.FindControl("selectProj");

                    if (((chkProj != null) & chkProj.Checked))
                    {
                        // Me.chkAll.Checked = True Then
                        if (string.IsNullOrEmpty(strFromDate))
                        {
                            Engineer.ReassignProjectAllWeeks(reassignFromEmpID, projectID, assignToEmpID, reassignedByEmpID);
                        }
                        else
                        {
                            DateTime fromDate = new DateTime();
                            DateTime toDate = new DateTime();

                            DateTime.TryParse(strFromDate, out fromDate);
                            DateTime.TryParse(strToDate, out toDate);

                            Engineer.ReassignProjectByDateRange(reassignFromEmpID, projectID, assignToEmpID, fromDate, toDate, reassignedByEmpID);
                        }
                    }
                    //Next
                //}
                //scope.Complete();
            }
        }

        public bool UnassignProjects(int empID, int unassignedByEmpID)
        {
            bool cantUnassign = false;

            //using (var scope = new System.Transactions.TransactionScope())
            //{
                foreach (GridViewRow row in gridHours.Rows)
                {
                    var projectID = gridHours.DataKeys[row.RowIndex].Values[1].GetValueOrDefault<int>();
                    var ID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                    CheckBox chkProj = default(CheckBox);

                    //For intI = 0 To clsSchedule.cWeekSpan
                    chkProj = (CheckBox)row.FindControl("selectProj");

                    if (((chkProj != null) & chkProj.Checked))
                    {
                        if (!Engineer.UnAssignProject(empID, projectID, unassignedByEmpID))
                        {
                            cantUnassign = true;
                        }
                    }
                    //Next
                //}
                //scope.Complete();
            }

            return !cantUnassign;

            //If cantUnassign Then
            //    Return False
            //Else
            //    Return True
            //End If
        }
    }
}