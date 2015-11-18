<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/ProjectDetail.apsx.cs" Inherits="KPFF.PMP.MyAdmin.ProjectDetail" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%--<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>--%>
<%@ Register Src="~/UserControls/EditableEngineerList.ascx" TagName="EditableGrid"
    TagPrefix="eg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Summary</title>
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
<%--    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />
    <style>
        .DetailsDiv
        {
            position: absolute;
            z-index: 3;
            top: 55px;
            left: 10px;
            width: 792px;
            padding: 3px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
        }
        .EmployeeAdd
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
            left: 160px;
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
        function BeforeDelete(gridname, rowid) {
            //show the confirm box
            del = confirm("Are you sure you want to delete this record?");
            //set deleting to true so confirm is not shown again
            deleting = true

            return !del;
        }

        function BeforeRowUpdate(gridName, rowId) {
            //show the confirm box
            upd = confirm("Are you sure you want to update this record?");
            //set deleting to true so confirm is not shown again
            updating = true

            return !upd;
        }
    </script>
    <script src="../_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../_scripts/jquery-ui-1.8.7.custom.min.js" type="text/javascript"></script>
    <script src="../_scripts/confirm.js" type="text/javascript"></script>
    <script src="../_scripts/projectDetails.js" type="text/javascript"></script>
    <script type="text/javascript" src="../_scripts/ListSearch.js"></script>
    <script type="text/javascript" src="../_scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../HoursBox/HourBox.js"></script>
    <script type="text/javascript" src="../_scripts/EnterToTab.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnEmployeeAdd').click(function () {
                $('#EmployeeReAssign').slideUp();
                $('#EmployeeAdd').slideDown();
            });

            $('#btnCloseEmployeeAdd').click(function () {
                $('#EmployeeAdd').slideUp();
            });

            $('#btnReAssignProject').click(function () {
                $('#EmployeeAdd').slideUp();
                $('#EmployeeReAssign').slideDown();
            });

            $('#btnCloseReassignEmployee').click(function () {
                $('#EmployeeReAssign').slideUp();
            });
        });
    </script>
<%--    <script language="Javascript">
        function ShowDiv(vDiv) {
            if (form1.document.getElementById('' + vDiv + '').style.visibility == 'visible') {
                form1.document.getElementById('' + vDiv + '').style.visibility = 'hidden'
            }
            else {
                form1.document.getElementById('' + vDiv + '').style.visibility = 'visible'
            }
        }
        function ShowDivAdd() {
            if (form1.document.getElementById('EmployeeAdd').style.visibility == 'visible') {
                form1.document.getElementById('EmployeeAdd').style.visibility = 'hidden'
                form1.document.getElementById('EmployeeReAssign').style.visibility = 'hidden'
            }
            else {
                form1.document.getElementById('EmployeeAdd').style.visibility = 'visible'
                form1.document.getElementById('EmployeeReAssign').style.visibility = 'hidden'
            }
        }

        function ShowDivReAssign() {
            if (form1.document.getElementById('EmployeeReAssign').style.visibility == 'visible') {
                form1.document.getElementById('EmployeeReAssign').style.visibility = 'hidden'
                form1.document.getElementById('EmployeeAdd').style.visibility = 'hidden'
            }
            else {
                form1.document.getElementById('EmployeeReAssign').style.visibility = 'visible'
                form1.document.getElementById('EmployeeAdd').style.visibility = 'hidden'
            }
        }

        function BeforeDelete(gridname, rowid) {
            //show the confirm box
            del = confirm("Are you sure you want to delete this record?");
            //set deleting to true so confirm is not shown again
            deleting = true

            return !del;
        }

        function BeforeRowUpdate(gridName, rowId) {
            //show the confirm box
            upd = confirm("Are you sure you want to update this record?");
            //set deleting to true so confirm is not shown again
            updating = true

            return !upd;
        }    

    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div class="EmployeeAdd" id="EmployeeAdd">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="detailTitle">
                    <asp:DropDownList ID="cboEmployeeName" CssClass="forms" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="4">
                    <img src="images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input type="button" id="btnCloseEmployeeAdd" value="Close" class="formButton" />
                    &nbsp;
                    <asp:Button CssClass="formButton" ID="btnAddEmployee" runat="server" Text="Assign" OnClick="btnAddEmployee_Click" />
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
                    <input type="button" value="Close" class="formButton" id="btnCloseReassignEmployee" />
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
                            <b>Project Summary</b>
                        </td>
                        <td align="right" colspan="3">
                            <input type="button" class="formButton" value="View/Edit Project Details" onclick="javascript: window.open('ProjectDetailEdit.aspx?PID=<%Response.Write(Request.Params["PID"]); %>','ProjectEdit','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=793, height=310, top=5, left=5');" />
                        </td>
                    </tr>
                    <tr>
                        <td width="120">
                            <img src="images/spacer.gif" width="120" height="5" border="0" alt="" />
                        </td>
                        <td width="276">
                            <img src="images/spacer.gif" width="276" height="1" border="0" alt="" />
                        </td>
                        <td width="115">
                            <img src="images/spacer.gif" width="115" height="1" border="0" alt="" />
                        </td>
                        <td width="281">
                            <img src="images/spacer.gif" width="281" height="1" border="0" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td class="detailTitle">
                                        <b>Project Name:</b>
                                    </td>
                                    <td class="detailData">
                                        <b>
                                            <asp:Label ID="lblProjectName" runat="server"></asp:Label></b>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td class="detailTitle">
                                        <b>Last Updated:</b>
                                    </td>
                                    <td class="detailData">
                                        <b>
                                            <asp:Label ID="lblLastUpdated" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detailTitle">
                                        <b>Project #:</b>
                                    </td>
                                    <td colspan="4" class="detailData">
                                        <b>
                                            <asp:Label ID="lblProjectNo" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="DetailsDiv" id="DetailsDiv" style="visibility: hidden">
                    <table width="792" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="120" class="detailTitle">
                                PIC:
                            </td>
                            <td width="276" class="detailData">
                                <asp:Label ID="lblPIC" runat="server"></asp:Label>
                            </td>
                            <td width="115" class="detailTitle">
                                PM:
                            </td>
                            <td width="281" class="detailData">
                                <asp:Label ID="lblPM" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Est. Start of Const.:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblEstStartOfConstruction" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Est. End of Const.:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblEstCompletionOfConstruction" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Client Name:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblClientName" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Fee:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblFeeAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Project Location:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblProjectLocation" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Fee Structure:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblFeeStructure" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Construction Type:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblConstructionType" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Project Type:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblProjectType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailTitle">
                                Project Phase:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblPhase" runat="server"></asp:Label>
                            </td>
                            <td class="detailTitle">
                                Remarks:
                            </td>
                            <td class="detailData">
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" height="5">
                                <img src="images/spacer.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <input type="button" value="Close" onclick="javascript: form1.document.getElementById('DetailsDiv').style.visibility='hidden'"
                                    class="formButton" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
<%--        <tr>
            <td height="18">
                <img src="images/spacer.gif" />
            </td>
        </tr>--%>
        <tr>
            <td>
                <table class="navTable">
                    <tr>
                        <td>
                            <input type="button" class="formButton" value="Assign Employee" onclick="ShowAssignEmployee();"
                                id="btnEmployeeAdd" />
                        </td>
                        <td>
                            <input type="button" class="formButton" value="Re-Assign Future Hours" id="btnReAssignProject" />
                        </td>
                        <td>
                            <asp:Button ID="btnUnAssignEmployees" runat="server" CssClass="formButton" OnClientClick="return ConfirmEmployeeUnassign();"
                                Text="Unassign Employees" OnClick="btnUnAssignEmployees_Click" />
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
<%--        <tr>
            <td align="right">
                <table cellpadding="0" cellspacing="0" border="0" width="810">
                    <tr>
                        <td width="435" align="left">
                            <input type="button" class="formButton" value="Assign Employee" onclick="ShowDivAdd();" />
                            &nbsp;<input type="button" class="formButton" value="Re-Assign Future Hours" onclick="ShowDivReAssign();" />
                        </td>
                        <td align="left" style="width: 43px">
                            <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev"></asp:LinkButton>
                        </td>
                        <td style="width: 111px" align="right">
                            <span class="search">Choose Date:</span>&nbsp;
                        </td>
                        <td style="width: 114px" align="left">
                            <igsch:WebDateChooser ID="wdtWeek" runat="server" Width="90px" AllowNull="False">
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
                                <AutoPostBack ValueChanged="True" />
                            </igsch:WebDateChooser>
                        </td>
                        <td width="70">
                            <asp:Button ID="btnThisWeek" runat="server" Text="This Week" CssClass="formButton" />
                        </td>
                        <td align="right" style="width: 110px">
                            <asp:LinkButton ID="btnNext" CssClass="nextweek" runat="server" Text="next >>"></asp:LinkButton>
                        </td>
                        <td width="6">
                            <img src="images/spacer.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td>
             <eg:EditableGrid ID="hoursGrid" runat="server" />
<%--                <igtbl:ultrawebgrid id="uwgProjects" runat="server" width="800px" height="200px">
                        <Bands>
                            <igtbl:UltraGridBand AddButtonCaption="Column0Column1Column2" Key="Column0Column1Column2" AllowAdd="Yes">
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowDeleteDefault="Yes" AllowUpdateDefault="Yes"
                            HeaderClickActionDefault="SortMulti" name="uwgProjects" RowHeightDefault="20px" SelectTypeRowDefault="Single" Version="4.00" AllowAddNewDefault="Yes" StationaryMargins="Footer" IndentationTypeDefault="Flat" TableLayout="Fixed" AllowSortingDefault="Yes">
                            <HeaderStyleDefault CssClass="HeaderStyle"></HeaderStyleDefault>
                            <FrameStyle CssClass="FrameStyle" Height="210px" Width="810px"></FrameStyle>
                            <SelectedRowStyleDefault CssClass="SelectedRowStyle"></SelectedRowStyleDefault>
                            <RowAlternateStyleDefault CssClass="AlternateRowStyle"></RowAlternateStyleDefault>
                            <RowStyleDefault CssClass="RowStyle" TextOverflow="Ellipsis"></RowStyleDefault>
                            <ImageUrls CollapseImage="../images/MinusBox.jpg" CurrentEditRowImage="../images/Arrow.jpg"
                                CurrentRowImage="../images/Arrow.jpg" ExpandImage="../images/PlusBox.jpg" />
                            <AddNewBox View="Compact" Location="Top">
                                <Style CssClass="AddNewBoxStyle"></Style>
                            </AddNewBox>
                            <RowSelectorStyleDefault CssClass="RowSelectorStyle">
                            </RowSelectorStyleDefault>
                            <FixedCellStyleDefault CssClass="FixedCellStyle">
                            </FixedCellStyleDefault>
                            <RowExpAreaStyleDefault CssClass="RowExpAreaStyle"></RowExpAreaStyleDefault>
                            <Pager PageSize="25" Alignment="Center">
                                <Style CssClass="PagerStyle"></Style>
                            </Pager>
                            <ActivationObject BorderColor="Red" />
                        </DisplayLayout>
                    </igtbl:ultrawebgrid>--%>
            </td>
        </tr>
        <tr>
            <td height="5">
                <img src="images/spacer.gif" />
            </td>
        </tr>
<%--        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CssClass="formButtonUpdate" Text="Delete" />
                            &nbsp;
                            <asp:Button ID="btnActiveInactive" runat="server" CssClass="formButton" Text="Make Inactive" /><img
                                src="images/spacer.gif" height="1" width="18" />
                        </td>
                        <td align="right">
                            <input type="button" class="formButton" value="Print" onclick="javascript: window.open('ProjectDetail_Print.aspx?PID=<%response.write(request.params("PID")) %>','ProjectSummaryPrint','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=700, height=500, top=5, left=5');" />
                            &nbsp;
                            <asp:Button ID="btnUpdateGrid" runat="server" CssClass="formButtonUpdate" Text="Update" />
                            &nbsp;
                            <input type="button" value="Close" class="formButton" onclick="javascript:window.close()" /><img
                                src="images/spacer.gif" height="1" width="18" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
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
