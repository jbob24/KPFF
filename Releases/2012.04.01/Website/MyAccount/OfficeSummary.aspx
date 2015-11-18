<%@ Page Language="C#" MasterPageFile="~/MyAccount/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="~/MyAccount/OfficeSummary.aspx.cs" Inherits="KPFF.PMP.MyAccount.OfficeSummary" Title="Untitled Page" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
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
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet_Print.css" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../Style_Print.css" media="print" />
    <style type="text/css">
        .HighlightNav
        {
            position: absolute;
            top: 47px;
            left: 571px;
            z-index: 1;
        }
        .PrevNext
        {
            position: absolute;
            top: 88px;
            left: 176px;
            z-index: 1;
        }
        #chartDiv
        {
            display: none;
        }
        .Chart
        {
            padding-top: 5px;
            padding-bottom: 10px;
        }
    </style>
    <script type="text/javascript">
        function DisplayChart() {
            var chartButton = $("#displayChart");

            if (chartButton.val() == "Show Chart") {
                chartButton.val("Hide Chart");
                $("#chartDiv").slideDown();
            }
            else {
                chartButton.val("Show Chart");
                $("#chartDiv").slideUp();
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <table class="navTable">
                    <tr>
                        <td>
                            <input type="button" id="displayChart" onclick="DisplayChart();" value="Show Chart"
                                class="formButton" />
                        </td>
                        <td>
                            <span class="search">Display By: </span>
                        </td>
                        <td>
                            <asp:Button ID="btnHours" runat="server" Text="Hours" CssClass="formButton" Visible="true" OnClick="btnHours_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPeople" runat="server" Text="People" CssClass="formButton" OnClick="btnPeople_Click" />
                        </td>
                        <td>
                            <img src="images/spacer.gif" width="2" border="0" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev" OnClick="btnPrevious_Click">
                            </asp:LinkButton>
                        </td>
                        <td>
                            <span class="search">Choose Date:</span>&nbsp;
                        </td>
                        <td>
                            <igsch:webdatechooser id="wdtWeek" runat="server" width="90px" allownull="False" OnValueChanged="wdtWeek_ValueChanged">
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
                            </igsch:webdatechooser>
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
                            <asp:LinkButton ID="btnNext" CssClass="nextweek" runat="server" Text="next >>" OnClick="btnNext_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="chartDiv" class="Chart">
                    <asp:Image ID="imgChart" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" height="100%" width="100%">
                    <tr>
                        <td width="985" valign="top" style="height: 530px">
                            <asp:Table ID="tblEngineers" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
