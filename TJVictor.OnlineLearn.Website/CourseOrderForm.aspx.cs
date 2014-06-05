using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVictor.OnlineLearn.Biz;
using System.Drawing;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Website
{
    public partial class CourseOrderForm : BasePage
    {
        public string U_ID { get { UserEntity ue = Session[Utils.UserSession] as UserEntity; return ue.ID; } }

        protected override void PageLoad()
        {
            if (!IsPostBack)
            {
                InitTeacher();
                BindScheduler();
            }

        }

        private void InitTeacher()
        {
            TeacherBiz tb = new TeacherBiz();
            Teacher teaTemp = new Teacher();
            teaTemp.ID = Guid.Empty;
            teaTemp.FirstName = "N/A";
            teaTemp.LastName = "";
            List<Teacher> teacherList = tb.GetAllTeacher();
            teacherList.Insert(0, teaTemp);
            TeacherCB.Items.Clear();

            teacherList.ForEach(p => TeacherCB.Items.Add(new Telerik.Web.UI.RadComboBoxItem(p.T_Name, p.ID.ToString())));
        }

        private void BindScheduler()
        {
            ScheduleBiz sb = new ScheduleBiz();

            string u_ID="";
            string t_ID = "";

            if(MeBtn.Checked)
                u_ID = U_ID;
            if (TeacherCB.SelectedIndex > 0)
                t_ID = TeacherCB.SelectedItem.Value;

            List<Schedule> scheduleList = sb.GetAllScheduleByU_IDAndT_ID(u_ID, t_ID);
            LogScheduler.DataSource = scheduleList.Where<Schedule>(p => p.Status == 1 || p.Status == 2 || p.Status == 4 || p.Status == 5);
        }

        protected void LogScheduler_AppointmentCreated(object sender, Telerik.Web.UI.AppointmentCreatedEventArgs e)
        {
            string status = e.Appointment.Attributes["Status"];

            switch (status)
            {
                case "1":
                    e.Appointment.BackColor = Color.Green;
                    break;
                case "2":
                    e.Appointment.BackColor = Color.Orange;
                    break;
                default:
                    break;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                BindScheduler();
            }
        }

        protected void TeacherCB_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindScheduler();
        }

        protected void MeBtn_CheckedChanged(object sender, EventArgs e)
        {
            BindScheduler();
        }

    }
}