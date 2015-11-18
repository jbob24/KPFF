using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPFF.Web.UserControls
{
    public partial class EngineerList : ControlBase
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

        public List<decimal> engHoursPerWeek = new List<decimal>();
        public DataTable EngineerData { get; set; }
        
        public void BindGrid()
        {
            if (EngineerData == null)
            {
                return;
            }

            CalculateColumnTotals(EngineerData);
            gridHours.DataSource = EngineerData.DefaultView;
            gridHours.DataMember = EngineerData.TableName;
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
                week1HoursTotal = (week1HoursTotal + GridControlHelpers.GetFieldValue(row, "Week1"));
                week2HoursTotal = (week2HoursTotal + GridControlHelpers.GetFieldValue(row, "Week2"));
                week3HoursTotal = (week3HoursTotal + GridControlHelpers.GetFieldValue(row, "Week3"));
                week4HoursTotal = (week4HoursTotal + GridControlHelpers.GetFieldValue(row, "Week4"));
                week5HoursTotal = (week5HoursTotal + GridControlHelpers.GetFieldValue(row, "Week5"));
                week6HoursTotal = (week6HoursTotal + GridControlHelpers.GetFieldValue(row, "Week6"));
                week7HoursTotal = (week7HoursTotal + GridControlHelpers.GetFieldValue(row, "Week7"));
                week8HoursTotal = (week8HoursTotal + GridControlHelpers.GetFieldValue(row, "Week8"));
                week9HoursTotal = (week9HoursTotal + GridControlHelpers.GetFieldValue(row, "Week9"));
                week10HoursTotal = (week10HoursTotal + GridControlHelpers.GetFieldValue(row, "Week10"));
                week11HoursTotal = (week11HoursTotal + GridControlHelpers.GetFieldValue(row, "Week11"));
                week12HoursTotal = (week12HoursTotal + GridControlHelpers.GetFieldValue(row, "Week12"));
                week13HoursTotal = (week13HoursTotal + GridControlHelpers.GetFieldValue(row, "Week13"));
                week14HoursTotal = (week14HoursTotal + GridControlHelpers.GetFieldValue(row, "Week14"));
                week15HoursTotal = (week15HoursTotal + GridControlHelpers.GetFieldValue(row, "Week15"));
                week16HoursTotal = (week16HoursTotal + GridControlHelpers.GetFieldValue(row, "Week16"));
                week17HoursTotal = (week17HoursTotal + GridControlHelpers.GetFieldValue(row, "Week17"));
                week18HoursTotal = (week18HoursTotal + GridControlHelpers.GetFieldValue(row, "Week18"));
                week19HoursTotal = (week19HoursTotal + GridControlHelpers.GetFieldValue(row, "Week19"));
                week20HoursTotal = (week20HoursTotal + GridControlHelpers.GetFieldValue(row, "Week20"));
                engHoursPerWeek.Add(GridControlHelpers.GetFieldValue(row, "HoursPerWeek"));
            }
        }

        protected void gridHours_PreRender(object sender, System.EventArgs e)
        {
            var theGrid = sender as GridView;

            int colNum = 1;
            var defaultStyles = string.Empty;
            DataBoundLiteralControl field;
            DateTime dateValue;
            Decimal hoursPerWeek;

            foreach (GridViewRow row in theGrid.Rows)
            {
                dateValue = GridControlHelpers.ConvertToDateValue(row.Cells[1].Text);
                row.Cells[1].CssClass = GridControlHelpers.GetCellStyle(dateValue);
                hoursPerWeek = engHoursPerWeek[row.RowIndex];
                foreach (TableCell cell in row.Cells)
                {
                    if ((colNum >= 4))
                    {
                        if (cell.HasControls())
                        {
                            field = (DataBoundLiteralControl)cell.Controls[0];
                            cell.CssClass = (defaultStyles + GridControlHelpers.GetCellStyle(field.Text, hoursPerWeek));
                            //  Engineer.HoursPerWeek)
                        }
                    }
                    colNum = (colNum + 1);
                }
                colNum = 1;
            }
        }

        protected string GetWeekHeader(int weekNum)
        {
            return GridControlHelpers.GetWeekHeader(weekNum, this.WeekDate, this.Schedule, "headerCurrentWeek");
        }
    }
}