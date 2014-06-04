using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVictor.OnlineLearn.Biz;

namespace TJVictor.OnlineLearn.Website
{
    public partial class CourseIntroForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gridView.MasterTableView.DataSource = new CourseBiz().GetAllCourseShow();
        }
    }
}