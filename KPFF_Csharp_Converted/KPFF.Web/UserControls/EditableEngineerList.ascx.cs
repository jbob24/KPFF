
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
    public partial class EditableEngineerList : ControlBase
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
        private DataTable _employeeData;
        public DataTable EmployeeData
        {
            get { return _employeeData; }
            set { _employeeData = value; }
        }


        private string _selectedEmployees;
        private string SelectedEmployees
        {
            get
            {
                _selectedEmployees = ViewState["SelectedEmployees"].GetValueOrDefault<string>();
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

            gridHours.DataSource = EmployeeData.DefaultView;
            gridHours.DataMember = EmployeeData.TableName;
            gridHours.DataBind();

            if ((gridHours.Rows.Count > 0))
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void CalculateColumnTotals(DataTable employeeData)
        {
            foreach (DataRow row in employeeData.Rows)
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
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            GridView theGrid = (GridView)sender;
            //Dim rowNum As Integer = 1
            int colNum = 1;
            HourBox weekBox = default(HourBox);
            object defaultStyles = "hours ui-droppable ui-draggable ";
            string style = null;
            int empID = 0;
            int weekID = 0;

            foreach (GridViewRow row in theGrid.Rows)
            {
                empID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();
                foreach (TableCell cell in row.Cells)
                {
                    string teststr = "";
                    if (colNum >= 4)
                    {
                        weekID = WeekDate.WeekIDs[colNum - 4];
                        weekBox = (HourBox)row.FindControl("week" + (colNum - 3) + "Hours");

                        style = GridControlHelpers.GetCellStyle(Engineer.GetEmployeeWeekTotalHours(empID, weekID), Engineer.HoursPerWeek);

                        cell.CssClass = defaultStyles + style;
                        weekBox.StyleClass = style;
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

        public void UpdateData(int projectID, int modifiedByEmpID)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                foreach (GridViewRow row in gridHours.Rows)
                {
                    var employeeID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

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
                                Engineer.UpdateSchedule(employeeID, projectID, WeekDate.WeekIDs[intI], weekHours, modifiedByEmpID);
                            }
                        }
                    }
                }

                scope.Complete();
            }
        }

        public void ReassignProjects(int assignToEmpID, int projectID, int reassignedByEmpID, string strFromDate, string strToDate)
        {
            //using (var scope = new System.Transactions.TransactionScope())
            //{
                foreach (GridViewRow row in gridHours.Rows)
                {
                    var reassignFromEmpID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                    CheckBox chkEmp = default(CheckBox);

                    //For intI = 0 To clsSchedule.cWeekSpan
                    chkEmp = (CheckBox)row.FindControl("selectEmp");

                    if (((chkEmp != null) & chkEmp.Checked))
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

        public bool UnassignProjects(int projectID, int unassignedByEmpID)
        {
            bool cantUnassign = false;

            //using (var scope = new System.Transactions.TransactionScope())
            //{
                foreach (GridViewRow row in gridHours.Rows)
                {
                    int empID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                    CheckBox chkEmp = default(CheckBox);

                    //For intI = 0 To clsSchedule.cWeekSpan
                    chkEmp = (CheckBox)row.FindControl("selectEmp");

                    if (((chkEmp != null) & chkEmp.Checked))
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