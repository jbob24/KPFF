<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="KPFF.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login Form</title>
    <link href="Styles/style.css" rel="stylesheet" media="all" />
    <link rel="Shortcut Icon" href="Images/favicon.ico" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/chrome-frame/1/CFInstall.min.js"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style>
        /* 
    CSS rules to use for styling the overlay:
      .chromeFrameOverlayContent
      .chromeFrameOverlayContent iframe
      .chromeFrameOverlayCloseBar
      .chromeFrameOverlayUnderlay
   */
    </style>
    <%--  <script>
      // You may want to place these lines inside an onload handler
      CFInstall.check({
          mode: "inline",
          node: "#chromeNode" 
      });

  </script>--%>
    <script>
        // The conditional ensures that this code will only execute in IE,
        // Therefore we can use the IE-specific attachEvent without worry
        window.attachEvent("onload", function () {
            CFInstall.check({
                preventPrompt: true,
                onmissing: IEChromeFrameMissing
            });
        });
    </script>
    <script type="text/javascript">
        function IEChromeFrameMissing() {
            $("#chromeNode").show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" width="541" style="border-left: 1px #000000 solid;
            border-right: 1px #000000 solid; border-top: 1px #000000 solid; border-bottom: 1px #000000 solid;">
            <tr>
                <td colspan="2" width="541">
                    <img src="Images/header.jpg" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    Login
                </td>
            </tr>
            <tr>
                <td colspan="2" height="10">
                    <img src="Images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td width="200" align="right">
                    Username:
                </td>
                <td width="341" align="left">
                    &nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="forms"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Password:
                </td>
                <td align="left">
                    &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="forms" Width="200px"
                        TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:CheckBox ID="cbRememberMe" Text="Remember Me" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" height="10">
                    <asp:Label ID="lblError" runat="server" CssClass="errors"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="formButton" Text="Login" />
                </td>
            </tr>
            <tr>
                <td colspan="2" height="10">
                    <img src="Images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="chromeNode" style="display: none; font-size: small">
                        <table width="100%">
                            <tr>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a id="chromeFrameLink" href="http://www.google.com/chromeframe" target="_blank">Click
                                        Here</a> to learn how to improve<br />
                                    the performance of this application in Internet Explorer.
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
