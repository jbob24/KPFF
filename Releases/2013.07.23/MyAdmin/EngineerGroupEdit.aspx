<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EngineerGroupEdit.aspx.cs"
    Inherits="KPFF.PMP.MyAdmin.EngineerGroupEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Employee Information</title>
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../Style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../style_print.css" media="print" />
    <script src="../_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../_scripts/KPFF.js" />
    <link href="../style.css" rel="stylesheet" media="screen" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />
    <script language="javascript">
        function resizeOuterTo() {
            w = 474;
            h = 772;
            if (parseInt(navigator.appVersion) > 3) {
                if (navigator.appName == "Netscape") {
                    top.outerWidth = w;
                    top.outerHeight = h;
                }
                else
                    top.resizeTo(w, h);
            }
        }
    </script>
</head>
<body onload="resizeOuterTo();" topmargin="5px" leftmargin="15px" rightmargin="15px"
    bottommargin="15px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="2" cellspacing="0" border="0" width="380" style="border-left: solid 1px #666699;
            border-right: solid 1px #666699; border-top: solid 1px #666699; border-bottom: solid 1px #666699">
            <tr>
                <td colspan="2" bgcolor="#000066" valign="middle">
                    &nbsp; <font style="color: #ffffff; font-size: 12px;"><b>Edit Engineer Group Information</b></font>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <input type="button" value="Close Window" onclick="javascript:window.close();" class="formButton" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" height="2">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td width="100" class="formContent">
                    &nbsp; Name:
                </td>
                <td width="260">
                    <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Description:
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Width="250px" MaxLength="250" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Group" CssClass="formButton"
                        OnClick="btnUpdate_Click" /><img src="images/spacer.gif" width="5" />
                </td>
            </tr>
            <tr>
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td class="formContent" colspan="2">
                    &nbsp; Group Members:
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Add Member:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployees" runat="server" />
                    <asp:Button ID="btnAddEmployee" runat="server" Text="Add" CssClass="formButton" OnClick="btnAddEmployee_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridMembers" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                        Width="380px" RowStyle-CssClass="RowStyle" AlternatingRowStyle-CssClass="AlternateRowStyle"
                        CellPadding="4" DataKeyNames="EmployeeId" OnRowDataBound="gridMembers_RowDataBound"
                        OnRowDeleting="gridMembers_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="EmployeeId" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                <ItemStyle Width="200px" BorderColor="#666699" BorderWidth="2px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="#666699" BorderWidth="2px" />
                            </asp:CommandField>
                        </Columns>
                        <RowStyle CssClass="RowStyle" Height="15px" />
                        <AlternatingRowStyle CssClass="AlternateRowStyle" Height="15px" />
                        <HeaderStyle CssClass="HeaderStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
