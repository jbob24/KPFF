<%@ Page Language="C#" MasterPageFile="~/MyAccount/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="~/MyAccount/Projects.aspx.cs" Inherits="KPFF.PMP.MyAccount.Projects" Title="Projects" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%--<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>--%>
<%@ Register Src="~/UserControls/ProjectList.ascx" TagName="ProjectList" TagPrefix="pl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="Server">
    KPFF
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMetaTags" runat="Server">
    <meta http-equiv="content-type" content="text/html;charset=iso-8859-1" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta name="rating" content="General" />
    <meta name="expires" content="never" />
    <meta name="langauge" content="english" />
    <meta name="charset" content="ISO-8859-1" />
    <meta name="distribution" content="Global" />
    <meta name="robots" content="INDEX,FOLLOW" />
    <meta name="revisit-after" content="31 Days" />
    <meta name="publisher" content="Red Meat Design" />
    <meta name="copyright" content="Copyright ©2006 - XXXXXX. All Rights Reserved." />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/projectList.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/nonSelectableList.css" />    
    <style type="text/css">
        .FilterBy
        {
            position: absolute;
            top: 51px;
            left: 709px;
        }
        .PrevNext
        {
            position: absolute;
            top: 88px;
            left: 152px;
            z-index: 1;
        }
        .HighlightNav
        {
            position: absolute;
            top: 47px;
            left: 363px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="900px">
                    <tr>
                        <td>
                            <span class="search">Filter: </span>
<%--                            <asp:DropDownList ID="ddlFilter" CssClass="forms" runat="server">
                            </asp:DropDownList>--%>
                            <%--&nbsp;--%>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="forms" Width="100px"></asp:TextBox>&nbsp;
                            <%--<asp:Button ID="btnGo" runat="server" Text="Go" CssClass="formButton" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="navTable">
                    <tr>
                        <td>
                            <input type="button" style="width: 110px" value="Add New Project" class="formButton"
                                onclick="javascript: window.open('ProjectDetailAdd.aspx','Report','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=793, height=310, top=5, left=5');" />
                        </td>
                        <%--                        <td>
                            <asp:Button ID="btnArchiveProjects" runat="server" CssClass="formButton" Text="Archive Projects" />
                        </td>--%>
                        <td>
                            <input type="button" id="btnPrint" style="width: 140px" onclick="javascript:window.open('ProjectsPrint.aspx');"
                                value="Print Expanded Report" class="formButton" />
                        </td>
                        <td>
                            <input type="button" class="formButton" value="Print" onclick="javascript: window.open('Projects_PrintList.aspx','Projects_PrintList','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=850, height=650, top=5, left=5');" />
                        </td>
                        <td>
                            <input type="button" onclick="javascript:window.location.reload();" value="Refresh"
                                class="formButtonRefresh" />
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnPrevious" OnClick="btnPrevious_Click" CssClass="prevweek" runat="server" Text="<< prev"></asp:LinkButton>
                        </td>
                        <td>
                            <span class="search">Choose Date:</span>&nbsp;
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
                <%--    </div>--%>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" height="100%" width="100%">
                    <tr>
                        <td height="100%" valign="top">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="top">
                                        <pl:ProjectList ID="hoursGrid" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="../HoursBox/jquery.sortable.js"></script>
    <script type="text/javascript" src="../HoursBox/jquery.hourbox2.js"></script>
    <script type="text/javascript" src="../_scripts/jquery.dataTables.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('.hoursGrid').hourbox2({
                editable: false,
                sortable: true,
                sortType: ['integer', 'link', 'date', 'string', 'string', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer', 'integer'],
                colresizable: true,
                curweekstyle: 'currentWeekCell',
                alternaterowclass: 'alternateRow',
                scrollable: true,
                height: 400,
                maxColWidth: 200,
                multicolsort: true
            });

//            $('.hoursGrid').dataTable({
//                "sScrollY": "400px",
//                "bPaginate": false,
//                "bFilter": false,
//                "bInfo": false,
//                "asStripClasses": ['','alternateRow'],
//                "aaSorting": []
//            });
        });
    </script>

    <script type="text/javascript" src="../_scripts/jquery.tablesorter.js"></script>
    <%--<script type="text/javascript" src="../HoursBox/NonEditableHourBox.js"></script>--%>
    <script type="text/javascript" src="../HoursBox/tableSearch.js"></script>
    <script type="text/javascript" src="../_scripts/EnterToTab.js"></script>
</asp:Content>
