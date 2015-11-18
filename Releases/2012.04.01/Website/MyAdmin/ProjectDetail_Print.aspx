<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/ProjectDetail_Print.apsx.cs" Inherits="KPFF.PMP.MyAdmin.ProjectDetail_Print" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project Summary</title>
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
        <TABLE BORDER="0" CELLSPACING="0" CELLPADDING="0" width="800">
            <tr>
                <td>
                    <table width="800" border="0" cellpadding="0" cellspacing="0">
                        <tr>
	                        <td width="400"><b>Project Summary</b></td>
	                        <td width="400" align="right" CLASS="detailData"><b><%Response.Write(DateTime.Now.ToShortDateString());%></b></td>
                        </tr>
                        <tr>
	                        <td><img src="images/spacer.gif" height="5" border="0" alt="" /></td>
	                        <td><img src="images/spacer.gif" height="1" border="0" alt="" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="792" border="0" cellpadding="0" cellspacing="0">
                        <tr>
	                        <td CLASS="detailTitle"><b>Project Name:</b></td>
	                        <td CLASS="detailData"><b><asp:Label ID="lblProjectName" runat="server"></asp:Label></b></td>
	                        <td CLASS="detailTitle"><b>Project #:</b></td>
	                        <td CLASS="detailData"><b><asp:Label ID="lblProjectNo" runat="server"></asp:Label></b></td>
                        </tr>
                        <tr>
	                        <td width="120"><img src="images/spacer.gif" width="120" height="5" border="0" alt="" /></td>
	                        <td width="276"><img src="images/spacer.gif" width="276" height="1" border="0" alt="" /></td>
	                        <td width="115"><img src="images/spacer.gif" width="115" height="1" border="0" alt="" /></td>
	                        <td width="281"><img src="images/spacer.gif" width="281" height="1" border="0" alt="" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <igtbl:UltraWebGrid ID="uwgProjects" runat="server" Width="800px" OnSortColumn="uwgProjects_SortColumn">
                        <Bands>
                            <igtbl:UltraGridBand AddButtonCaption="Column0Column1Column2" Key="Column0Column1Column2" AllowAdd="Yes">
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowDeleteDefault="Yes" AllowUpdateDefault="Yes"
                            HeaderClickActionDefault="SortSingle" name="uwgProjects" RowHeightDefault="20px" SelectTypeRowDefault="Single" Version="4.00" AllowAddNewDefault="Yes" StationaryMargins="Header" IndentationTypeDefault="Flat" TableLayout="Fixed" AllowSortingDefault="Yes">
                            <HeaderStyleDefault CssClass="HeaderStyle"></HeaderStyleDefault>
                            <FrameStyle CssClass="FrameStyle" Width="810px"></FrameStyle>
                            <SelectedRowStyleDefault CssClass="SelectedRowStyle"></SelectedRowStyleDefault>
                            <RowAlternateStyleDefault CssClass="AlternateRowStyle"></RowAlternateStyleDefault>
                            <RowStyleDefault CssClass="RowStyle" TextOverflow="Ellipsis"></RowStyleDefault>
                            <ImageUrls CollapseImage="../images/MinusBox.jpg" CurrentEditRowImage="../images/Arrow.jpg"
                                CurrentRowImage="../images/Arrow.jpg" ExpandImage="../images/PlusBox.jpg" />
                            <AddNewBox View="Compact" Location="Top">
                                <Style CssClass="AddNewBoxStyle"></Style>
                            </AddNewBox>
                            <RowSelectorStyleDefault CssClass="RowSelectorStyle">
                            </RowSelectorStyleDefault>
                            <FixedCellStyleDefault CssClass="FixedCellStyle">
                            </FixedCellStyleDefault>
                            <RowExpAreaStyleDefault CssClass="RowExpAreaStyle"></RowExpAreaStyleDefault>
                            <Pager PageSize="25" Alignment="Center">
                                <Style CssClass="PagerStyle"></Style>
                            </Pager>
                            <ActivationObject BorderColor="Red" />
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>                    
				</TD>
			</TR>	      
        </TABLE>     
    </div>
    </form>
</body>
</html>
