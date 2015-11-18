using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace KPFF.PMP.UserControls
{
    public partial class EngineerList : ControlBase
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

        public List<decimal> engHoursPerWeek = new List<decimal>();
        private DataTable _engineerData;
        public DataTable EngineerData
        {
            get { return _engineerData; }
            set { _engineerData = value; }
        }

        public bool AllowEngineerSelect { get; set; }

        public void BindGrid()
        {
            if (EngineerData == null)
            {
                return;
            }

            CalculateColumnTotals(EngineerData);

            gridHours.Columns[0].Visible = true;

            gridHours.DataSource = EngineerData.DefaultView;
            gridHours.DataMember = EngineerData.TableName;
            gridHours.DataBind();

            if ((gridHours.Rows.Count > 0))
            {
                gridHours.UseAccessibleHeader = true;
                gridHours.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridHours.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            if (!AllowEngineerSelect)
            {
                gridHours.Columns[0].Visible = false;
            }
        }

        protected void CalculateColumnTotals(DataTable scheduleData)
        {
            foreach (DataRow row in scheduleData.Rows)
            {
                week1HoursTotal = week1HoursTotal + GridControlHelpers.GetFieldValue(row, "Week1");
                week2HoursTotal = week2HoursTotal + GridControlHelpers.GetFieldValue(row, "Week2");
                week3HoursTotal = week3HoursTotal + GridControlHelpers.GetFieldValue(row, "Week3");
                week4HoursTotal = week4HoursTotal + GridControlHelpers.GetFieldValue(row, "Week4");
                week5HoursTotal = week5HoursTotal + GridControlHelpers.GetFieldValue(row, "Week5");
                week6HoursTotal = week6HoursTotal + GridControlHelpers.GetFieldValue(row, "Week6");
                week7HoursTotal = week7HoursTotal + GridControlHelpers.GetFieldValue(row, "Week7");
                week8HoursTotal = week8HoursTotal + GridControlHelpers.GetFieldValue(row, "Week8");
                week9HoursTotal = week9HoursTotal + GridControlHelpers.GetFieldValue(row, "Week9");
                week10HoursTotal = week10HoursTotal + GridControlHelpers.GetFieldValue(row, "Week10");
                week11HoursTotal = week11HoursTotal + GridControlHelpers.GetFieldValue(row, "Week11");
                week12HoursTotal = week12HoursTotal + GridControlHelpers.GetFieldValue(row, "Week12");
                week13HoursTotal = week13HoursTotal + GridControlHelpers.GetFieldValue(row, "Week13");
                week14HoursTotal = week14HoursTotal + GridControlHelpers.GetFieldValue(row, "Week14");
                week15HoursTotal = week15HoursTotal + GridControlHelpers.GetFieldValue(row, "Week15");
                week16HoursTotal = week16HoursTotal + GridControlHelpers.GetFieldValue(row, "Week16");
                week17HoursTotal = week17HoursTotal + GridControlHelpers.GetFieldValue(row, "Week17");
                week18HoursTotal = week18HoursTotal + GridControlHelpers.GetFieldValue(row, "Week18");
                week19HoursTotal = week19HoursTotal + GridControlHelpers.GetFieldValue(row, "Week19");
                week20HoursTotal = week20HoursTotal + GridControlHelpers.GetFieldValue(row, "Week20");

                engHoursPerWeek.Add(GridControlHelpers.GetFieldValue(row, "HoursPerWeek"));
            }
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            GridView theGrid = (GridView)sender;
            //Dim rowNum As Integer = 1
            int colNum = 1;
            object defaultStyles = "";
            DataBoundLiteralControl field = null;
            DateTime dateValue = default(DateTime);
            decimal hoursPerWeek = default(decimal);

            foreach (GridViewRow row in theGrid.Rows)
            {
                var empId = row.Cells[0].Text;
                row.Cells[1].Attributes.Add("empId", empId);

                dateValue = GridControlHelpers.ConvertToDateValue(row.Cells[3].Text);
                row.Cells[3].CssClass = GridControlHelpers.GetCellStyle(dateValue);
                
                hoursPerWeek =  engHoursPerWeek[row.RowIndex];

                foreach (TableCell cell in row.Cells)
                {
                    string teststr = "";
                    if (colNum >= 4)
                    {
                        if ((cell.HasControls()))
                        {
                            field = (DataBoundLiteralControl)cell.Controls[0];

                            cell.CssClass = defaultStyles + GridControlHelpers.GetCellStyle(field.Text, hoursPerWeek);
                            // Engineer.HoursPerWeek)
                        }
                    }
                    colNum = colNum + 1;
                }
                colNum = 1;
            }

            theGrid.Columns[0].Visible = false;
        }

        protected string GetWeekHeader(int weekNum)
        {
            return GridControlHelpers.GetWeekHeader(weekNum, WeekDate, Schedule, "headerCurrentWeek");
        }

        protected void gridHours_DataBound(object sender, EventArgs e)
        {
            if (AllowEngineerSelect)
            {
                GridView grid = (GridView)sender;

                foreach (GridViewRow row in grid.Rows)
                {
                    var empId = row.Cells[0].Text;

                    row.Cells[1].Attributes.Add("empId", empId);                   
                }
            }
        }
    }
}