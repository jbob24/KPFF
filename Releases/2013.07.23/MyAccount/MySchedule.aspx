<%@ Page Title="" Language="C#" MasterPageFile="~/MyAccount/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="~/MyAccount/MySchedule.aspx.cs" Inherits="KPFF.PMP.MyAccount.MySchedule" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%--<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>--%>
<%--<%@ Register Src="~/HoursBox/HourBox.ascx" TagName="HourBox" TagPrefix="hb" %>--%>

<%@ Register Src="~/UserControls/EditableEngineerProjectList.ascx" TagName="EditableGrid" TagPrefix="eg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="Server">
    KPFF
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMetaTags" runat="Server">
    <meta http-equiv="content-type" content="text/html;charset=iso-8859-1">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="rating" content="General">
    <meta name="expires" content="never">
    <meta name="langauge" content="english">
    <meta name="charset" content="ISO-8859-1">
    <meta name="distribution" content="Global">
    <meta name="robots" content="INDEX,FOLLOW">
    <meta name="revisit-after" content="31 Days">
    <meta name="publisher" content="Red Meat Design">
    <meta name="copyright" content="Copyright ©2006 - XXXXXX. All Rights Reserved.">
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <%--<link rel="stylesheet" type="text/css" href="../HoursBox/scrollableTable.css" />--%>
    <style type="text/css">
        .HighlightNav
        {
            position: absolute;
            top: 47px;
            left: 256px;
            z-index: 1;
        }
        .ProjectsAdd
        {
            position: absolute;
            z-index: 3;
            top: 105px;
            left: 20px;
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
            top: 105px;
            left: 110px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
    </style>
    <style type="text/css">
        .ui-button
        {
            margin-left: -1px;
        }
        .ui-button-icon-only .ui-button-text
        {
            padding: 0.35em;
        }
        .ui-autocomplete-input
        {
            margin: 0;
            padding: 0.48em 0 0.47em 0.45em;
        }
    </style>
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

            $('#btnCloseReassignEmployee').click(function () {
                $('#EmployeeReAssign').slideUp();
            });
        });
    </script>
    <script type="text/javascript">
        function ConfirmUnassign() {
            if (confirm("Are you sure you want to unassign the selected projects?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="ProjectsAdd" id="ProjectsAdd">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="detailTitle">
                    Search for the project you wish to add or select it from the list below:
                </td>
            </tr>
            <tr>
                <td class="detailTitle">
                    <input id="project" type="text" onkeyup="filterList(this.value)" />
                    <%--                    <div id="selectedValue">
                    </div>--%>
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
                    <input type="button" value="Close" id="btnCloseReassignEmployee" class="formButton" />
                    &nbsp;
                    <asp:Button CssClass="formButton" ID="btnReAssignEmployee" runat="server" Text="Re-Assign" OnClick="btnReAssignEmployee_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" border="0">
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
                            <%--<asp:Button ID="btnArchiveProjects" runat="server" CssClass="formButton" Text="Archive Projects" />--%>
                            <asp:Button ID="btnUnAssignProjects" runat="server" CssClass="formButton" OnClientClick="return ConfirmUnassign();"
                                Text="Unassign Projects" OnClick="btnUnAssignProjects_Click" />
                        </td>
                        <td>
                            <input type="button" class="formButton" value="Print" onclick="javascript: window.open('MySchedule_Print.aspx','MySchedule','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=700, height=600, top=5, left=5');" />
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev" OnClick="btnPrevious_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <span class="search">Choose Date:</span>&nbsp;
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="wdtWeek" runat="server" AllowNull="False" OnValueChanged="wdtWeek_ValueChanged">
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
                        <td align="right">
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
    </table>
    <asp:Table ID="tblUpdateButton" runat="server">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">
                <asp:Button ID="btnUpdateGrid" runat="server" CssClass="formButtonUpdate" Text="Update" OnClick="btnUpdateGrid_Click" />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                <input type="button" value="Cancel" id="btnCancelEdit" class="formButtonUpdate" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <script type="text/javascript" src="../HoursBox/jquery.sortable.js"></script>
    <script type="text/javascript" src="../HoursBox/jquery.EditHourbox.js"></script>
    <%--<script type="text/javascript" src="../HoursBox/jquery.MultiColSort.js"></script>--%>
    <script type="text/javascript" src="../HoursBox/jquery.hourbox2.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.hoursGrid').hourbox2({
                editable: true,
                sortable: true,
                sortType: ['integer', 'integer', 'link', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox'],
                colresizable: true,
                curweekstyle: 'currentWeekCell',
                alternaterowclass: 'alternateRow',
                height: 400,
                multicolsort: true
            });
       });

    </script>


    <script type="text/javascript" src="../_scripts/ListSearch.js"></script>
    <%--<script type="text/javascript" src="../_scripts/jquery.tablesorter.js"></script>--%>
    <%--<script type="text/javascript" src="../HoursBox/HourBox.js"></script>--%>
    <%--<script type="text/javascript" src="../_scripts/EnterToTab.js"></script>--%>
</asp:Content>
