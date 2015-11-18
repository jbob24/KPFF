
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
    public partial class ProjectList : ControlBase
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
        private DataTable _projectData;
        public DataTable ProjectData
        {
            get { return _projectData; }
            set { _projectData = value; }
        }


        private string _selectedProjects;
        private string SelectedProjects
        {
            get
            {
                _selectedProjects = ViewState["SelectedProjects"].GetValueOrDefault<string>();
                return _selectedProjects;
            }
            set
            {
                _selectedProjects = value;
                ViewState["SelectedProjects"] = _selectedProjects;
            }
        }


        private bool _displayFooter = true;
        public bool DisplayFooter
        {
            get { return _displayFooter; }
            set { _displayFooter = value; }
        }


        private bool _allowProjectSelect = true;
        public bool AllowProjectSelect
        {
            get { return _allowProjectSelect; }
            set { _allowProjectSelect = value; }
        }


        private string _currentWeek;
        public string CurrentWeek
        {
            get
            {
                if (string.IsNullOrEmpty(_currentWeek))
                {
                    _currentWeek = Schedule.GetCurrentWeekLabel();
                }

                return _currentWeek;
            }
            set { _currentWeek = value; }
        }

        private bool _showLastModified = false;
        public bool ShowLastModified
        {
            get { return _showLastModified; }
            set { _showLastModified = value; }
        }


        public void BindGrid()
        {
            if (ProjectData == null)
            {
                return;
            }

            if (DisplayFooter)
            {
                CalculateColumnTotals(ProjectData);
            }

            gridHours.Columns[3].Visible = ShowLastModified;

            gridHours.DataSource = ProjectData.DefaultView;
            gridHours.DataMember = ProjectData.TableName;
            gridHours.DataBind();

            if ((gridHours.Rows.Count > 0))
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            if (!DisplayFooter)
            {
                gridHours.FooterRow.Visible = false;
            }

            if (!AllowProjectSelect)
            {
                gridHours.Columns[0].Visible = false;
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

            week1HoursCellStyle = GridControlHelpers.GetCellStyle(week1HoursTotal, Engineer.HoursPerWeek);
            week2HoursCellStyle = GridControlHelpers.GetCellStyle(week2HoursTotal, Engineer.HoursPerWeek);
            week3HoursCellStyle = GridControlHelpers.GetCellStyle(week3HoursTotal, Engineer.HoursPerWeek);
            week4HoursCellStyle = GridControlHelpers.GetCellStyle(week4HoursTotal, Engineer.HoursPerWeek);
            week5HoursCellStyle = GridControlHelpers.GetCellStyle(week5HoursTotal, Engineer.HoursPerWeek);
            week6HoursCellStyle = GridControlHelpers.GetCellStyle(week6HoursTotal, Engineer.HoursPerWeek);
            week7HoursCellStyle = GridControlHelpers.GetCellStyle(week7HoursTotal, Engineer.HoursPerWeek);
            week8HoursCellStyle = GridControlHelpers.GetCellStyle(week8HoursTotal, Engineer.HoursPerWeek);
            week9HoursCellStyle = GridControlHelpers.GetCellStyle(week9HoursTotal, Engineer.HoursPerWeek);
            week10HoursCellStyle = GridControlHelpers.GetCellStyle(week10HoursTotal, Engineer.HoursPerWeek);
            week11HoursCellStyle = GridControlHelpers.GetCellStyle(week11HoursTotal, Engineer.HoursPerWeek);
            week12HoursCellStyle = GridControlHelpers.GetCellStyle(week12HoursTotal, Engineer.HoursPerWeek);
            week13HoursCellStyle = GridControlHelpers.GetCellStyle(week13HoursTotal, Engineer.HoursPerWeek);
            week14HoursCellStyle = GridControlHelpers.GetCellStyle(week14HoursTotal, Engineer.HoursPerWeek);
            week15HoursCellStyle = GridControlHelpers.GetCellStyle(week15HoursTotal, Engineer.HoursPerWeek);
            week16HoursCellStyle = GridControlHelpers.GetCellStyle(week16HoursTotal, Engineer.HoursPerWeek);
            week17HoursCellStyle = GridControlHelpers.GetCellStyle(week17HoursTotal, Engineer.HoursPerWeek);
            week18HoursCellStyle = GridControlHelpers.GetCellStyle(week18HoursTotal, Engineer.HoursPerWeek);
            week19HoursCellStyle = GridControlHelpers.GetCellStyle(week19HoursTotal, Engineer.HoursPerWeek);
            week20HoursCellStyle = GridControlHelpers.GetCellStyle(week20HoursTotal, Engineer.HoursPerWeek);
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            if (ShowLastModified)
            {
                GridView theGrid = (GridView)sender;
                DateTime dateValue = default(DateTime);

                foreach (GridViewRow row in theGrid.Rows)
                {
                    dateValue = GridControlHelpers.ConvertToDateValue(row.Cells[3].Text);
                    row.Cells[3].CssClass = GridControlHelpers.GetCellStyle(dateValue);
                }
            }
        }

        protected string GetWeekHeader(int weekNum)
        {

            return GridControlHelpers.GetWeekHeader(weekNum, WeekDate, Schedule, "currentWeekHeader");

            //If WeekDate Is Nothing Then
            //    Return "n/a"
            //End If

            //Dim header As StringBuilder = New StringBuilder()

            //header.Append("<div     ")

            //Dim weekLabel As String = WeekDate.WeekLabels(weekNum)

            //If (weekLabel = CurrentWeek) Then
            //    header.Append(" class='currentWeekHeader'>")
            //Else
            //    header.Append(">")
            //End If

            //header.Append(weekLabel)
            //header.Append("</div>")

            //Return header.ToString
        }


        public bool UnassignProjects(int empID, int unassignedByEmpID)
        {
            bool cantUnassign = false;

            foreach (GridViewRow row in gridHours.Rows)
            {
                var projectID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();
                var ID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();

                CheckBox chkProj = default(CheckBox);

                chkProj = (CheckBox)row.FindControl("selectProj");

                if (((chkProj != null) & chkProj.Checked))
                {
                    if (!Engineer.UnAssignProject(empID, projectID, unassignedByEmpID))
                    {
                        cantUnassign = true;
                    }
                }
            }

            return !cantUnassign;
        }

        public void ArchiveProjects(int empId)
        {
            foreach (GridViewRow row in gridHours.Rows)
            {
                int projectID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();
                int ID = gridHours.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();
                Project Project = new Project();

                CheckBox chkProj = default(CheckBox);

                chkProj = (CheckBox)row.FindControl("selectProj");

                if (((chkProj != null) & chkProj.Checked))
                {
                    if (Session["ActiveProj"].GetValueOrDefault<int>() == 1)
                    {
                        Project.ArchiveProject(empId, projectID);
                    }
                    else
                    {
                        Project.UnArchiveProject(empId, projectID);
                    }
                }
            }
        }
    }
}