<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 900px;">
        <div style="z-index: 0">
            <telerik:RadRotator ID="RotatorImg" runat="server" Width="900px" Height="300px" CssClass="horizontalRotator"
                BannersPath="~/img/Banners" ScrollDuration="500" SlideShowAnimation-Type="Fade"
                FrameDuration="5000" RotatorType="AutomaticAdvance">
            </telerik:RadRotator>
        </div>
        <div>
            <h3 style="background-image: url(img/splitter.png); background-position: bottom;
                background-repeat: no-repeat">
                <span style="margin-right: 20px">课程体系</span>
                <asp:ImageButton ID="f" runat="server" PostBackUrl="~/Default.aspx" ImageUrl="Img/more.gif"
                    ImageAlign="Middle" />
            </h3>
            <table style="text-align: center; color: White; font-size: 12px; margin: 0px 0px 10px 0px">
                <tr style="background-color: #01698E">
                    <td style="width: 100px">
                        阶段
                    </td>
                    <td style="width: 300px">
                        级别
                    </td>
                    <td style="width: auto">
                        具体表现
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #659A32">
                        入门者
                    </td>
                    <td style="background-color: #838280">
                        MOL: L1(CEFRA1-orMETENM0)
                    </td>
                    <td style="background-color: #DEDEDC; color: Black; text-align: left; line-height: 22px;
                        height: 100px">
                        学习者以前学习或接触过英语，但是学到的或记住的很少。一般来说，他们熟悉英文字母/语音，常见的礼貌用语和问候语，并且能回答一些简单问题和要求。他们需要反复复习并掌握基本的语言点，而不是学习一些新的内容。
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #659A32">
                        初学者
                    </td>
                    <td style="background-color: #838280">
                        MOL: L2/L3(CEFR A1, M1)
                    </td>
                    <td style="background-color: #DEDEDC; color: Black; text-align: left; line-height: 22px;
                        height: 100px">
                        学习者能理解并使用我们所熟知的日常用语和基本短语。他们能作自我介绍并介绍他人，能就一些个人信息提问并做出相应回答，比如：他/她住在哪？描述他/她熟悉的人以及他/她所拥有的物品。只有在对方的语速缓慢而清晰的时候，他们才能以简洁的方式回应对方并提供帮助。
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #659A32">
                        高级初学者
                    </td>
                    <td style="background-color: #838280">
                        MOL: L4/L5(CEFR A2,M2)
                    </td>
                    <td style="background-color: #DEDEDC; color: Black; text-align: left; line-height: 22px;
                        height: 100px">
                        学习者能够理解句子并时常谈论所熟悉的事物（比如：基本的个人和家庭信息，购物，街坊邻里，就业等）。他们能够运用一些直接的问句和答语对简单的日常工作进行交流，能够用简单语言描述诸如人物背景，街坊邻里以及他们所需表达的事物之类的情况。
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <h3 style="background-image: url(img/splitter.png); background-position: bottom;
                background-repeat: no-repeat">
                <span style="margin-right: 20px">外教师资</span><asp:ImageButton ID="ImageButton1" runat="server"
                    PostBackUrl="~/Default.aspx" ImageUrl="Img/more.gif" ImageAlign="Middle" />
            </h3>
            <center>
                <telerik:RadRotator ID="RadRotator1" runat="server" Width="800px" Height="250px"
                    CssClass="horizontalRotator" BannersPath="~/img/TeacherShow" ScrollDuration="500"
                    FrameDuration="2000" RotatorType="AutomaticAdvance">
                </telerik:RadRotator>
            </center>
        </div>
        <div>
            <h3 style="background-image: url(img/splitter.png); background-position: bottom;
                background-repeat: no-repeat">
                <span style="margin-right: 20px">学员心声</span><asp:ImageButton ID="ImageButton2" runat="server"
                    PostBackUrl="~/Default.aspx" ImageUrl="Img/more.gif" ImageAlign="Middle" />
            </h3>
            <table style="text-align: left; font-size: 12px; line-height: 22px;">
                <tr>
                    <td>
                        <img src="Img/StudentShow/s1.png" alt="studentshow" style="width: 100px; height: 100px;
                            margin-right: 20px" />
                    </td>
                    <td>
                        Frank选择"美联英语在线VIP"学习英语之前，已经有很好的英语单词基础，但由于觉得自己英语发音不够标准，一直都不敢开口说。他总是认为，作为上市公司的高层，说一口蹩脚英语很不衬自己的身份，而且自己的年纪实在不适合去英语口语培训班与一帮年轻人一起学。后来他选择了网络在线学英语的方式，他觉得既可以练习纯正的英语口语，又解决了之前的尴尬。
                    </td>
                </tr>
                <tr>
                    <td style="margin-right: 20px">
                        <img src="Img/StudentShow/s2.jpg" alt="studentshow" style="width: 100px; height: 100px;" />
                    </td>
                    <td>
                        课时随时定 旅途不寂寞 。经常出差，旅途中可以随时和不同老师交谈，既提高了英语水平，也让枯燥的出差生活充满乐趣。
                    </td>
                </tr>
                <tr>
                    <td style="margin-right: 20px">
                        <img src="Img/StudentShow/s3.jpg" alt="studentshow" style="width: 100px; height: 100px;" />
                    </td>
                    <td>
                        Lily到外企工作前，英语水平已通过了全国英语等级考试六级，所以在此之前，Lily对自己都是非常有信心的，认为这份工作对自己完全就是Easy-peasy可是真正上班后，她发现自己有时候不能理解总监交代的任务，而且涉及到与外籍同事的邮件沟通，总会有同事抱怨邮件内容繁琐，不太明白她表达的意思，这让Lily一度觉得非常受挫。来到"美联英语在线VIP"学习后，Lily开始明白自己存在的问题并得到了有效的解决，现在工作也越来越顺利，压力小了很多。
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <h3 style="background-image: url(img/splitter.png); background-position: bottom;
                background-repeat: no-repeat">
                <span style="margin-right: 20px">在线选课</span><asp:ImageButton ID="ImageButton3" runat="server"
                    PostBackUrl="~/Default.aspx" ImageUrl="Img/more.gif" ImageAlign="Middle" />
            </h3>
            <img src="Img/process.png" alt="Process" />
        </div>
    </div>
</asp:Content>
