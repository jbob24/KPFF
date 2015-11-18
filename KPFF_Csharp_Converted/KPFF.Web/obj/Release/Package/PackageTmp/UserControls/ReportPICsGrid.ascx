<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportPICsGrid.ascx.cs"
    Inherits="KPFF.PMP.UserControls.ReportPICsGrid" %>
<asp:GridView ID="picProjects" runat="server" AutoGenerateColumns="false" CssClass="hoursGrid reportGrid"
    DataKeyNames="ProjectID"
    EmptyDataText="There are no data records to display." EnableViewState="False">
    <HeaderStyle CssClass="headerStyle headerFormat" />
    <FooterStyle CssClass="footerStyle" />
    <AlternatingRowStyle CssClass="alternateRow" />
    <Columns>        
        <asp:BoundField DataField="PIC" HeaderStyle-Width="398px" HeaderStyle-CssClass="projectHeader" HeaderText="PIC Name" />
        <asp:BoundField DataField="FeeString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Fee" />
        <asp:BoundField DataField="CostString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Cost" />
        <asp:BoundField DataField="BillingString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing" />
        <asp:BoundField DataField="ProfitString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing - Cost" />
        <asp:BoundField DataField="ARString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="A/R" />   
    </Columns>
</asp:GridView>
