<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleUpdateForm.aspx.cs"
    Inherits="TJVictor.OnlineLearn.Website.ScheduleUpdateForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .OpButton
        {
            margin-right: 50px;
        }
    </style>
    <telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                GetRadWindow().close();
            }

            function ShowCancelConfrimButton() {
                var tr = document.getElementById("hiddenTR");
                if (tr)
                    tr.style.visibility = "visible";
            }

            function CanceledSchedule() {
                alert('退订成功!');
                Close();
            }

            function FormAutoSize() {
                
            }

            function pageLoad() {
                GetRadWindow().autoSize(true);
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body style="width: 600px; height: 350px">
    <form id="updateform" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div style="margin: 20px;">
        <table style="width: 100%; text-align: left;">
            <tr>
                <td style="width: 30%;">
                    课程名称:
                </td>
                <td>
                    <asp:Label ID="CourseLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    上课教师:
                </td>
                <td>
                    <asp:Label ID="TeacherNameLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    课程状态:
                </td>
                <td>
                    <asp:Label ID="CourseStatusLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    课程开始时间:
                </td>
                <td>
                    <asp:Label ID="StartTimeLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    课程结束时间:
                </td>
                <td>
                    <asp:Label ID="EndTimeLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    课程简介:
                </td>
                <td>
                    <asp:Label ID="CourseIntroLB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;">
                    <telerik:RadButton ID="BookBtn" runat="server" Text="预订" CssClass="OpButton" OnClick="BookBtn_Click">
                    </telerik:RadButton>
                    <telerik:RadButton ID="CancelBtn" runat="server" Text="退订" CssClass="OpButton" AutoPostBack="false"
                        OnClientClicked="ShowCancelConfrimButton">
                    </telerik:RadButton>
                    <telerik:RadButton ID="ExitBtn" runat="server" Text="退出" CssClass="OpButton" AutoPostBack="false"
                        OnClientClicked="Close">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr id="hiddenTR" style="visibility: hidden;">
                <td colspan="2" style="text-align: right;">
                    请简述退订原因:
                    <telerik:RadTextBox ID="CommentTxt" runat="server">
                    </telerik:RadTextBox>
                    <telerik:RadButton ID="CancelConfirmBtn" runat="server" Text="确认退订" CssClass="OpButton"
                        OnClick="CancelBtn_Click">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
        <br />
        <div id="ConfirmDiv" runat="server" visible="false">
            <label style="color: Blue;">
                预订成功! 请点击下面的淘宝链接去在线交费，以便管理员确认此订单生效。</label>
            <br />
            <a>
                <label id="TaobaoLink" runat="server">
                </label>
            </a>
        </div>
    </div>
    </form>
</body>
</html>
