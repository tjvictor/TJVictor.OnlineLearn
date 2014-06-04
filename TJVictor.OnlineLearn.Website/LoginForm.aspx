<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="LoginForm.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.LoginForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 900px; margin-top: 20px;">
        <div style="margin: 50px;">
            <h3>
                用户登录</h3>
            <div>
                <label style="width: 100px; display: inline-block;">
                    登录ID:</label>
                <telerik:RadTextBox ID="LoginIDTxt" runat="server" Width="200px">
                </telerik:RadTextBox>
            </div>
            <div>
                <label style="width: 100px; display: inline-block;">
                    登录密码:</label>
                <telerik:RadTextBox ID="PwdTxt" runat="server" TextMode="Password" Width="200px">
                </telerik:RadTextBox>
            </div>
            <div style="margin: 10px 0px">
            <asp:Label ID="ErrorTxt" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div style="margin: 20px 0px">
                <telerik:RadButton ID="LoginBtn" runat="server" Text="登录" Width="305px" 
                    BackColor="Blue" onclick="LoginBtn_Click">
                </telerik:RadButton>
                <span style="margin:0px 20px;">
                <telerik:RadButton ID="ForgetPwdBtn" runat="server" Text="忘记密码?" 
                    ButtonType="LinkButton" Width="100px"  Skin=""></telerik:RadButton>
                </span>
                <span style="margin:0px 20px;">
                <telerik:RadButton ID="RadButton1" runat="server" Text="还没有帐号？马上注册!" 
                    ButtonType="LinkButton" Width="150px" Skin="" PostBackUrl="RegisterForm.aspx" ></telerik:RadButton>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
