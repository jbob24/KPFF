<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportClientsGrid.ascx.cs"
    Inherits="KPFF.PMP.UserControls.ReportClientsGrid" %>
<asp:GridView ID="gridClients" runat="server" AutoGenerateColumns="false" CssClass="hoursGrid reportGrid"
    DataKeyNames="ClientID"
    EmptyDataText="There are no data records to display." EnableViewState="False">
    <HeaderStyle CssClass="headerStyle headerFormat" />
    <FooterStyle CssClass="footerStyle" />
    <AlternatingRowStyle CssClass="alternateRow" />
    <Columns>        
        <asp:BoundField DataField="ClientName" HeaderStyle-Width="398px" HeaderStyle-CssClass="projectHeader" HeaderText="Client Name" />
        <asp:BoundField DataField="FeeString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Fee" />
        <asp:BoundField DataField="CostString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Cost" />
        <asp:BoundField DataField="BillingString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing" />
        <asp:BoundField DataField="ProfitString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing - Cost" />
        <asp:BoundField DataField="ARString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="A/R" />
    </Columns>
</asp:GridView>
