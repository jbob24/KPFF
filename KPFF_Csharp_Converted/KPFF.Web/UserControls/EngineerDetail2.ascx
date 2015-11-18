<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EngineerDetail2.ascx.cs"
    ClassName="EngineerDetail2" Inherits="KPFF.PMP.UserControls.EngineerDetail2" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.1, Version=6.1.20061.28, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Src="~/HoursBox/HourBox.ascx" TagName="HourBox" TagPrefix="hb" %>
<hr />
<div class="ProjectsAdd" id="ProjectsAdd">
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
                <input type="button" value="Close" id="btnCloseAddProject" class="formButton" />
                &nbsp;
                <asp:Button CssClass="formButton" ID="btnAddProject" runat="server" Text="Assign"
                    OnClick="btnAddProject_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="EmployeeReAssign" id="EmployeeReAssign">
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
                <igsch:WebDateChooser ID="wdtFrom" runat="server" Width="90px" AllowNull="False">
                    <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="../images/igsch_right_arrow.gif"
                        PrevMonthImageUrl="../images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
                        ShowYearDropDown="False" CellPadding="5">
                        <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" BackColor="White" BorderColor="#7F9DB9" BorderStyle="Solid"
                            Font-Names="Tahoma,Verdana" Font-Size="8pt" Height="150px">
                        </CalendarStyle>
                        <TodayDayStyle BackColor="#FBE694" />
                        <SelectedDayStyle BackColor="Transparent" ForeColor="Black" BorderColor="#BB5503"
                            BorderStyle="Solid" BorderWidth="2px" />
                        <OtherMonthDayStyle ForeColor="#ACA899" />
                        <DayHeaderStyle>
                            <BorderDetails ColorBottom="172, 168, 153" StyleBottom="Solid" WidthBottom="1px" />
                        </DayHeaderStyle>
                        <TitleStyle BackColor="#9EBEF5"></TitleStyle>
                    </CalendarLayout>
                    <EditStyle Font-Names="Tahoma" Font-Size="8pt">
                    </EditStyle>
                </igsch:WebDateChooser>
            </td>
            <td class="formContent" width="60" align="right">
                To: &nbsp;
            </td>
            <td width="90">
                <igsch:WebDateChooser ID="wdtTo" runat="server" Width="90px" AllowNull="False">
                    <CalendarLayout DayNameFormat="FirstLetter" NextMonthImageUrl="../images/igsch_right_arrow.gif"
                        PrevMonthImageUrl="../images/igsch_left_arrow.gif" ShowFooter="False" ShowMonthDropDown="False"
                        ShowYearDropDown="False" CellPadding="5">
                        <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" BackColor="White" BorderColor="#7F9DB9" BorderStyle="Solid"
                            Font-Names="Tahoma,Verdana" Font-Size="8pt" Height="150px">
                        </CalendarStyle>
                        <TodayDayStyle BackColor="#FBE694" />
                        <SelectedDayStyle BackColor="Transparent" ForeColor="Black" BorderColor="#BB5503"
                            BorderStyle="Solid" BorderWidth="2px" />
                        <OtherMonthDayStyle ForeColor="#ACA899" />
                        <DayHeaderStyle>
                            <BorderDetails ColorBottom="172, 168, 153" StyleBottom="Solid" WidthBottom="1px" />
                        </DayHeaderStyle>
                        <TitleStyle BackColor="#9EBEF5"></TitleStyle>
                    </CalendarLayout>
                    <EditStyle Font-Names="Tahoma" Font-Size="8pt">
                    </EditStyle>
                </igsch:WebDateChooser>
            </td>
        </tr>
        <tr>
            <td colspan="4" height="8">
                <img src="../images/spacer.gif" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <input type="button" value="Close" class="formButton" id="btnCloseReAssignEmployee" />
                &nbsp;
                <asp:Button CssClass="formButton" ID="btnReAssignEmployee" runat="server" Text="Re-Assign"
                    OnClick="btnReAssignEmployee_Click" />
            </td>
        </tr>
    </table>
</div>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="810" valign="top">
            <table width="792" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" nowrap="nowrap">
                        <b>
                            <asp:Label ID="empName" runat="server" /></b>
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
                        <input type="button" class="formButton" value="Assign Project" id="btnAssignProject" />
                    </td>
                    <td>
                        <input type="button" class="formButton" value="Re-Assign Project" id="btnReAssignProject" />
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
            <%--            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" class="hoursGrid" border="1" >
                            <thead>
                            <tr class="headerStyle headerFormat">
                                <th align="center" width="23">
                                </th>
                                <th align="center" width="39">
                                    Job #
                                </th>
                                <th align="center" width="231">
                                    Project
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(0)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(1)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(2)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(3)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(4)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(5)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(6)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(7)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(8)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(9)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(10)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(11)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(12)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(13)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(14)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(15)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(16)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(17)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(18)%>
                </div>
                                </th>
                                <th align="center" class="weekHeader" width="34">
                <div class="weekHeader">
                    <%= GetWeekHeader(19)%>
                </div>
                                </th>
                            </tr>
                            </thead>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="engineerList" style="overflow: auto; height: 200px;">
                            <asp:GridView ID="dgEngineers" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                AlternatingRowStyle-CssClass="alternateRow" CellSpacing="0" 
                                OnRowDataBound="dgEngineers_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="21px" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="selectProj" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectNo">
                                        <ItemStyle Width="37px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a title='<%# Eval("ProjectName") %>' onclick="javascript: window.open('ProjectDetail.aspx?PID=<%# Eval("ProjectID") %>' ,'MyScheduleProjectDetail','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=1000, height=412, top=5, left=5');"
                                                href="#">
                                                <%# Eval("ProjectName") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="229px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week1Hours" text='<%# Eval("Week1") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week2Hours" text='<%# Eval("Week2") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week3Hours" text='<%# Eval("Week3") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week4Hours" text='<%# Eval("Week4") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week5Hours" text='<%# Eval("Week5") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week6Hours" text='<%# Eval("Week6") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week7Hours" text='<%# Eval("Week7") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week8Hours" text='<%# Eval("Week8") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week9Hours" text='<%# Eval("Week9") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week10Hours" text='<%# Eval("Week10") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week11Hours" text='<%# Eval("Week11") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week12Hours" text='<%# Eval("Week12") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week13Hours" text='<%# Eval("Week13") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week14Hours" text='<%# Eval("Week14") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week15Hours" text='<%# Eval("Week15") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week16Hours" text='<%# Eval("Week16") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week17Hours" text='<%# Eval("Week17") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week18Hours" text='<%# Eval("Week18") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week19Hours" text='<%# Eval("Week19") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="hours">
                                        <ItemTemplate>
                                            <hb:hourbox id="week20Hours" text='<%# Eval("Week20") %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="32px" />
                                    </asp:TemplateField>




                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>




                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="1" >
                            <tr class="footerStyle">
                                <td colspan="3" width="293">
                                    <img src="../images/GridLegend.jpg">
                                </td>
                                <td align="center" width="34">
                                   <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                                <td align="center" width="34">
                                    <%# week1HoursTotal %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>




            </table>
            <%--                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdateGrid" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>--%>
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
