<%@ Page Title="KPFF - All Engineers" Language="C#" MasterPageFile="~/MyAccount/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="AllEngineers.aspx.cs" Inherits="KPFF.Web.MyAccount.AllEngineers" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Src="~/UserControls/EngineerList.ascx" TagName="EngineerList" TagPrefix="el" %>
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
    <link rel="stylesheet" type="text/css" href="../Styles/wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/Style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/HourBox.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/nonSelectableList.css" />
    <style type="text/css">
        .HighlightNav
        {
            position: absolute;
            top: 47px;
            left: 461px;
            z-index: 1;
        }
        .PrevNext
        {
            position: absolute;
            top: 88px;
            left: 156px;
            z-index: 1;
        }
        .EmployeesFilter
        {
            position: absolute;
            z-index: 3;
            top: 105px;
            left: 10px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }        
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnDisplayFilters').click(function () {
                $('#EmployeesFilter').slideDown();
            });

            $('#btnHideFilters').click(function () {
                $('#EmployeesFilter').slideUp();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="EmployeesFilter" id="EmployeesFilter">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="formContent" width="60" align="right">
                    Start Date: &nbsp;
                </td>
                <td width="90">
                    <igsch:WebDateChooser ID="FilterStart" runat="server" Width="90px" AllowNull="False" ClientSideEvents-CalendarDateClicked="WebDateChooser1_CalendarDateClicked">
                        <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="../Images/igsch_right_arrow.gif"
                            PrevMonthImageUrl="../Images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
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
                    End Date: &nbsp;
                </td>
                <td width="90">
                    <igsch:WebDateChooser ID="FilterEnd" runat="server" Width="90px" AllowNull="False">
                        <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="../images/igsch_right_arrow.gif"
                            PrevMonthImageUrl="../images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
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
                    <img src="../Images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="formContent"  align="left">
                    Available Hours:&nbsp;
                    <asp:TextBox ID="FilterAvailHours" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="FilterAvailHoursValidator" runat="server" ErrorMessage="Invalid value" ForeColor="Red" Type="Double" ControlToValidate="FilterAvailHours"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" height="8">
                    <img src="../Images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <input type="button" value="Close" id="btnHideFilters" class="formButton" />
                    &nbsp;
                    <asp:Button CssClass="formButton" ID="btnFilterEmployees" runat="server" Text="Apply Filter" />
                    <asp:Button CssClass="formButton" ID="btnClearEmployeeFilters" runat="server" Text="Clear Filter" />
                </td>
            </tr>
        </table>
    </div>



    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <table class="navTable">
                    <tr>
                        <td><input type="button" class="formButton" value="Filter by Availability" id="btnDisplayFilters" /></td>
                        <td>
                            <input type="button" id="btnPrint" onclick="javascript:window.open('AllEngineersPrint.aspx');"
                                value="Print Expanded Report" class="formButton" />
                        </td>
                        <td>
                            <input type="button" class="formButton" value="Print" onclick="javascript: window.open('AllEngineers_PrintList.aspx','AllEngineersPrint','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=700, height=500, top=5, left=5');" />
                        </td>
                        <td>
                            <input type="button" onclick="javascript:window.location.reload();" value="Refresh"
                                class="formButtonRefresh" />
                        </td>
                        <td>
                            <img src="../Images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev"></asp:LinkButton>
                        </td>
                        <td>
                            <span class="search">Choose Date:</span>&nbsp;
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="wdtWeek" runat="server" Width="90px" AllowNull="False">
                                <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="../Images/igsch_right_arrow.gif"
                                    PrevMonthImageUrl="../Images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
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
                            <img src="../Images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:Button ID="btnThisWeek" runat="server" Text="This Week" CssClass="formButton" />
                        </td>
                        <td>
                            <img src="../Images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnNext" CssClass="nextweek" runat="server" Text="next >>"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" height="100%">
                    <tr>
                        <td height="100%" valign="top">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="top">
                                        <el:EngineerList ID="hoursGrid" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


    <script type="text/javascript" src="../Scripts/HoursBox/jquery.hourbox2.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.hoursGrid').hourbox2({
                editable: false,
                sortable: true,
                sortType: ['link', 'date', 'string', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer'],
                colresizable: true,
                curweekstyle: 'currentWeekCell',
                alternaterowclass: 'alternateRow',
                scrollable: true,
                height: 400
            });
        });
    </script>


    <%--<script type="text/javascript" src="../_scripts/jquery.tablesorter.js"></script>--%>
    <%--<script type="text/javascript" src="../HoursBox/NonEditableHourBox.js"></script>--%>
    <script type="text/javascript" src="../Scripts/HoursBox/tableSearch.js"></script>
    <script type="text/javascript" src="../Scripts/EnterToTab.js"></script>

    <script type="text/javascript">
        function WebDateChooser1_CalendarDateClicked(oCalendar, oDate, oEvent) {
            var endDateCal = $('input[id*="FilterEnd_input"]');

            if (endDateCal.length > 0) {
                var dow = oDate.getDay();
                var endDate = new Date(oDate.toDateString());

                if (dow < 6) {
                    endDate.setDate(endDate.getDate() + (6 - (dow + 1)));
                }
                else {
                    endDate.setDate(endDate.getDate() + 6);
                }
                igdrp_getComboById($('input[id*="FilterEnd_input"]')[0].id).setValue(endDate, true);
            }

        }    
    </script>
</asp:Content>
