<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/EmployeeDetail.apsx.cs" Inherits="KPFF.PMP.MyAdmin.EmployeeDetail" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Src="~/UserControls/EditableEngineerProjectList.ascx" TagName="EditableGrid"
    TagPrefix="eg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Detail</title>
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />
    <style>
        .DetailsDiv
        {
            position: absolute;
            z-index: 3;
            top: 55px;
            left: 10px;
            padding: 3px;
            width: 792px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
        .ProjectsAdd
        {
            position: absolute;
            z-index: 3;
            top: 91px;
            left: 10px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
        .EmployeeReAssign
        {
            position: absolute;
            z-index: 3;
            top: 91px;
            left: 137px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
    </style>
    <script src="../_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../_scripts/jquery-ui-1.8.7.custom.min.js" type="text/javascript"></script>
    <script src="../_scripts/confirm.js" type="text/javascript"></script>
    <script type="text/javascript" src="../_scripts/ListSearch.js"></script>
    <script type="text/javascript" src="../_scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../HoursBox/HourBox.js"></script>
    <script type="text/javascript" src="../_scripts/EnterToTab.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAssignProject').click(function () {
                $('#EmployeeReAssign').slideUp();
                $('#ProjectsAdd').slideDown();
            });

            $('#btnCloseAddProject').click(function () {
                $('#ProjectsAdd').slideUp();
            });

            $('#btnReAssignProject').click(function () {
                $('#ProjectsAdd').slideUp();
                $('#EmployeeReAssign').slideDown();
            });

            $('#btnCloseReAssignEmployee').click(function () {
                $('#EmployeeReAssign').slideUp();
            });

            $('#btnShowEmployeeDetails').click(function () {
                $('#ProjectsAdd').slideUp();
                $('#EmployeeReAssign').slideUp();
                $('#DetailsDiv').slideDown();
            });

            $('#btnCloseEmployeeDetails').click(function () {
                $('#DetailsDiv').slideUp();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="ProjectsAdd" id="ProjectsAdd">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="detailTitle">
                    Search:
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    <input id="project" type="text" onkeyup="filterList(this.value)" />
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    <asp:RadioButton ID="rbByName" class="rbByName" runat="server" Text="Order By Name"
                        Checked="true" GroupName="projectsGroup" />
                    <asp:RadioButton ID="rbByNumber" class="rbByNumber" runat="server" Text="Order By Number"
                        GroupName="projectsGroup" />
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    <div id="projectLists">
                        <div id="projectsByName">
                            <asp:ListBox ID="cboProjectsByName" Rows="10" CssClass="forms projectsByName" runat="server" />
                        </div>
                        <div id="projectsByNumber" style="display: none">
                            <asp:ListBox ID="cboProjectsByNumber" Rows="10" CssClass="forms projectsByNumber"
                                runat="server" />
                        </div>
                        <div id="projectList" style="display: none">
                            <select id="projectSelect" size="10" onchange="modifySelection();">
                            </select>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="4">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input type="button" value="Close" id="btnCloseAddProject" class="formButton" />
                    &nbsp;
                    <asp:Button CssClass="formButton" ID="btnAddProject" runat="server" Text="Assign" OnClick="btnAddProject_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="EmployeeReAssign" id="EmployeeReAssign">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="formContent">
                    Engineer:
                </td>
                <td colspan="3" align="right">
                    <asp:DropDownList ID="cboEmployee" CssClass="forms" runat="server" Width="240">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" height="8">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="images/spacer.gif" />
                </td>
                <td colspan="3" align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="All Weeks" CssClass="detailTitle"
                        Checked="false" />
                </td>
            </tr>
            <tr>
                <td colspan="4" height="8">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td class="formContent" width="60" align="right">
                    From: &nbsp;
                </td>
                <td width="90">
                    <igsch:WebDateChooser ID="wdtFrom" runat="server" Width="90px" AllowNull="False">
                        <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="./images/igsch_right_arrow.gif"
                            PrevMonthImageUrl="./images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
                            ShowYearDropDown="False" CellPadding="5">
                            <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" BackColor="White" BorderColor="#7F9DB9" BorderStyle="Solid"
                                Font-Names="Tahoma,Verdana" Font-Size="8pt" Height="150px">
                            </CalendarStyle>
                            <TodayDayStyle BackColor="#FBE694" />
                            <SelectedDayStyle BackColor="Transparent" ForeColor="Black" BorderColor="#BB5503"
                                BorderStyle="Solid" BorderWidth="2px" />
                            <OtherMonthDayStyle ForeColor="#ACA899" />
                            <DayHeaderStyle>
                                <BorderDetails ColorBottom="172, 168, 153" StyleBottom="Solid" WidthBottom="1px" />
                            </DayHeaderStyle>
                            <TitleStyle BackColor="#9EBEF5"></TitleStyle>
                        </CalendarLayout>
                        <EditStyle Font-Names="Tahoma" Font-Size="8pt">
                        </EditStyle>
                    </igsch:WebDateChooser>
                </td>
                <td class="formContent" width="60" align="right">
                    To: &nbsp;
                </td>
                <td width="90">
                    <igsch:WebDateChooser ID="wdtTo" runat="server" Width="90px" AllowNull="False">
                        <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="./images/igsch_right_arrow.gif"
                            PrevMonthImageUrl="./images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
                            ShowYearDropDown="False" CellPadding="5">
                            <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" BackColor="White" BorderColor="#7F9DB9" BorderStyle="Solid"
                                Font-Names="Tahoma,Verdana" Font-Size="8pt" Height="150px">
                            </CalendarStyle>
                            <TodayDayStyle BackColor="#FBE694" />
                            <SelectedDayStyle BackColor="Transparent" ForeColor="Black" BorderColor="#BB5503"
                                BorderStyle="Solid" BorderWidth="2px" />
                            <OtherMonthDayStyle ForeColor="#ACA899" />
                            <DayHeaderStyle>
                                <BorderDetails ColorBottom="172, 168, 153" StyleBottom="Solid" WidthBottom="1px" />
                            </DayHeaderStyle>
                            <TitleStyle BackColor="#9EBEF5"></TitleStyle>
                        </CalendarLayout>
                        <EditStyle Font-Names="Tahoma" Font-Size="8pt">
                        </EditStyle>
                    </igsch:WebDateChooser>
                </td>
            </tr>
            <tr>
                <td colspan="4" height="8">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <input type="button" value="Close" class="formButton" id="btnCloseReAssignEmployee" />
                    &nbsp;
                    <asp:Button CssClass="formButton" ID="btnReAssignEmployee" runat="server" Text="Re-Assign" OnClick="btnReAssignEmployee_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="810" valign="top">
                <table width="792" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" nowrap="nowrap">
                            <b>Employee Summary</b>
                        </td>
                        <td align="right" colspan="3">
                            <input class="formButton" type="button" value="Employee Details" id="btnShowEmployeeDetails" />
                        </td>
                    </tr>
                    <tr>
                        <td width="150">
                            <img src="images/spacer.gif" width="150" height="5" border="0" alt="" />
                        </td>
                        <td width="246">
                            <img src="images/spacer.gif" width="246" height="1" border="0" alt="" />
                        </td>
                        <td width="150">
                            <img src="images/spacer.gif" width="150" height="1" border="0" alt="" />
                        </td>
                        <td width="246">
                            <img src="images/spacer.gif" width="246" height="1" border="0" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td class="detailTitle">
                                        <b>Name:</b>
                                    </td>
                                    <td class="detailData">
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td class="detailTitle">
                                        <b>Last Updated:</b>
                                    </td>
                                    <td class="detailData">
                                        <asp:Label ID="lblLastUpdated" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="DetailsDiv" id="DetailsDiv">
                    <table width="792" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="150" class="detailTitle">
                                Title:
                            </td>
                            <td width="246" class="detailData">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                            <td width="150" class="detailTitle">
                                Licenses:
                            </td>
                            <td width="246" class="detailData">
                                <asp:Label ID="lblLicenses" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                KPFF Start Date:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblEmployeeStartDate" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Education:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblEducation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Years of Experience:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblYearsOfExperience" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Remarks:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Prof. Memberships:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblProfessionalMemberships" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                            </td>
                            <td class="detailData">
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Prof. Committees:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblProfessionalCommittees" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Hours Per Week:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblHoursPerWeek" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Phone Extension:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblPhoneExtension" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Email Address:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" height="5">
                                <img src="images/spacer.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <input type="button" value="Close" class="formButton" id="btnCloseEmployeeDetails" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table class="navTable">
                    <tr>
                        <td>
                            <input type="button" class="formButton" value="Assign Project" id="btnAssignProject" />
                        </td>
                        <td>
                            <input type="button" class="formButton" value="Re-Assign Project" id="btnReAssignProject" />
                        </td>
                        <td>
                            <asp:Button ID="btnUnAssignProjects" runat="server" CssClass="formButton" OnClientClick="return ConfirmUnassign();"
                                Text="Unassign Projects" OnClick="btnUnAssignProjects_Click" />
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev" OnClick="btnPrevious_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <span class="search">Choose Date:</span>
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="wdtWeek" runat="server" Width="90px" AllowNull="False" OnValueChanged="wdtWeek_ValueChanged">
                                <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="./images/igsch_right_arrow.gif"
                                    PrevMonthImageUrl="./images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
                                    ShowYearDropDown="False" CellPadding="5">
                                    <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" BackColor="White" BorderColor="#7F9DB9" BorderStyle="Solid"
                                        Font-Names="Tahoma,Verdana" Font-Size="8pt" Height="150px">
                                    </CalendarStyle>
                                    <TodayDayStyle BackColor="#FBE694" />
                                    <SelectedDayStyle BackColor="Transparent" ForeColor="Black" BorderColor="#BB5503"
                                        BorderStyle="Solid" BorderWidth="2px" />
                                    <OtherMonthDayStyle ForeColor="#ACA899" />
                                    <DayHeaderStyle>
                                        <BorderDetails ColorBottom="172, 168, 153" StyleBottom="Solid" WidthBottom="1px" />
                                    </DayHeaderStyle>
                                    <TitleStyle BackColor="#9EBEF5"></TitleStyle>
                                </CalendarLayout>
                                <AutoPostBack ValueChanged="True" />
                                <EditStyle Font-Names="Tahoma" Font-Size="8pt">
                                </EditStyle>
                            </igsch:WebDateChooser>
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:Button ID="btnThisWeek" runat="server" Text="This Week" CssClass="formButton" OnClick="btnThisWeek_Click" />
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnNext" CssClass="nextweek" runat="server" Text="next >>" OnClick="btnNext_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <eg:EditableGrid ID="hoursGrid" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="5">
                <img src="images/spacer.gif" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button ID="btnUpdateGrid" runat="server" CssClass="formButtonUpdate" Text="Update" OnClick="btnUpdateGrid_Click" />
                <input type="button" value="Cancel" onclick="CancelEdit();" id="btnCancelEdit" class="formButtonUpdate" />
                <input type="button" class="formButton" value="Print" onclick="javascript: window.open('ProjectDetail_Print.aspx?PID=<%Response.Write(Request.Params["PID"]); %>','ProjectSummaryPrint','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=700, height=500, top=5, left=5');" />
                <input type="button" value="Close" class="formButton" onclick="javascript:window.close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
