<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAdmin/EmployeeDetail_Print.apsx.cs" Inherits="KPFF.PMP.MyAdmin.EmployeeDetail_Print" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Detail</title>
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
	                        <td width="400"><b>Employee Summary</b></td>
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
					        <td CLASS="detailData" colspan="4"><b><asp:Label ID="lblName" runat="server"></asp:Label></b></td>
				        </tr>				        
				        <tr>
					        <td width="150"><img src="images/spacer.gif" width="150" height="5" border="0" alt="" /></td>
					        <td width="246"><img src="images/spacer.gif" width="246" height="1" border="0" alt="" /></td>
					        <td width="150"><img src="images/spacer.gif" width="150" height="1" border="0" alt="" /></td>
					        <td width="246"><img src="images/spacer.gif" width="246" height="1" border="0" alt="" /></td>
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
                            HeaderClickActionDefault="SortMulti" name="uwgProjects" RowHeightDefault="20px" SelectTypeRowDefault="Extended" Version="4.00" AllowAddNewDefault="Yes" StationaryMargins="HeaderAndFooter" IndentationTypeDefault="Flat" TableLayout="Fixed">
                            <HeaderStyleDefault CssClass="HeaderStyle"></HeaderStyleDefault>
                            <FrameStyle CssClass="FrameStyle" Width="785px"></FrameStyle>
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
                            <ActivationObject BorderColor="red" />
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </td>
            </tr>	
        </TABLE>    
    </div>
    </form>
</body>
</html>
