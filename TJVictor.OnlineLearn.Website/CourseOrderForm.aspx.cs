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
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BindScheduler();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void BindScheduler()
        {
            ScheduleBiz sb = new ScheduleBiz();
            List<Schedule> scheduleList = sb.GetAllSchedule();
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

    }
}