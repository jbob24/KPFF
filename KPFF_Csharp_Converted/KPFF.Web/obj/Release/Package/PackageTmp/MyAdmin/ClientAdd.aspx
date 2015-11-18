<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/ClientAdd.apsx.cs" Inherits="KPFF.PMP.MyAdmin.ClientAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../style.css" rel="stylesheet" media="screen" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />
    <script language="javascript">
        function resizeOuterTo() {
            w = 474;
            h = 512;
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
    <table cellpadding="2" cellspacing="0" border="0" width="410" style="border-left: solid 1px #666699;
        border-right: solid 1px #666699; border-top: solid 1px #666699; border-bottom: solid 1px #666699">
        <tr>
            <td colspan="2" bgcolor="#000066" valign="middle">
                &nbsp; <font style="color: #ffffff; font-size: 12px;"><b>Add New Client</b></font>
            </td>
        </tr>
        <tr>
            <td width="100" class="formContent">
                &nbsp; Client Name:
            </td>
            <td width="310">
                <asp:TextBox ID="txtClientName" runat="server" CssClass="forms" Width="300px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; Client Type:
            </td>
            <td>
                <asp:DropDownList ID="cboClientType" runat="server" CssClass="forms" Width="304px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; Address:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="forms" Width="300px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; City:
            </td>
            <td>
                <asp:TextBox ID="txtCity" runat="server" CssClass="forms" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; State:
            </td>
            <td>
                <asp:TextBox ID="txtState" runat="server" CssClass="forms" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; Zip:
            </td>
            <td>
                <asp:TextBox ID="txtZip" runat="server" CssClass="forms" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; Office Phone:
            </td>
            <td>
                <asp:TextBox ID="txtOfficePhone" runat="server" CssClass="forms" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="formContent">
                &nbsp; Fax:
            </td>
            <td>
                <asp:TextBox ID="txtFax" runat="server" CssClass="forms" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" class="formContent">
                &nbsp; Comments:
            </td>
            <td>
                <asp:TextBox ID="txtComments" runat="server" CssClass="forms" Rows="5" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <img src="images/spacer.gif" />
            </td>
            <td align="center">
                <asp:Label ID="lblError" runat="server" CssClass="errors"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <input type="button" onclick="javascript:window.close();" value="Close Window" class="formButton" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" CssClass="formButton" Text="Add Client" OnClick="btnAdd_Click" /><img
                    src="images/spacer.gif" width="5" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="1">
                <img src="images/spacer.gif" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
