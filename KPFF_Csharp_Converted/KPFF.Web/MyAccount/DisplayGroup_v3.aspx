<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayGroup_v3.aspx.cs"
    Inherits="KPFF.PMP.MyAccount.DisplayGroup_v3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Detail</title>

    <link rel="stylesheet" type="text/css" href="../Themes/jquery-ui-1.8.21.custom/css/custom-theme/jquery-ui-1.8.21.custom.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <asp:HiddenField ID="hdnEngineerList" runat="server" Value="163|114|31|" />
        <table class="navTable">
            <tr>
                <td>
                    <a href="#" id="linkPrevious"><< prev</a>
                </td>
                <td>
                    <span class="search">Choose Date:</span>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="weekCal" CssClass="weekCal" ReadOnly="true" Width="75px"></asp:TextBox>
                </td>
                <td>
                    <a href="#" id="linkCurrentWeek">This Week</a>
                </td>
                <td>
                    <a href="#" id="linkNextWeek">next >></a>
                </td>
            </tr>
        </table>
        <div id="engineers"></div>
    </div>
    </form>
</body>
</html>


<script src="../_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../_scripts/jquery-ui-1.8.7.custom.min.js" type="text/javascript"></script>
<script src="../_scripts/jquery.engineerdetails.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#main').engineerdetails();
    });
</script>
