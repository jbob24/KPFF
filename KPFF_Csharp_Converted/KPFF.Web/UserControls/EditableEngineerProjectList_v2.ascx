<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditableEngineerProjectList_v2.ascx.cs"
    Inherits="KPFF.PMP.UserControls.EditableEngineerProjectList_v2" %>
<%@ Register Src="~/HoursBox/HourBox.ascx" TagName="HourBox" TagPrefix="hb" %>
<table class="hoursGrid" empId="<%= this.EmpId %>" cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <table class="editableGridHeader" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr>
                        <th class="headerStyle headerFormat" width="19">
                        </th>
                        <th class="headerStyle headerFormat projectHeader sortabledata" sort="ProjectNo" width="56" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientName')">--%>Job
                            #<%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat projectHeader sortabledata" sort="ProjectName" width="231" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientName')">--%>Project<%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week1" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%>
                            <%=GetWeekHeader(0) %>
                            <%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week2" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(1)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week3" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(2)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week4" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(3)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week5" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(4)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week6" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(5)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week7" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(6)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week8" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(7)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week9" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(8)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week10" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(9)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week11" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(10)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week12" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(11)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week13" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(12)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week14" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(13)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week15" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(14)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week16" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(15)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week17" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(16)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week18" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(17)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week19" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(18)%><%--</a>--%>
                        </th>
                        <th class="headerStyle headerFormat weekHeader sortabledata" sort="Week20" width="31" align="center">
                            <%--<a href="javascript:__doPostBack('ctl00$cphMainContent$dgClients','Sort$ClientType')">--%><%= GetWeekHeader(19)%><%--</a>--%>
                        </th>
                    </tr>
                </thead>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div class="GroupEngList" style="overflow: auto; border-bottom: solid 1px #666699;
                border-left: solid 1px #666699; border-right: solid 1px #666699;">
                <asp:GridView ID="gridHours" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                    ShowFooter="false" CssClass="tablesorter scrollTable editableGridBody" OnPreRender="gridHours_PreRender"
                    DataKeyNames="ID, ProjectID" EmptyDataText="There are no data records to display.">
                    <HeaderStyle CssClass="headerStyle headerFormat" />
                    <FooterStyle CssClass="footerStyle" />
                    <AlternatingRowStyle CssClass="alternateRow" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="{sorter: false}">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="selectProj" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField FooterStyle-CssClass="footerStyle" DataField="ProjectNo" ItemStyle-Width="60px"
                            HeaderText="Job #" HeaderStyle-CssClass="{sorter: false}" />
                        <asp:TemplateField>
                            <ItemStyle Width="250px" />
                            <ItemTemplate>
                                <a title='<%# Eval("ProjectName") %>' onclick="javascript: window.open('ProjectDetail.aspx?PID=<%# Eval("ProjectID") %>' ,'MyScheduleProjectDetail','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=1000, height=412, top=5, left=5');"
                                    href="#">
                                    <%# Eval("ProjectName") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week1Hours" Text='<%# Eval("Week1") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week2Hours" Text='<%# Eval("Week2") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week3Hours" Text='<%# Eval("Week3") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week4Hours" Text='<%# Eval("Week4") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week5Hours" Text='<%# Eval("Week5") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week6Hours" Text='<%# Eval("Week6") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week7Hours" Text='<%# Eval("Week7") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week8Hours" Text='<%# Eval("Week8") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week9Hours" Text='<%# Eval("Week9") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week10Hours" Text='<%# Eval("Week10") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week11Hours" Text='<%# Eval("Week11") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week12Hours" Text='<%# Eval("Week12") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week13Hours" Text='<%# Eval("Week13") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week14Hours" Text='<%# Eval("Week14") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week15Hours" Text='<%# Eval("Week15") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week16Hours" Text='<%# Eval("Week16") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week17Hours" Text='<%# Eval("Week17") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week18Hours" Text='<%# Eval("Week18") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week19Hours" Text='<%# Eval("Week19") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hours">
                            <ItemStyle Width="34px" />
                            <ItemTemplate>
                                <hb:HourBox ID="week20Hours" Text='<%# Eval("Week20") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <table class="hoursGridFooter" cellpadding="0" cellspacing="0" border="0">
                <tfoot>
                    <tr class="footerStyle">
                        <td width="17">
                            &nbsp;
                        </td>
                        <td width="54" class="footerStyle">
                            &nbsp;
                        </td>
                        <td width="229">
                            <img src="../images/GridLegend.jpg">
                        </td>
                        <td width="29">
                            <div id="week1Footer">
                                <%= week1HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week2Footer">
                                <%= week2HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week3Footer">
                                <%= week3HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week4Footer">
                                <%= week4HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week5Footer">
                                <%= week5HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week6Footer">
                                <%= week6HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week7Footer">
                                <%= week7HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week8Footer">
                                <%= week8HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week9Footer">
                                <%= week9HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week1<%= week1HoursTotal%>Footer">
                                <%= week10HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week11Footer">
                                <%= week11HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week12Footer">
                                <%= week12HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week13Footer">
                                <%= week13HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week14Footer">
                                <%= week14HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week15Footer">
                                <%= week15HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week16ooter">
                                <%= week16HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week17Footer">
                                <%= week17HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week18Footer">
                                <%= week18HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week19Footer">
                                <%= week19HoursTotal%>
                            </div>
                        </td>
                        <td width="29">
                            <div id="week20Footer">
                                <%= week20HoursTotal%>
                            </div>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </td>
    </tr>
</table>
