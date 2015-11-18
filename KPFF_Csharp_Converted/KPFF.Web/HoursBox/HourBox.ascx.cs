
using System.ComponentModel;

public partial class HourBox : System.Web.UI.UserControl
{

	public string Text {
		get { return txtHours.Text; }
		set { txtHours.Text = value; }
	}

	public decimal NumVal {
		get {
			decimal val = 0;

			decimal.TryParse(Text, out val);

			return val;
		}
	}


	private string _styleClass;
	public string StyleClass {
		get { return _styleClass; }
		set { _styleClass = value; }
	}


	private string _originalValue;
	public string OriginalValue {
		get {
			if ((txtHours.HasAttributes)) {
				//If Not ViewState("OriginalValue") Is Nothing Then
				//Return ViewState("OriginalValue").ToString()
				return txtHours.Attributes["OriginalValue"].ToString();
			} else {
				return "";
			}

			//Return _originalValue
		}
		set {
			//ViewState("OriginalValue") = value
			txtHours.Attributes.Add("OriginalValue", value);
			_originalValue = value;
		}
	}

	//Public Overrides Sub RenderControl(ByVal writer As System.Web.UI.HtmlTextWriter)
	protected override void OnPreRender(System.EventArgs e)
	{
		txtHours.CssClass = "textBox hourBox " + _styleClass;
		txtHours.Attributes.Add("onBlur", "textChanged(this);");
		if (txtHours.Text == string.Empty) {
			txtHours.Text = "0";
		}

		OriginalValue = txtHours.Text;
		//txtHours.Attributes.Add("OriginalValue", txtHours.Text)
		//MyBase.RenderControl(writer)

		base.OnPreRender(e);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik, @toddanglin
//Facebook: facebook.com/telerik
//=======================================================
