using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPFF.Web.UserControls
{
    public partial class HourBox : System.Web.UI.UserControl
    {
        public string Text
        {
            get
            {
                return txtHours.Text;
            }
            set
            {
                txtHours.Text = value;
            }
        }

        public Decimal NumVal
        {
            get
            {
                Decimal val = 0;
                Decimal.TryParse(Text, out val);
                return val;
            }
        }

        public string StyleClass { get; set; }

        private string _originalValue;
        public string OriginalValue
        {
            get
            {
                if (txtHours.HasAttributes)
                {
                    return txtHours.Attributes["OriginalValue"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                txtHours.Attributes.Add("OriginalValue", value);
                _originalValue = value;
            }
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            txtHours.CssClass = ("textBox hourBox " + StyleClass);
            txtHours.Attributes.Add("onBlur", "textChanged(this);");
            if ((txtHours.Text == String.Empty))
            {
                txtHours.Text = "0";
            }
            OriginalValue = txtHours.Text;
            base.OnPreRender(e);
        }
    }
}