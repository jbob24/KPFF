<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportProjectsGrid.ascx.cs"
    Inherits="KPFF.PMP.UserControls.ReportProjectsGrid" %>
<asp:GridView ID="gridProjects" runat="server" AutoGenerateColumns="false" CssClass="hoursGrid projectGrid"
    DataKeyNames="ProjectID"
    EmptyDataText="There are no data records to display." EnableViewState="False">
    <HeaderStyle CssClass="headerStyle headerFormat" />
    <FooterStyle CssClass="footerStyle" />
    <AlternatingRowStyle CssClass="alternateRow" />
    <Columns>        
        <asp:BoundField DataField="ProjectID" HeaderStyle-Width="70px" HeaderStyle-CssClass="projectHeader" HeaderText="Job #" />
        <asp:BoundField DataField="ProjectName" HeaderStyle-Width="323px" HeaderStyle-CssClass="projectHeader" HeaderText="Project" />
        <asp:BoundField DataField="PM" HeaderStyle-Width="71px" HeaderStyle-CssClass="projectHeader" HeaderText="PM" />
        <asp:BoundField DataField="PIC" HeaderStyle-Width="71px" HeaderStyle-CssClass="projectHeader" HeaderText="PIC" />
        <asp:BoundField DataField="FeeString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Fee" />
        <asp:BoundField DataField="CostString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Cost" />
        <asp:BoundField DataField="BillingString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing" />
        <asp:BoundField DataField="ProfitString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="Billing - Cost" />
        <asp:BoundField DataField="ARString" HeaderStyle-Width="71px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="projectHeader" HeaderText="A/R" />
    </Columns>
</asp:GridView>
