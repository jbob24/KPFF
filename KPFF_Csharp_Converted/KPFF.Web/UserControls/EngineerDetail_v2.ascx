<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EngineerDetail_v2.ascx.cs"
    ClassName="EngineerDetail" Inherits="KPFF.PMP.UserControls.EngineerDetail_v2" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%--<%@ Register Src="~/UserControls/EditableEngineerProjectList.ascx" TagName="EditableGrid" TagPrefix="eg" %>--%>
<%@ Register Src="~/UserControls/EditableEngineerProjectList_v2.ascx" TagName="EditableGrid" TagPrefix="eg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<li id="engItem" runat="server">
    <div class="engineerData">
        <hr />
        <div class="ProjectsAdd" id="ProjectsAdd" runat="server">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="detailTitle">
                        Search:
                    </td>
                </tr>
                <tr>
                    <td class="detailTitle">
                        <input id="project" type="text" onkeyup="filterList(this.value)" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="detailTitle">
                        <asp:RadioButton ID="rbByName" class="rbByName" runat="server" Text="Order By Name"
                            Checked="true" GroupName="projectsGroup" />
                        <asp:RadioButton ID="rbByNumber" class="rbByNumber" runat="server" Text="Order By Number"
                            GroupName="projectsGroup" />
                    </td>
                </tr>
                <tr>
                    <td class="detailTitle">
                        <div id="projectLists">
                            <div id="projectsByName">
                                <asp:ListBox ID="cboProjectsByName" Rows="10" CssClass="forms projectsByName" runat="server" />
                            </div>
                            <div id="projectsByNumber" style="display: none">
                                <asp:ListBox ID="cboProjectsByNumber" Rows="10" CssClass="forms projectsByNumber"
                                    runat="server" />
                            </div>
                            <div id="projectList" style="display: none">
                                <select id="projectSelect" size="10" onchange="modifySelection();">
                                </select>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="4">
                        <img src="../images/spacer.gif" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <input type="button" value="Close" id="btnCloseAddProject" class="formButton CloseAddProject" />
                        &nbsp;
                        <asp:Button CssClass="formButton" ID="btnAddProject" runat="server" Text="Assign"
                            OnClick="btnAddProject_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="EmployeeReAssign" id="EmployeeReAssign" runat="server">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="formContent">
                        Engineer:
                    </td>
                    <td colspan="3" align="right">
                        <asp:DropDownList ID="cboEmployee" CssClass="forms" runat="server" Width="240">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="8">
                        <img src="../images/spacer.gif" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../images/spacer.gif" />
                    </td>
                    <td colspan="3" align="left">
                        <asp:CheckBox ID="chkAll" runat="server" Text="All Weeks" CssClass="detailTitle"
                            Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="8">
                        <img src="../images/spacer.gif" />
                    </td>
                </tr>
                <tr>
                    <td class="formContent" width="60" align="right">
                        From: &nbsp;
                    </td>
                    <td width="90">
                            <asp:TextBox runat="server" ID="weekFrom" CssClass="weekFrom" Width="75px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="weekFrom"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td class="formContent" width="60" align="right">
                        To: &nbsp;
                    </td>
                    <td width="90">
                            <asp:TextBox runat="server" ID="weekTo" CssClass="weekTo" Width="75px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="weekTo"></ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="8">
                        <img src="../images/spacer.gif" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <input type="button" value="Close" class="formButton CloseReAssignEmployee" id="btnCloseReAssignEmployee" />
                        &nbsp;
                        <asp:Button CssClass="formButton ReAssignButton" ID="btnReAssignEmployee" runat="server"
                            Text="Re-Assign" OnClick="btnReAssignEmployee_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" nowrap="nowrap" colspan="3" valign="middle">
                                <b>
                                    <asp:Label ID="empName" runat="server" /></b>
                            </td>
                            <td align="right">
                                <a href="" id="closeEngImg" runat="server" class="closeEngImg">remove</a>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">
                                <img src="../images/spacer.gif" width="150" height="5" border="0" alt="" />
                            </td>
                            <td width="246">
                                <img src="../images/spacer.gif" width="246" height="1" border="0" alt="" />
                            </td>
                            <td width="150">
                                <img src="../images/spacer.gif" width="150" height="1" border="0" alt="" />
                            </td>
                            <td width="246">
                                <img src="../images/spacer.gif" width="246" height="1" border="0" alt="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="navTable">
                        <tr>
                            <td>
                                <input type="button" class="formButton assignProjectBtn" value="Assign Project" id="btnAssignProject"
                                    runat="server" />   
                            </td>
                            <td>
                                <input type="button" class="formButton reAssignProjectBtn" value="Re-Assign Project"
                                    id="btnReAssignProject" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnUnAssignProjects" runat="server" CssClass="formButton" OnClientClick="return ConfirmUnassign();"
                                    Text="Unassign Projects" OnClick="btnUnAssignProjects_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="errorLbl" runat="server" CssClass="errors"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <eg:EditableGrid ID="hoursGrid" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="5">
                    <img src="../images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnUpdateGrid" runat="server" CssClass="formButtonUpdate" Text="Update"
                        OnClick="btnUpdateGrid_Click" />
                </td>
            </tr>
        </table>
    </div>
</li>
