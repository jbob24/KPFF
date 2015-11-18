<%@ Page Language="C#" MasterPageFile="~/MyAdmin/_TemplateMaster.master" AutoEventWireup="true"
    CodeBehind="EngineerGroups.aspx.cs" Inherits="KPFF.PMP.MyAdmin.EngineerGroups"
    Title="Untitled Page" %>

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
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../Style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../style_print.css" media="print" />
    <style type="text/css">
        .HighlightNav
        {
            position: absolute;
            top: 47px;
            left: 153px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="cphJavaScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAddGroup').click(function () {
                $('#addGroup').slideDown();
            });

            $('#cancelAdd').click(function () {
                $('#addGroup').slideUp();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <b>>> Engineer Groups</b><img src="images/spacer.gif" width="410" height="1" />
                <input type="button" value="Add Group" class="formButton" id="btnAddGroup" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="addGroup" style="display: none;">
                    <table cellpadding="2" cellspacing="0" border="0" width="360" style="border-left: solid 1px #666699;
                        border-right: solid 1px #666699; border-top: solid 1px #666699; border-bottom: solid 1px #666699">
                        <tr>
                            <td colspan="2" bgcolor="#000066" valign="middle">
                                &nbsp; <font style="color: #ffffff; font-size: 12px;"><b>Add New Group</b></font>
                            </td>
                        </tr>
                        <tr>
                            <td class="formContent">
                                &nbsp; Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="50" CssClass="forms"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formContent">
                                &nbsp; Description:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Width="250px" MaxLength="255" CssClass="forms"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/spacer.gif" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblError" runat="server" CssClass="errors"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <input type="button" value="Cancel" class="formButton" id="cancelAdd" />&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="Add Group" CssClass="formButton" OnClick="btnAdd_Click" /><img
                                    src="images/spacer.gif" width="5" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="2">
                                <img src="images/spacer.gif" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td height="5">
                <img src="images/spacer.gif" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridGroups" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                    RowStyle-CssClass="RowStyle" AlternatingRowStyle-CssClass="AlternateRowStyle"
                    CellPadding="4" DataKeyNames="GroupId" OnRowDataBound="gridGroups_RowDataBound"
                    OnRowDeleting="gridGroups_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="GroupId" Visible="false" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ItemStyle Width="200px" BorderColor="#666699" BorderWidth="2px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                            <ItemStyle Width="300px" BorderColor="#666699" BorderWidth="2px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="GroupId" DataNavigateUrlFormatString="EngineerGroupEdit.aspx?GID={0}"
                            Text="Edit" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="#666699" BorderWidth="2px" />
                        </asp:HyperLinkField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="#666699" BorderWidth="2px" />
                        </asp:CommandField>
                    </Columns>
                    <RowStyle CssClass="RowStyle" Height="15px" />
                    <AlternatingRowStyle CssClass="AlternateRowStyle" Height="15px" />
                    <HeaderStyle CssClass="HeaderStyle" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
