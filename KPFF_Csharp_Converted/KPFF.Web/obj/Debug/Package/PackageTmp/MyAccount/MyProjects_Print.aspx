<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/MyAccount/MyProjects_Print.aspx.cs" Inherits="KPFF.PMP.MyAccount.MyProjects_Print" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>My Projects</title>
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
        <table cellpadding="0" cellspacing="0" border="0" width="980">  
	            <tr>
                    <td width="490" align="left"><b>My Projects - <%Response.Write(Session["UserName"]); %></b></td>
                    <td CLASS="detailTitle" width="490" align="right"><b><%Response.Write(DateTime.Now.ToShortDateString()); %></b></td>
                </tr>
                <tr>
                    <td colspan="2" height="20"><img src="images/spacer.gif" height="5" border="0" alt="" /></td>  
               </tr>               
                <tr>
                    <td valign="top" colspan="2">
                      <igtbl:UltraWebGrid ID="uwgProjects" runat="server" DataKeyField="ID" DataMember="Projects" OnInitializeLayout="uwgProjects_InitializeLayout" OnPageIndexChanged="uwgProjects_PageIndexChanged"> 
                            <Bands>
                                <igtbl:UltraGridBand AddButtonCaption="Column0Column1Column2" Key="Column0Column1Column2" AllowAdd="Yes">
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free"
                                HeaderClickActionDefault="SortMulti" name="uwgProjects" RowHeightDefault="20px" SelectTypeRowDefault="Extended" Version="4.00" StationaryMargins="Header" IndentationTypeDefault="Flat" TableLayout="Fixed" AllowSortingDefault="Yes">
                                <HeaderStyleDefault CssClass="HeaderStyle"></HeaderStyleDefault>
                                <FrameStyle CssClass="FrameStyle" Width="980px"></FrameStyle>
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
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
