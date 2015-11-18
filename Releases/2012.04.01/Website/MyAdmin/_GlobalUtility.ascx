<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="~/MyAdmin/_GlobalUtility.ascx.cs" Inherits="KPFF.PMP.MyAdmin.GlobalUtility" %>

<script Language="C#" runat="server">
  Public Property selState  As String
    Get
	Return ddlStates.SelectedItem.Text
    End Get
    Set
	ddlStates.Items.FindByText(value).Selected = true 
    End Set
  End Property

  Public Property TabIndex  As Integer
    Get
	Return ddlStates.TabIndex
    End Get
    Set
	ddlStates.TabIndex=value
    End Set
  End Property

Sub Page_Load(Source as Object, E as EventArgs)
if not Page.IsPostBack then
	Dim sStates() as string={"","AK", "AL", "AR", "AS", "AZ", "CA", _
	"CO", "CT", "DC", "DE", "FL", "GA", "GU", "HI", "IA", "ID", "IL", _
	"IN", "KS", "KY", "LA", "MA", "ME", "MD", "MI", "MN", "MO", "MP", _
	"MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", _
	"OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", _
	"VA", "VI", "VT", "WA", "WI", "WV", "WY"}
	ddlStates.datasource=sStates
	ddlStates.databind() 
end if
End Sub
</script>
<asp:DropDownList id="ddlStates" runat="server"/>
