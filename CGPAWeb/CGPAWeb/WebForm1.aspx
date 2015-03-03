<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CGPAWeb.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                <%--<asp:BoundField DataField="CourseId" HeaderText="Course Code" SortExpression="CourseId" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" SortExpression="CourseTitle" HeaderStyle-Width="150px" />
                    <asp:BoundField DataField="CourseUnit" HeaderText="Course Unit" SortExpression="CourseUnit" HeaderStyle-Width="100px" />--%>
                <asp:TemplateField HeaderText="Scores">
                    <ItemTemplate>
                        <asp:TextBox ID="txtScore" runat="server" Width="35px" MaxLength="3" TabIndex="1" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGrade" runat="server" Width="35px" ReadOnly="True" />
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
        <br/><asp:Button runat="server" ID="btnClick" Text="Click" OnClick="btnClick_OnClick"/>
        <asp:Label runat="server" ID="lblText" />
        <br/>Total Unit : <asp:TextBox runat="server" ID="txtTotalUnit"/>
        <br/>Total Point<asp:TextBox runat="server" ID="txtTotalPoint"/>
        <br/>GPA :<asp:Label runat="server" ID="lblGPA"/>
        
        

        <%--<asp:Panel ID="panelCourseContent" runat="server">
            <table width="75%" cellpadding="2" cellspacing="2" border="0" bgcolor="#EAEAEA">
                <tr align="left" style="background-color: #004080; color: White;">
                    <th style="width: 100px">
                        Course Code
                    </th>
                    <th style="width: 250px">
                        Course Title
                    </th>
                    <th>
                        CourseUnit
                    </th>
                    <th>
                        Scores
                    </th>
                    <th>
                        Grade
                    </th>
                </tr>
                <tr id="rowData" align="left" style="background-color: #e0ffff; color: black;">
                    <td style="width: 100px">
                        <asp:Label runat="server" ID="lblCode" Text='<%# Eval("CourseId") %>' />
                    </td>
                    <td style="width: 250px">
                        <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("CourseTitle") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblUnit" Text='<%# Eval("CourseUnit") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtScore" runat="server" Width="50%" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtGrade" runat="server" Width="50%" ReadOnly="True" />
                    </td>
                </tr>
            </table>
        </asp:Panel>--%>
    </div>
    </form>
</body>
</html>
