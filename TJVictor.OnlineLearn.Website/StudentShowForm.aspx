<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StudentShowForm.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.StudentShowForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 900px; margin-top: 20px;">
        <telerik:RadGrid ID="gridView" runat="server" Width="100%" AllowPaging="true" OnNeedDataSource="RadGrid_NeedDataSource"
            PageSize="15" AutoGenerateColumns="false" AllowFilteringByColumn="false" EnableViewState="true"
            ShowHeader="false" GroupingEnabled="false">
            <MasterTableView>
                <Columns>
                    <telerik:GridTemplateColumn ItemStyle-Width="120px" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <ul style="list-style-image: none; list-style-type: none; margin: 0px; padding: 10px">
                                <li>
                                    <telerik:RadBinaryImage ID="AvatarImg" runat="server" Width="100px" Height="100px"
                                        AutoAdjustImageControlSize="false" DataValue='<%#Eval("U_Avatar") %>' /></li>
                                <li>
                                    <%# Eval("U_ID") %></li>
                                <li>
                                    <%# Eval("UserName") %></li>
                            </ul>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        发表于
                                        <%# Eval("CreateTime")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>对
                                                <%# Eval("T_Name")%>
                                                教师的评价:</legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <ul style="list-style-image: none; list-style-type: none; margin: 0px; padding: 10px">
                                                                <li>
                                                                    <label>
                                                                        激情:</label>
                                                                    <telerik:RadRating ID="jqRate" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("T1")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        态度:</label>
                                                                    <telerik:RadRating ID="tdRate" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("T2")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        幽默:</label>
                                                                    <telerik:RadRating ID="ymRate" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("T3")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        准时:</label>
                                                                    <telerik:RadRating ID="zsRate" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("T4")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        其它:</label>
                                                                    <telerik:RadRating ID="qtRate" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("T5")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <%# Eval("T_Des") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>对
                                                <%# Eval("C_Name")%>
                                                课程的评价:</legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <ul style="list-style-image: none; list-style-type: none; margin: 0px; padding: 10px">
                                                                <li>
                                                                    <label>
                                                                        激情:</label>
                                                                    <telerik:RadRating ID="RadRating1" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("C1")) %>'
                                                                        ReadOnly="true" Precision="Item" >
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        态度:</label>
                                                                    <telerik:RadRating ID="RadRating2" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("C2")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        幽默:</label>
                                                                    <telerik:RadRating ID="RadRating3" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("C3")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        准时:</label>
                                                                    <telerik:RadRating ID="RadRating4" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("C4")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        其它:</label>
                                                                    <telerik:RadRating ID="RadRating5" runat="server" AutoPostBack="false" Value='<%# Convert.ToInt16(Eval("C5")) %>'
                                                                        ReadOnly="true" Precision="Item">
                                                                    </telerik:RadRating>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <%# Eval("C_Des") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <div>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="Des">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
