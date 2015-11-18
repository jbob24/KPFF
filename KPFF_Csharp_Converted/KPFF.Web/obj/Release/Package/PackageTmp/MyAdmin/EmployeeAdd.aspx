<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/EmployeeAdd.apsx.cs" Inherits="KPFF.PMP.MyAdmin.EmployeeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Employee</title>
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
        <table cellpadding="2" cellspacing="0" border="0" width="410" style="border-left: solid 1px #666699;
            border-right: solid 1px #666699; border-top: solid 1px #666699; border-bottom: solid 1px #666699">
            <tr>
                <td colspan="2" bgcolor="#000066" valign="middle">
                    &nbsp; <font style="color: #ffffff; font-size: 12px;"><b>Add New Employee</b></font>
                </td>
            </tr>
            <tr>
                <td width="150" class="formContent">
                    &nbsp; Employee Code:
                </td>
                <td width="260">
                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; First Name:
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Last Name:
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Employee Type:
                </td>
                <td>
                    <asp:DropDownList ID="cboEmployeeType" runat="server" CssClass="forms">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Address:
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; City:
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; State:
                </td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Zip:
                </td>
                <td>
                    <asp:TextBox ID="txtZip" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Home Phone:
                </td>
                <td>
                    <asp:TextBox ID="txtHomePhone" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Cell Phone:
                </td>
                <td>
                    <asp:TextBox ID="txtCellPhone" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Title:
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Emp. Start Date:
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeStartDate" runat="server" Width="250px" MaxLength="50"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Emp. End Date:
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeEndDate" runat="server" Width="250px" MaxLength="50"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Years of Experience:
                </td>
                <td>
                    <asp:TextBox ID="txtYearsOfExperience" runat="server" Width="250px" MaxLength="50"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Education:
                </td>
                <td>
                    <asp:TextBox ID="txtEducation" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Licenses:
                </td>
                <td>
                    <asp:TextBox ID="txtLicenses" runat="server" Width="250px" MaxLength="100" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Prof. Memberships:
                </td>
                <td>
                    <asp:TextBox ID="txtProfMemberships" runat="server" Width="250px" MaxLength="200"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Prof. Committees:
                </td>
                <td>
                    <asp:TextBox ID="txtProfCommittees" runat="server" Width="250px" MaxLength="500"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Hours Per Week:
                </td>
                <td>
                    <asp:TextBox ID="txtHoursPerWeek" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Comments:
                </td>
                <td>
                    <asp:TextBox ID="txtComments" runat="server" Rows="4" TextMode="MultiLine" Width="250px"
                        CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; Active:
                </td>
                <td>
                    <asp:RadioButton ID="rbtnActiveEmployeeYes" runat="server" Checked="True" GroupName="ActiveEmployee"
                        Text="Yes" CssClass="formContent" />&nbsp;
                    <asp:RadioButton ID="rbtnActiveEmployeeNo" runat="server" GroupName="ActiveEmployee"
                        Text="No" CssClass="formContent" />
                </td>
            </tr>
            <tr>
                <td class="formContent">
                    &nbsp; User Account:
                </td>
                <td>
                    <asp:RadioButton ID="rbtnUserYes" runat="server" Checked="True" GroupName="UserAccount" OnCheckedChanged="rbtnUserYes_CheckedChanged"
                        Text="Yes" AutoPostBack="True" CssClass="formContent" />&nbsp;
                    <asp:RadioButton ID="rbtnUserNo" runat="server" GroupName="UserAccount" Text="No" OnCheckedChanged="rbtnUserNo_CheckedChanged"
                        AutoPostBack="True" CssClass="formContent" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlUserAccount" runat="server" Width="400px">
                        <table cellpadding="0" cellspacing="0" border="0" width="400">
                            <tr>
                                <td class="formContent">
                                    &nbsp; User Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="formContent">
                                    &nbsp; Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
                    <input type="button" value="Close Window" onclick="javascript:window.close();" class="formButton" />&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add Employee" CssClass="formButton" OnClick="btnAdd_Click" /><img
                        src="images/spacer.gif" width="5" />
                </td>
            </tr>
            <tr>
                <td colspan="2" height="2">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
