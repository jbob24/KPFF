<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditableEngineerList.ascx.cs"
    Inherits="KPFF.PMP.UserControls.EditableEngineerList" %>
<%@ Register Src="~/HoursBox/HourBox.ascx" TagName="HourBox" TagPrefix="hb" %>
<asp:GridView ID="gridHours" runat="server" AutoGenerateColumns="false" CssClass="hoursGrid tablesorter scrollTable"
    ShowFooter="true" OnPreRender="gridHours_PreRender" DataKeyNames="EmployeeID"
    EmptyDataText="There are no data records to display.">
    <HeaderStyle CssClass="headerStyle headerFormat" />
    <FooterStyle CssClass="footerStyle" />
    <AlternatingRowStyle CssClass="alternateRow" />
    <Columns>
        <asp:TemplateField HeaderStyle-CssClass="{sorter: false}">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="selectEmp" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <div class="projectHeader">
                    Employee
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <a title='<%# Eval("EmployeeName") %>' onclick="javascript: window.open('EmployeeDetail.aspx?EID=<%# Eval("EmployeeID") %>' ,'MyScheduleProjectDetail','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=1000, height=412, top=5, left=5');"
                    href="#">
                    <%# Eval("EmployeeName")%></a>
            </ItemTemplate>
            <FooterTemplate>
                <img src="../images/GridLegend.jpg" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:BoundField FooterStyle-CssClass="footerStyle" DataField="Role" HeaderText="Role" />
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(0)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week1Hours" Text='<%# Eval("Week1") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week1Footer">
                    <%# week1HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(1)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week2Hours" Text='<%# Eval("Week2") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week2Footer">
                    <%# week2HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(2)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week3Hours" Text='<%# Eval("Week3") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week3Footer">
                    <%# week3HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(3)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week4Hours" Text='<%# Eval("Week4") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week4Footer">
                    <%# week4HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(4)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week5Hours" Text='<%# Eval("Week5") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week5Footer">
                    <%# week5HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(5)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week6Hours" Text='<%# Eval("Week6") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week6Footer">
                    <%# week6HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(6)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week7Hours" Text='<%# Eval("Week7") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week7Footer">
                    <%# week7HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(7)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week8Hours" Text='<%# Eval("Week8") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week8Footer">
                    <%# week8HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(8)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week9Hours" Text='<%# Eval("Week9") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week9Footer">
                    <%# week9HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(9)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week10Hours" Text='<%# Eval("Week10") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week10Footer">
                    <%# week10HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(10)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week11Hours" Text='<%# Eval("Week11") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week11Footer">
                    <%# week11HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(11)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week12Hours" Text='<%# Eval("Week12") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week12Footer">
                    <%# week12HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(12)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week13Hours" Text='<%# Eval("Week13") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week13Footer">
                    <%# week13HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(13)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week14Hours" Text='<%# Eval("Week14") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week14Footer">
                    <%# week14HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(14)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week15Hours" Text='<%# Eval("Week15") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week15Footer">
                    <%# week15HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>


        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(15)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week16Hours" Text='<%# Eval("Week16") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week16Footer">
                    <%# week16HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(16)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week17Hours" Text='<%# Eval("Week17") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week17Footer">
                    <%# week17HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(17)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week18Hours" Text='<%# Eval("Week18") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week18Footer">
                    <%# week18HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(18)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week19Hours" Text='<%# Eval("Week19") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week19Footer">
                    <%# week19HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField ItemStyle-CssClass="hours">
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(19)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <hb:HourBox ID="week20Hours" Text='<%# Eval("Week20") %>' runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <div id="week20Footer">
                    <%# week20HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>


    </Columns>
</asp:GridView>
