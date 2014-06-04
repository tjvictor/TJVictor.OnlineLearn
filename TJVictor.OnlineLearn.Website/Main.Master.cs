using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Website
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Utils.UserSession] == null)
            {
                loginBtn.Visible = true;
                registerBtn.Visible = true;
                UserBtn.Visible = false;
                ExitBtn.Visible = false;
            }
            else
            {
                loginBtn.Visible = false;
                registerBtn.Visible = false;
                UserBtn.Visible = true;
                ExitBtn.Visible = true;

                UserEntity user = Session[Utils.UserSession] as UserEntity;
                UserBtn.Text = user.UserName;
            }
        }

        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Session[Utils.UserSession] = null;
            Server.Transfer("LoginForm.aspx");
        }

        protected void UserBtn_Click(object sender, EventArgs e)
        {
            Server.Transfer("CourseOrderForm.aspx");
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            Server.Transfer("LoginForm.aspx");
        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            Server.Transfer("RegisterForm.aspx");
        }
    }
}