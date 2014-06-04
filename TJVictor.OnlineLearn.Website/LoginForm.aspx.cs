using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TJVictor.OnlineLearn.Biz;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Website
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginIDTxt.Text.Trim()))
            {
                ErrorTxt.Text = "请输入用户名";
                ErrorTxt.Visible = true;
                return;
            }
            if(string.IsNullOrEmpty(PwdTxt.Text.Trim()))
            {
                ErrorTxt.Text = "请输入密码";
                ErrorTxt.Visible = true;
                return;
            }

            UserEntity user = new UserBiz().LoginAuth(LoginIDTxt.Text.Trim(), PwdTxt.Text.Trim());
            if (user == null)
            {
                ErrorTxt.Text = "用户不存在或是密码错误";
                ErrorTxt.Visible = true;
                return;
            }

            Session[Utils.UserSession] = user;
            Server.Transfer("Default.aspx");
        }
    }
}