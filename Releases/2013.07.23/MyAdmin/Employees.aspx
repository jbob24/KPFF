<%@ Page Language="C#" MasterPageFile="~/MyAdmin/_TemplateMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Employees.apsx.cs" Inherits="KPFF.PMP.MyAdmin.Employees" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" Runat="Server">
KPFF Administration
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
        <td><b>>> Employees</b><img src="images/spacer.gif" width="560" height="1" /><input type="button" onclick="window.open('EmployeeAdd.aspx');" value="Add Employee" class="formButton" /></td>
    </tr>
    <tr>
        <td height="5"><img src="images/spacer.gif" /></td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" style="border-top:solid 1px #666699; border-left:solid 1px #666699; border-right:solid 1px #666699">
                <tr>
                    <td align="center" class="HeaderStyle" width="95"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgEmployees','Sort$EmployeeCode')">Employee Code</a></td>
                    <td align="center" class="HeaderStyle" width="133"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgEmployees','Sort$EmployeeFirst')">First Name</a></td>
                    <td align="center" class="HeaderStyle" width="207"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgEmployees','Sort$EmployeeLast')">Last Name</a></td>
                    <td align="center" class="HeaderStyle" width="133"><a href="javascript:__doPostBack('ctl00$cphMainContent$dgEmployees','Sort$EmployeeType')">Employee Type</a></td>
                    <td width="173" class="HeaderStyle" align="center">Total Records: <%Response.Write(GetTotalRecords()); %></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div class="EmployeeList" style="OVERFLOW: auto; border-bottom:solid 1px #666699; border-left:solid 1px #666699; border-right:solid 1px #666699">
                <asp:GridView ID="dgEmployees" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource" ShowHeader="False" RowStyle-CssClass="RowStyle" AlternatingRowStyle-CssClass="AlternateRowStyle" AllowSorting="True" EnableSortingAndPagingCallbacks="True" CellPadding="4" DataKeyNames="EmployeeID" OnRowDataBound="dgEmployees_RowDataBound" OnRowDeleting="dgEmployees_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="EmployeeID" Visible="False" SortExpression="EmployeeID" />
                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" >
                            <ItemStyle Width="93px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="EmployeeFirst" HeaderText="First Name" SortExpression="EmployeeFirst" >
                            <ItemStyle Width="133px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="EmployeeLast" HeaderText="Last Name" SortExpression="EmployeeLast" >
                            <ItemStyle Width="208px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" SortExpression="EmployeeType" >
                            <ItemStyle Width="133px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="EmployeeEdit.aspx?EID={0}"
                            Text="Edit" Target="_blank">                            
                            <ItemStyle HorizontalAlign="Center" Width="96px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:HyperLinkField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="#666699" BorderWidth="2px"/>
                        </asp:CommandField>
                    </Columns>
                    <RowStyle CssClass="RowStyle" Height="15px" />
                    <AlternatingRowStyle CssClass="AlternateRowStyle" Height="15px" />
                    <HeaderStyle CssClass="HeaderStyle" />
                </asp:GridView>        
            </div>
        </td>      
    </tr>    
</table>
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:KPFFSchedulingConnectionString %>" 
        SelectCommand="SELECT tblEmployees.EmployeeID, tblEmployees.EmployeeCode, tblEmployees.EmployeeFirst, tblEmployees.EmployeeLast, tblEmployeeTypes.EmployeeType FROM tblEmployees LEFT OUTER JOIN tblEmployeeTypes ON tblEmployees.EmployeeTypeID = dbo.tblEmployeeTypes.ID ORDER BY tblEmployees.EmployeeCode" DeleteCommand="DELETE FROM dbo.tblEngineerGroupMembers WHERE  (EmployeeID = @EmployeeID);  DELETE FROM dbo.tblEmployees WHERE (EmployeeID = @EmployeeID);">
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>

