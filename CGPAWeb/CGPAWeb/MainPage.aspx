<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="CGPAWeb.MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #e0ffff;" >
    <form id="form1" runat="server">
    <%--Heading--%>
    <div style="background-color: purple; color: white; height: 130px;">
        <h1 align="center" style="text-align: center; padding-top: 20px; font-size: 40px">ONLINE RESULT MANAGER</h1>
            <%--<marquee direction="left">Please Bear with us Sir!.This project would be completed soon...</marquee>--%>
    </div>
    <div>
        <%--Information--%>
        <asp:Panel runat="server" ID="ppnel" Style="padding-top: 40px" HorizontalAlign="Center">
            <asp:Label runat="server" Text="Matric No:" Style="padding-left: 4%; font-family: monospace;
                font-size: 25px" />
            <asp:TextBox ID="txtMatric" runat="server" Font-Names="monospace" Font-Size="12" /><br />
            <div style="margin-top: 40px">
                <asp:Label ID="Label1" runat="server" Text="Department:" Style="padding-left: 3%;
                    font-family: monospace; font-size: 20px;" />
                <asp:DropDownList ID="ddlDept" Font-Size="12" runat="server" Width="150" Font-Names="monospace"
                    AppendDataBoundItems="True" DataTextField="DeptName" DataValueField="DeptId" AutoPostBack="True">
                    <asp:ListItem Value="0">--select--</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text="Semester:" Style="padding-left: 3%; font-family: monospace;
                    font-size: 20px" />
                <asp:DropDownList ID="ddlSemester" runat="server" Font-Size="12" Width="150" Font-Names="monospace"
                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSemester_OnSelectedIndexChanged" AutoPostBack="True" />
                <asp:Label ID="Label3" runat="server" Text="Level:" Style="padding-left: 3%; font-family: monospace;
                    font-size: 20px" />
                <asp:DropDownList ID="ddlLevel" runat="server" Width="150" Font-Names="monospace"
                    Font-Size="12" AutoPostBack="True" />
                <asp:Button runat="server" ID="btnLoad" Font-Names="monospace" Text="Load Course"
                    Font-Size="17px" OnClick="btnLoad_Click" />
                <br />
                <br />
                <asp:Label runat="server" ID="lblError" Font-Size="15" ForeColor="red" Style="padding-left: 10%;
                    text-align: center" />
            </div>
        </asp:Panel>
        <br />
        <%--Course GridView--%>
        <asp:GridView ID="courseGridView" runat="server" BackColor="White" BorderColor="#CC9966"
            HorizontalAlign="Center" BorderStyle="None" BorderWidth="1px" CellPadding="4"
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Course Code" SortExpression="CourseId" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Title" SortExpression="CourseTitle" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseTitle" runat="server" Text='<%# Eval("CourseTitle") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Unit" SortExpression="CourseUnit" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseUnit" runat="server" Text='<%# Eval("CourseUnit") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scores">
                    <ItemTemplate>
                        <asp:TextBox ID="txtScore" runat="server" Width="35px" MaxLength="3" />
                        <asp:RangeValidator runat="server" ControlToValidate="txtScore" MinimumValue="0"
                            MaximumValue="100" Type="Integer" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txtGrade" runat="server" Width="35px" ReadOnly="True" Enabled="False" />--%>
                        <asp:Label ID="lblGrade" runat="server" Width="35px" ReadOnly="True" Style="text-align: center"
                            Font-Bold="True" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
        <br />
        <asp:Button runat="server" ID="btnCompute" Text="Compute" Width="150" Font-Names="monospace"
            Font-Size="12" Style="margin-left: 600px" OnClick="btnCompute_OnClick" />
        <br />
        <asp:Panel runat="server" HorizontalAlign="Center">
            <br />
            <asp:Label ID="Label4" runat="server" Text="Total Point :" Font-Size="15" />
            <asp:Label runat="server" ID="lblTotalPoint" Font-Names="monospace" Font-Size="15" />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Total Unit :" Font-Size="15" />
            <asp:Label runat="server" ID="lblTotalUnit" Font-Names="monospace" Font-Size="15" />
            <br />
            <asp:Label runat="server" Text="GPA :" Font-Size="15" />
            <asp:Label runat="server" ID="lblGPA" Font-Size="15" Font-Names="monospace" />
            <br/>
            <asp:Label ID="Label6" runat="server" Text="Cummulative GPA :" Font-Size="15" />
            <asp:Label ID="lblCGPA" runat="server" Font-Size="15" Font-Names="monospace" />
            <br/>
            <asp:Label ID="Label7" runat="server" Text="Remark :" Font-Size="15" />
            <asp:Label ID="lblRemark" runat="server" Font-Size="15" Font-Names="monospace" />
            <br/>
            <%--<asp:Label ID="Label8" runat="server" Text="First Total Point :" Font-Size="15" />
            <asp:Label ID="lblFirstTP" runat="server" Font-Size="15" Font-Names="monospace" />
            <br/>
            <asp:Label ID="Label9" runat="server" Text="First Total Unit :" Font-Size="15" />
            <asp:Label ID="lblSecondTP" runat="server" Font-Size="15" Font-Names="monospace" />--%>
        </asp:Panel>
        
    </div>
    <br />
    <asp:Button runat="server" Style="margin-left: 550px" Width="200" Font-Size="12" Font-Names="monospace" ID="btnSubmit" Text="Save Result" OnClick="btnSubmit_Click" />
    <br/>
    <br/>
    <asp:Label ID="lblSuccess" runat="server" Font-Size="15" ForeColor="Brown" style="padding-left: 550px" />
    <br/>
    <br/>
    </form>
    <%--footer--%>
    <div style="background-color: purple; color: white; height: 30px;">
        <h5 align="center" style="padding-top: 8px">Developed by: IT Student - Software Development Unit. LICT</h5>
    </div>
</body>
</html>
