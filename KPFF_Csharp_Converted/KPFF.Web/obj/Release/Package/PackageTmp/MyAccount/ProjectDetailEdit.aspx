<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAccount/ProjectDetailEdit.aspx.cs" Inherits="KPFF.PMP.MyAccount.ProjectDetailEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Project Summary</title>
    <link href="../style.css" rel="stylesheet" media="screen" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />

    <script type="text/javascript">
        function reloadOpener() {
            window.opener.location.href = window.opener.location.href; // refresh the main page
            window.opener.focus(); // focus on the main page
            window.close(); // close the popup page
        }    
    </script>
</head>
<body topmargin="5px" leftmargin="15px" rightmargin="15px" bottommargin="15px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="2" cellspacing="0" border="0" width="746" style="border-left: solid 1px #666699;
            border-right: solid 1px #666699; border-top: solid 1px #666699; border-bottom: solid 1px #666699">
            <tr>
                <td colspan="4" bgcolor="#000066" valign="middle">
                    &nbsp; <font style="color: #ffffff; font-size: 12px;"><b>Edit Project Summary</b></font>
            </tr>
            <tr>
                <td width="150" class="detailTitle">
                    &nbsp;Project Name:
                </td>
                <td width="246" class="detailData">
                    <asp:TextBox ID="txtProjectName" runat="server" Width="230px" CssClass="forms" MaxLength="50"></asp:TextBox>
                </td>
                <td width="120" class="detailTitle">
                    Project #:
                </td>
                <td width="230" class="detailData">
                    <asp:TextBox ID="txtProjectNo" runat="server" CssClass="forms" MaxLength="50" Width="220px"></asp:TextBox><asp:Label
                        ID="lblProjectNo" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;PIC:
                </td>
                <td class="detailData">
                    <asp:DropDownList ID="cboPIC" runat="server" CssClass="forms" Width="234px">
                    </asp:DropDownList>
                </td>
                <td class="detailTitle">
                    PM:
                </td>
                <td class="detailData">
                    <asp:DropDownList ID="cboPM" runat="server" CssClass="forms" Width="224px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;Est. Start of Const.:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtEstStartOfConstruction" runat="server" CssClass="forms" MaxLength="50"
                        Width="230px"></asp:TextBox>
                </td>
                <td class="detailTitle">
                    Est. End of Const.:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtEstCompletionOfConstruction" runat="server" CssClass="forms"
                        MaxLength="50" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;Client Name:
                </td>
                <td class="detailData">
                    <asp:DropDownList ID="cboClientName" runat="server" CssClass="forms" Width="234px">
                    </asp:DropDownList>
                </td>
                <td class="detailTitle">
                    Fee:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtFeeAmount" runat="server" CssClass="forms" MaxLength="50" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;Project Location:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtProjectLocation" runat="server" CssClass="forms" MaxLength="100"
                        Width="230px"></asp:TextBox>
                </td>
                <td class="detailTitle">
                    Fee Structure:
                </td>
                <td class="detailData">
                    <asp:DropDownList ID="cboFeeStructure" runat="server" CssClass="forms" Width="224px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;Construction Type:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtConstructionType" runat="server" CssClass="forms" MaxLength="100"
                        Width="230px"></asp:TextBox>
                </td>
                <td class="detailTitle">
                    Project Type:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtProjectType" runat="server" CssClass="forms" MaxLength="100"
                        Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    &nbsp;Project Phase:
                </td>
                <td class="detailData">
                    <asp:DropDownList ID="cboPhase" runat="server" CssClass="forms" Width="234px">
                    </asp:DropDownList>
                </td>
                <td class="detailTitle">
                    Remarks:
                </td>
                <td class="detailData">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="forms" MaxLength="50" Rows="5"
                        TextMode="MultiLine" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="errors"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    &nbsp;<input class="formButton" id="btnClose" onclick="javascript:window.close();" type="button"
                        value="Close Window" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" CssClass="formButton" Text="Update Project" OnClick="btnUpdate_Click" /><img
                        src="images/spacer.gif" width="5" />
                </td>
            </tr>
            <tr>
                <td colspan="4" height="1">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
