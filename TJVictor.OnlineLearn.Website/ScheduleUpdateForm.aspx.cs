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
    public partial class ScheduleUpdateForm : System.Web.UI.Page
    {
        public string S_ID { get { return Request.QueryString["S_ID"]; } }

        public string U_ID { get { UserEntity ue = Session[Utils.UserSession] as UserEntity; return ue.ID; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSchedule();
            }
        }

        private void InitSchedule()
        {
            ScheduleBiz sb = new ScheduleBiz();
            Schedule s = sb.GetScheduleByS_ID(S_ID);

            CourseLB.Text = s.C_Name;
            TeacherNameLB.Text = s.T_Name;
            CourseStatusLB.Text = s.StatusStr;
            CourseStatusLB.ForeColor = s.StatusBackColor;
            StartTimeLB.Text = s.C_StartTime.ToString("yyyy-MM-dd HH:mm");
            EndTimeLB.Text = s.C_EndTime.ToString("yyyy-MM-dd HH:mm");
            CourseIntroLB.Text = s.C_Des;

            TaobaoLink.InnerText = s.TaobaoLink;

            switch (s.Status)
            {
                case 1: CancelBtn.Visible = false; break;
                case 2:
                    UserBiz ub = new UserBiz();

                    if (ub.IsUserBooked(U_ID, S_ID))
                        BookBtn.Visible = false;
                    else
                    {
                        CancelBtn.Visible = false; BookBtn.Visible = false;
                    }
                    break;
                default: CancelBtn.Visible = false; BookBtn.Visible = false; break;
            }
        }

        protected void BookBtn_Click(object sender, EventArgs e)
        {
            ScheduleBiz sb = new ScheduleBiz();
            sb.UserBookSchedule(S_ID, U_ID);
            InitSchedule();
            ConfirmDiv.Visible = true;
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            ScheduleBiz sb = new ScheduleBiz();
            sb.CancelSchedule(sb.GetScheduleByS_ID(S_ID), U_ID, CommentTxt.Text.Trim());

            
            string radconfirmscript = "<script language='javascript'>function f(){CanceledSchedule(); Sys.Application.remove_load(f) ;}; Sys.Application.add_load (f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radconfirm", radconfirmscript);
        }
    }
}