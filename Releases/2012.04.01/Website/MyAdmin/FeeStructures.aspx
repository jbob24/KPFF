<%@ Page Language="C#" MasterPageFile="~/MyAdmin/_TemplateMaster.master" AutoEventWireup="true" CodeBehind="~/MyAdmin/FeeStructures.apsx.cs" Inherits="KPFF.PMP.MyAdmin.FeeStructures" title="Untitled Page" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" Runat="Server">
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
    <link rel="stylesheet" type="text/css" href="../UWGStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <style type="text/css">
        .HighlightNav { position: absolute; top: 47px; left: 153px; z-index: 1; }        
    </style>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <b>>> Fee Structures</b><img src="images/spacer.gif" height="1" width="43" /><asp:Button CssClass="formButtonUpdate" ID="btnUpdate" runat="server" Text="Update Grid" />
            </td>
        </tr>
        <tr>
            <td height="5"><img src="images/spacer.gif" /></td>
        </tr>
        <tr>
            <td>
                <igtbl:UltraWebGrid ID="uwgFeeStructure" runat="server" DataKeyField="ID" DataMember="FeeStructureType" OnDeleteRowBatch="uwgPhases_DeleteRowBatch" OnInitializeLayout="uwgPhases_InitializeLayout" OnUpdateGrid="uwgPhases_UpdateGrid">
                    <Bands>
                        <igtbl:UltraGridBand AddButtonCaption="Column0Column1Column2" Key="Column0Column1Column2">
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowDeleteDefault="Yes"
                        AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                        Name="uwgPhases" RowHeightDefault="15px" SelectTypeRowDefault="Extended"
                        Version="4.00">
                        <AddNewRowDefault View="Top" Visible="Yes">
                        </AddNewRowDefault>
                        <HeaderStyleDefault CssClass="HeaderStyle"></HeaderStyleDefault>
                        <RowStyleDefault CssClass="RowStyle"></RowStyleDefault>
                        <SelectedRowStyleDefault CssClass="SelectedRowStyle"></SelectedRowStyleDefault>
	                    <RowAlternateStyleDefault CssClass="AlternateRowStyle"></RowAlternateStyleDefault>
                        <RowSelectorStyleDefault CssClass="RowSelectorStyle">
                        </RowSelectorStyleDefault>
                        <FixedCellStyleDefault CssClass="FixedCellStyle">
                        </FixedCellStyleDefault>
                    </DisplayLayout>
                </igtbl:UltraWebGrid>            
            </td>
        </tr>
    </table>
</asp:Content>

