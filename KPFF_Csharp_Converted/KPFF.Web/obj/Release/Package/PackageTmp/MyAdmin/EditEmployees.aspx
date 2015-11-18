<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/EditEmployees.apsx.cs" Inherits="KPFF.PMP.MyAdmin.EditEmployees" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label></td>                
            </tr>        
            <tr>
                <td align="right"   >
                Employee Name :                    
                </td>
                <td align="left"   >
                    <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                Employee Type :                     
                </td>
                <td align="left"   >
                    <asp:DropDownList ID="cboEmployeeType" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right"  style="height: 26px"  >
                Address :                    
                </td>
                <td align="left"  style="height: 26px"  >
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                City :                    
                </td>
                <td align="left"   >
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" >
                State :                    
                </td>
                <td align="left"   >
                    <asp:DropDownList ID="cboState" runat="server">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                Zip :                    
                </td>
                <td align="left"   >
                    <asp:TextBox ID="txtZip" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td align="right"   >
                Home Phone :                   
                </td>
                <td align="left"   >
                    <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                Cell Phone :                   
                </td>
                <td align="left"   >
                    <asp:TextBox ID="txtCellPhone" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                Comments :                    
                </td>
                <td align="left"   >
                     <asp:TextBox ID="txtComments" runat="server" MaxLength="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right"   >
                Active :                    
                </td>
                <td align="left"   >
                    <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Active </asp:ListItem>
                        <asp:ListItem Value="0">InActive</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
          </table>    
    </div>
    </form>
</body>
</html>
