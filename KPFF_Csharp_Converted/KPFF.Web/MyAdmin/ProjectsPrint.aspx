<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/ProjectsPrint.apsx.cs" Inherits="KPFF.PMP.MyAdmin.ProjectsPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>KPFF</title>
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet_Print.css" />
    <link rel="stylesheet" type="text/css" href="../Style_Print.css" />
    <!-- 
    <link rel="stylesheet" type="text/css" href="../style_print.css" />
    <link rel="stylesheet" type="text/css" href="../ExpandedReportStyle.css" />
    -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <TABLE BORDER="0" CELLSPACING="0" CELLPADDING="0" HEIGHT="100%">
	        <TR>
		        <TD WIDTH="985" VALIGN="top" style="height: 530px">
		            <asp:Table ID="tblEngineers" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server"></asp:Table>
	            </TD>
	        </TR>
        </TABLE>     
    </div>
    </form>
</body>
</html>
