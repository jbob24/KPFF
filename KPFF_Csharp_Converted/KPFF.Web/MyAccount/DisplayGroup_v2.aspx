<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayGroup_v2.aspx.cs" Inherits="KPFF.PMP.MyAccount.DisplayGroup_v2" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Src="~/UserControls/EngineerDetail.ascx" TagName="EngineerDetail" TagPrefix="ed" %>--%>
<%@ Reference Control="~/UserControls/EngineerDetail_v2.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Detail</title>
    <link rel="stylesheet" type="text/css" href="../wdtStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Style.css" />
    <link rel="stylesheet" type="text/css" href="../HoursBox/HourBox.css" />
    <link rel="Shortcut Icon" href="../images/favicon.ico" />
    <style>
        .DetailsDiv
        {
            position: absolute;
            z-index: 3;
            top: 55px;
            left: 10px;
            padding: 3px;
            width: 792px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
        .ProjectsAdd
        {
            position: absolute;
            z-index: 3;            
            left: 10px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
        .EmployeeReAssign
        {
            position: absolute;
            z-index: 3;            
            left: 137px;
            padding: 10px;
            background-color: #ffffff;
            border-left: 1px #000000 solid;
            border-right: 1px #000000 solid;
            border-top: 1px #000000 solid;
            border-bottom: 1px #000000 solid;
            display: none;
        }
        .closeEngImg
        {
            color: #006;
            font-size: 10px;
            cursor: pointer;
        }
    </style>
    <script src="../_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../_scripts/jquery-ui-1.8.7.custom.min.js" type="text/javascript"></script>

<%--    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libx/jqueryui/1.8.18/jquery-ui.min.js" type="text/javascript"></script>--%>

    <script src="../_scripts/confirm.js" type="text/javascript"></script>
    <script type="text/javascript" src="../HoursBox/jquery.hourbox3.js"></script>
    <script type="text/javascript" src="../_scripts/ListSearch.js"></script>
    <script type="text/javascript" src="../_scripts/EnterToTab.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            SetupHourBox();
            BindInputs();


//            var dataIn = '{' + '"personName":"Joe"}';

//            $.ajax({
//                url: "../Service/EngineerData.svc/HelloWorld",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                data: dataIn,
//                dataType: "json",
//                success: function (data) {
//                    var object = JSON.parse(data.d);
//                    if (object.Error == '') {
//                        alert(object.Response);
//                    }
//                },
//                error: function (error) {
//                    alert("Error: " + error);
//                }
//            });
            

        });

        function SetupHourBox() {
            $('.hoursGrid').editableGrid({
                editable: true,
                sortable: true,
                sortType: ['integer', 'integer', 'link', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox', 'hourbox'],
                curweekstyle: 'currentWeekCell',
                zebraClass: 'alternateRow'
            });

            $('.sortable').sortable({ update: function (event, ui) { SortStop(event, ui); } });
            $('.closeEngImg').click(function () { HideEngineer($(this).attr('EngId')); });


//            $('.sortabledata').live("click", function (event) {
//                alert("sorting by: " + $(this).attr('sort'));
//            });
        }

        function SortStop(event, ui) {
            var items = $('.sortable li');
            var order = "";

            for (var i = 0; i < items.length; i++) {
                order += $(items[i]).attr('EngId') + "|";                
            }

            $('#hdnEngOrder').val(order);
        }

        function HideEngineer(id) {
            $('li[EngId="' + id + '"]').hide();            
            var hdgEngs = $('#hdnRemovedEngs').val();
            $('#hdnRemovedEngs').val(hdgEngs + id + '|');
        }

        function BindInputs() {
            $('.weekCal').attr('readonly', 'readonly');
            $('.weekFrom').attr('readonly', 'readonly');
            $('.weekTo').attr('readonly', 'readonly');

            $('.assignProjectBtn').click(function () {
                var engId = $(this).attr('EngId');
                $('.EmployeeReAssign').slideUp();
                $('.ProjectsAdd[EngId="' + engId + '"]').slideDown();
            });

            $('.CloseAddProject').click(function () {
                $('.ProjectsAdd').slideUp();
            });

            $('.reAssignProjectBtn').click(function () {
                var engId = $(this).attr('EngId');
                $('.ProjectsAdd').slideUp();
                $('.EmployeeReAssign[EngId="' + engId + '"]').slideDown();
            });

            $('.CloseReAssignEmployee').click(function () {
                $('.EmployeeReAssign').slideUp();
            });
        }

        function weekCalChanged(sender, args) {
            $('.weekCalBtn').click();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"
        AsyncPostBackErrorMessage="An error occured processing your request.">
    </asp:ScriptManager>
    <ajaxToolkit:UpdatePanelAnimationExtender ID="ae" runat="server" TargetControlID="UpdatePanel1">
        <Animations>
        <OnUpdating>
            <Sequence>
                <ScriptAction Script="$('#engineersDiv').fadeTo('slow', 0.3);"></ScriptAction>
            </Sequence>
        </OnUpdating>
        <OnUpdated>
            <Sequence>
                <ScriptAction Script="SetupHourBox();"></ScriptAction>
                <ScriptAction Script="BindInputs();"></ScriptAction>
            </Sequence>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
    <div id="engineerpanel">
        <table>
            <tr>
                <td>
                    <table class="navTable">
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnPrevious" CssClass="prevweek" runat="server" Text="<< prev"
                                    OnClick="btnPrevious_Click"></asp:LinkButton>
                            </td>
                            <td>
                                <span class="search">Choose Date:</span>
                            </td>
                            <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="weekCal" CssClass="weekCal" Width="75px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" OnClientDateSelectionChanged="weekCalChanged" TargetControlID="weekCal"></ajaxToolkit:CalendarExtender>
                            <asp:Button style="display: none;" runat="server" Text="Button" ID="weekCalBtn" onclick="weekCalBtn_Click" CssClass="weekCalBtn"></asp:Button>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnThisWeek" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                            <%--<asp:PostBackTrigger ControlID="wdtWeek" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                            </td>
                            <td>
                                <img src="../images/spacer.gif" width="2" border="0" alt="" />
                            </td>
                            <td>
                                <asp:Button ID="btnThisWeek" runat="server" Text="This Week" CssClass="formButton"
                                    OnClick="btnThisWeek_Click" />
                            </td>
                            <td>
                                <img src="../images/spacer.gif" width="2" border="0" alt="" />
                            </td>
                            <td>
                                <asp:LinkButton ID="btnNext" CssClass="nextweek" runat="server" Text="next >>" OnClick="btnNext_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="engineersDiv">
                                <asp:HiddenField ID="hdnEngOrder" runat="server" />
                                <asp:HiddenField ID="hdnRemovedEngs" runat="server" />
                                <ul class="sortable" style="list-style-type: none; margin: 0; padding: 0;">
                                    <asp:PlaceHolder ID="phEngineers" runat="server" EnableViewState="true"></asp:PlaceHolder>
                                </ul>
                                <%--<ed:EngineerDetail ID="engDetail" runat="server" />--%>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnThisWeek" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                            <%--<asp:AsyncPostBackTrigger ControlID="wdtWeek" EventName="ValueChanged" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
