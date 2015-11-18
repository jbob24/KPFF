<%@ Page Language="C#" MasterPageFile="~/MyAdmin/_TemplateMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="~/MyAdmin/Clients.apsx.cs" Inherits="KPFF.PMP.MyAdmin.Clients" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" Runat="Server">
    KPFF - Administration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMetaTags" Runat="Server">
    <META HTTP-EQUIV="content-type" CONTENT="text/html;charset=iso-8859-1">
    <META NAME="keywords" CONTENT="">
    <META NAME="description" CONTENT="">
    <META NAME="rating" CONTENT="General">
    <META NAME="expires" CONTENT="never">
    <META NAME="langauge" CONTENT="english">
    <META NAME="charset" CONTENT="ISO-8859-1">
    <META NAME="distribution" CONTENT="Global">
    <META NAME="robots" CONTENT="INDEX,FOLLOW">
    <META NAME="revisit-after" CONTENT="31 Days">
    <META NAME="publisher" CONTENT="Red Meat Design">
    <META NAME="copyright" CONTENT="Copyright ©2006 - XXXXXX. All Rights Reserved.">
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../Style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../style_print.css" media="print" />
    <style type="text/css">
        .HighlightNav { position: absolute; top: 47px; left: 153px; z-index: 1; }        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td><b>>> Clients</b><img src="images/spacer.gif" width="606" height="1" /><input type="button" value="Add Client" onclick="window.open('ClientAdd.aspx');" class="formButton" /></td>
        </tr>
        <tr>
            <td height="5" align="center" colspan="2">             
                <asp:Label ID="lblGridError" runat="server" CssClass="errors" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-top:solid 1px #666699; border-left:solid 1px #666699; border-right:solid 1px #666699">
                    <tr>
                        <td class="HeaderStyle" width="289" align="center"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientName')">Name</a></td>
                        <td class="HeaderStyle" width="195" align="center"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">Client Type</a></td>
                        <td class="HeaderStyle" width="131" align="center"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$OfficePhone')">Office Phone</a></td>
                        <td class="HeaderStyle" width="116" align="center">Total Records: <%Response.Write(GetTotalRecords()); %></td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div class="ClientList" style="OVERFLOW: auto; border-bottom:solid 1px #666699; border-left:solid 1px #666699; border-right:solid 1px #666699;">
                    <asp:GridView ID="dgClients" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource" 
                    ShowHeader="False" RowStyle-CssClass="RowStyle" AlternatingRowStyle-CssClass="AlternateRowStyle" AllowSorting="True" 
                    EnableSortingAndPagingCallbacks="True" CellPadding="4" OnRowDeleting="dgClients_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"
                                SortExpression="ID" ShowHeader="False" Visible="False" />
                            <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName">
                                <ItemStyle Width="300px" BorderColor="#666699" BorderWidth="2px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ClientType" HeaderText="Client Type" SortExpression="ClientType">
                                <ItemStyle Width="200px" BorderColor="#666699" BorderWidth="2px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" SortExpression="OfficePhone">
                                <ItemStyle Width="140px" BorderColor="#666699" BorderWidth="2px"/>
                            </asp:BoundField>              
                            <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ClientEdit.aspx?CID={0}" Target="_blank">
                                <ItemStyle HorizontalAlign="center" Width="40px" BorderColor="#666699" BorderWidth="2px"/>
                            </asp:HyperLinkField>    
                            <asp:CommandField ShowDeleteButton="True" >
                                <ItemStyle HorizontalAlign="center" Width="50px" BorderColor="#666699" BorderWidth="2px"/>
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>  
                </div>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:KPFFSchedulingConnectionString %>"
                    SelectCommand="SELECT dbo.tblClients.ID, dbo.tblClients.ClientName, dbo.tblClientTypes.ClientType, dbo.tblClients.Address, dbo.tblClients.OfficePhone FROM dbo.tblClients INNER JOIN dbo.tblClientTypes ON dbo.tblClients.ClientTypeID = dbo.tblClientTypes.ID ORDER BY dbo.tblClients.ClientName" DeleteCommand="DELETE FROM [tblClients] WHERE [ID] = @ID" UpdateCommand="">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>              
            </td>
        </tr>
    </table>
</asp:Content>

