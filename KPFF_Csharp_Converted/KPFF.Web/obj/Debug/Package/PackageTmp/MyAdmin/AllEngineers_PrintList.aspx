<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/AllEngineers_PrintList.apsx.cs" Inherits="KPFF.PMP.MyAdmin.AllEngineers_PrintList" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>All Engineers</title>
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <script language="javascript" type="text/javascript">
           function onLoad()
           {
                window.print();
                window.close();
                return true;
           }
    </script>
</head>
<body onload="onLoad()">
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="850"> 
        <tr>
            <td width="425" align="left"><b>All Engineers</b></td>
            <td CLASS="detailTitle" width="425" align="right"><b><%Response.Write(DateTime.Now.ToShortDateString());%></b></td>
        </tr>
        <tr>
            <td colspan="2" height="20"><img src="images/spacer.gif" height="5" border="0" alt="" /></td>  
       </tr>                  
        <tr>
            <td valign="top" colspan="2">
                <asp:Table ID="tblEngineers" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"></asp:Table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
