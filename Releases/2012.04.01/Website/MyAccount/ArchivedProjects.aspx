<%@ Page Language="C#" MasterPageFile="~/MyAccount/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="~/MyAccount/ArchivedProjects.aspx.cs" Inherits="KPFF.PMP.MyAccount.ArchivedProjects" Title=" Archived Projects" %>


<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
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
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
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
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <span class="search">Filter by: </span>
                            <asp:DropDownList ID="ddlFilter" CssClass="forms" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="forms" Width="100px"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="formButton" OnClick="btnGo_Click" />
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
                        <td>
                            <asp:Button ID="btnRestoreProjects" runat="server" CssClass="formButton" Text="Restore Projects" OnClick="btnRestoreProjects_Click" />
                        </td>
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
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev" OnClick="btnPrevious_Click"></asp:LinkButton>
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
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" height="100%">
                    <tr>
                        <td width="985" height="100%" valign="top">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="top">
                                        <igtbl:UltraWebGrid ID="uwgProjects" runat="server" DataKeyField="ProjectID" Width="980px"
                                            Height="463px" OnInitializeLayout="uwgProjects_InitializeLayout" OnInitializeRow="uwgProjects_InitializeRow" OnPageIndexChanged="uwgProjects_PageIndexChanged" OnUpdateGrid="uwgProjects_UpdateGrid">
                                            <Bands>
                                                <igtbl:UltraGridBand AddButtonCaption="Column0Column1Column2" Key="Column0Column1Column2"
                                                    AllowAdd="Yes">
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout AllowColSizingDefault="Free" HeaderClickActionDefault="SortMulti"
                                                AllowUpdateDefault="Yes" Name="uwgProjects" RowHeightDefault="20px" SelectTypeRowDefault="Extended"
                                                Version="4.00" StationaryMargins="Header" IndentationTypeDefault="Flat" TableLayout="Fixed"
                                                AllowSortingDefault="Yes">
                                                <HeaderStyleDefault CssClass="HeaderStyle">
                                                </HeaderStyleDefault>
                                                <FrameStyle CssClass="FrameStyle" Height="463px">
                                                </FrameStyle>
                                                <SelectedRowStyleDefault CssClass="SelectedRowStyle">
                                                </SelectedRowStyleDefault>
                                                <RowAlternateStyleDefault CssClass="AlternateRowStyle">
                                                </RowAlternateStyleDefault>
                                                <RowStyleDefault CssClass="RowStyle" TextOverflow="Ellipsis">
                                                </RowStyleDefault>
                                                <ImageUrls CollapseImage="../images/MinusBox.jpg" CurrentEditRowImage="../images/Arrow.jpg"
                                                    CurrentRowImage="../images/Arrow.jpg" ExpandImage="../images/PlusBox.jpg" />
                                                <AddNewBox View="Compact" Location="Top">
                                                    <Style CssClass="AddNewBoxStyle"></Style>
                                                </AddNewBox>
                                                <RowSelectorStyleDefault CssClass="RowSelectorStyle">
                                                </RowSelectorStyleDefault>
                                                <FixedCellStyleDefault CssClass="FixedCellStyle">
                                                </FixedCellStyleDefault>
                                                <RowExpAreaStyleDefault CssClass="RowExpAreaStyle">
                                                </RowExpAreaStyleDefault>
                                                <Pager PageSize="25" Alignment="Center">
                                                    <Style CssClass="PagerStyle"></Style>
                                                </Pager>
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
