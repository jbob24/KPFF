<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="KPFF.PMP.UserControls.ProjectList" %>
<asp:GridView ID="gridHours" runat="server" AutoGenerateColumns="false" CssClass="hoursGrid tablesorter scrollTable" OnPreRender="gridHours_PreRender"
    ShowFooter="true" DataKeyNames="ProjectID"
    EmptyDataText="There are no data records to display." >
    <HeaderStyle CssClass="headerStyle headerFormat" />
    <FooterStyle CssClass="footerStyle" />
    <AlternatingRowStyle CssClass="alternateRow" />
    <Columns>
        <asp:TemplateField HeaderStyle-CssClass="{sorter: false}">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="selectProj" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField FooterStyle-CssClass="footerStyle" DataField="ProjectNo" HeaderText="Job #" />
        <asp:TemplateField>
            <HeaderTemplate>
                <div class="projectHeader">
                    Project
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <a title='<%# Eval("ProjectName") %>' onclick="javascript: window.open('ProjectDetail.aspx?PID=<%# Eval("ProjectID") %>' ,'MyScheduleProjectDetail','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=1000, height=412, top=5, left=5');"
                    href="#">
                    <%# Eval("ProjectName")%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField FooterStyle-CssClass="footerStyle" DataField="LastModifiedDate" DataFormatString="{0:d}" HeaderText="Last Update" Visible="false" />
        <asp:TemplateField>
            <HeaderTemplate>
                <div class="pocHeader">
                    PIC
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("PICCode")%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <div class="pocHeader">
                    PM
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("PM1Code")%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(0)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week1") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week1Footer">
                    <%# week1HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(1)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week2") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week2Footer">
                    <%# week2HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(2)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week3") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week3Footer">
                    <%# week3HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(3)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week4") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week4Footer">
                    <%# week4HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(4)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week5") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week5Footer">
                    <%# week5HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(5)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week6") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week6Footer">
                    <%# week6HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(6)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week7") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week7Footer">
                    <%# week7HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(7)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week8") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week8Footer">
                    <%# week8HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(8)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week9") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week9Footer">
                    <%# week9HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(9)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week10") %>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week10Footer">
                    <%# week10HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(10)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week11")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week11Footer">
                    <%# week11HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(11)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week12")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week12Footer">
                    <%# week12HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(12)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week13")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week13Footer">
                    <%# week13HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(13)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week14")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week14Footer">
                    <%# week14HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(14)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week15")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week15Footer">
                    <%# week15HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(15)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week16")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week16Footer">
                    <%# week16HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(16)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week17")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week17Footer">
                    <%# week17HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(17)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week18")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week18Footer">
                    <%# week18HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(18)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week19")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week19Footer">
                    <%# week19HoursTotal%>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderTemplate>
                <div class="weekHeader">
                    <%# GetWeekHeader(19)%>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Week20")%>
            </ItemTemplate>
            <FooterTemplate>
                <div id="week20Footer">
                    <%# week20HoursTotal %>
                </div>
            </FooterTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
