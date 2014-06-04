using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TJVictor.OnlineLearn.Website
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Session["UserEntity"] == null)
                Server.Transfer("LoginForm.aspx", false);
        }
    }
}