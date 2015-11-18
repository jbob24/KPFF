<%@ Page Language="C#" MasterPageFile="~/MyAccount/BQReportMaster.master" AutoEventWireup="true" CodeBehind="~/MyAccount/FiscalSummary.aspx.cs" Inherits="KPFF.PMP.MyAccount.FiscalSummary" Title="Fiscal Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ReportProjectsGrid.ascx" TagName="ProjectList" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/ReportClientsGrid.ascx" TagName="ClientList" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/ReportPMsGrid.ascx" TagName="PMList" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/ReportPICsGrid.ascx" TagName="PICList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="Server">
    KPFF
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMetaTags" runat="Server">
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/nonSelectableList.css" />

    
    <%--<link rel="Stylesheet" type="text/css" href="../base.css" />--%>

    <style type="text/css">
        .tabContainer
        {
            margin-top: 10px;
        }
        .summaryGroup
        {
            margin-bottom: 10px;
        }
        .alternateRow
        {
            background-color: #D1D8F3;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/Service.svc" />
        </Services>
    </asp:ToolkitScriptManager>
    <div class="tabContainer">
        <asp:TabContainer ID="tabs" runat="server" AutoPostBack="True" OnActiveTabChanged="tabs_ActiveTabChanged">
            <asp:TabPanel ID="pmTab" runat="server" HeaderText="my fiscal summary">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Time Frame:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbPMTimeFrame" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="rbTimeFrame_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="PrevMonth">Previous Month</asp:ListItem>
                                                <asp:ListItem Value="CurYear">Current Fiscal Year</asp:ListItem>
                                                <asp:ListItem Value="AllOpen">All Open Projects</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlPMProjects" runat="server" GroupingText="My Projects" CssClass="summaryGroup" Visible="false">
                                    <uc:ProjectList ID="myPMProjects" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnlPMClients" runat="server" GroupingText="My Clients" CssClass="summaryGroup" Visible="false">
                                    <uc:ClientList ID="myPMClients" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnlPms" runat="server" GroupingText="My PMs" CssClass="summaryGroup" Visible="false">
                                    <uc:PMList ID="myPMs" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="picTab" runat="server" HeaderText="office fiscal summary">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Time Frame:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbPICTimeFrame" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="rbTimeFrame_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="PrevMonth">Previous Month</asp:ListItem>
                                                <asp:ListItem Value="CurYear">Current Fiscal Year</asp:ListItem>
                                                <asp:ListItem Value="AllOpen">All Open Projects</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlPICProjects" runat="server" GroupingText="All Projects" CssClass="summaryGroup" Visible="false">
                                    <uc:ProjectList ID="myPICProjects" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnlPICClients" runat="server" GroupingText="All Clients" CssClass="summaryGroup" Visible="false">
                                    <uc:ClientList ID="myPICClients" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnlPICs" runat="server" GroupingText="All PICs" CssClass="summaryGroup" Visible="false">
                                    <uc:PICList ID="myPICs" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <%--<script type="text/javascript" src="../HoursBox/jquery.fixheadertable.min.js"></script>--%>
    <script type="text/javascript" src="../HoursBox/jquery.fixheadertable_custom.js"></script>    
    <%--<script type="text/javascript" src="../HoursBox/jquery.hourbox.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.projectGrid').fixheadertable({
                colratio: [75, 345, 85, 85, 85, 85, 85, 85],
                height: 190,
                sortable: true,
                sortedColId: 1,
                sortType: ['integer', 'string', 'string', 'integer', 'integer', 'integer', 'integer', 'integer'],
                zebra: true,
                zebraClass: 'alternateRow'
            });

            $('.reportGrid').fixheadertable({
                colratio: [505, 85, 85, 85, 85, 85],
                height: 190,
                sortable: true,
                sortedColId: 0,
                sortType: ['string', 'integer', 'integer', 'integer', 'integer', 'integer'],
                zebra: true,
                zebraClass: 'alternateRow'
            });

            $('.hoursGrid tr:nth-child(even)').addClass('alternateRow');
           
           
            $('.head').addClass('hoursGrid').removeClass('head').css('border-spacing', '0px').css('border-padding', '0px');


            //            var service = new KPFFServices.Service();
            //            service.GetProjectsByPIC(84,'CurYear', onSuccess, null, null);
        });

//        function onSuccess(result){
//            alert(result);
//        }

    </script>
</asp:Content>
