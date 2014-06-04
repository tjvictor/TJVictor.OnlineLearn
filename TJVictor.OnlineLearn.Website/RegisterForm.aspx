<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="RegisterForm.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.RegisterForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 900px; margin-top: 20px;">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <h3>
                        用户注册</h3>
                        <div>
                        <asp:Label ID="ErrorTxt" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            登录ID(<span style="color:Red;">必填</span>):</label>
                        <telerik:RadTextBox ID="LoginNameTxt" runat="server" Width="200px">
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            登录密码(<span style="color:Red;">必填</span>):</label>
                        <telerik:RadTextBox ID="PwdTxt" runat="server" Width="200px" TextMode="Password">
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            确认密码(<span style="color:Red;">必填</span>):</label>
                        <telerik:RadTextBox ID="ConPwdTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            真实姓名(<span style="color:Red;">必填</span>):</label>
                        <telerik:RadTextBox ID="UserName" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            注册邮箱(<span style="color:Red;">必填</span>):</label>
                        <telerik:RadTextBox ID="EmailTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            手机号(可选):</label>
                        <telerik:RadTextBox ID="MobileNo" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            QQ(可选):</label>
                        <telerik:RadTextBox ID="QQTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            微信(可选):</label>
                        <telerik:RadTextBox ID="WeixinTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            Skype(可选):</label>
                        <telerik:RadTextBox ID="SkypeTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 150px; display: inline-block;">
                            FaceTime(可选):</label>
                        <telerik:RadTextBox ID="FaceTimeTxt" runat="server" Width="200px" >
                        </telerik:RadTextBox>
                    </div>
                    <div>
                        <label style="width: 100%; display: inline-block;">
                            点击“注册”按钮表示<span style="color: Red;">已阅读并同意</span>右边的服务使用协议</label>
                    </div>
                    <div>
                        <telerik:RadButton ID="RegisterBtn" runat="server" Text="注册" Width="360px" OnClick="RegisterBtn_Click">
                        </telerik:RadButton>
                    </div>
                </td>
                <td style="width: 50%; height: 100%; vertical-align: top;">
                    <telerik:RadTextBox ID="policyTxt" runat="server" TextMode="MultiLine" Width="100%"
                        Wrap="true" Height="500px" Text="">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
