using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using TJVictor.OnlineLearn.Biz;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Website
{
    public partial class CourseLogForm : BasePage
    {
        protected override void PageLoad()
        {
            if (!IsPostBack)
            {
                InitGridView();
            }
        }

        private void InitGridView()
        {
            GridView.MasterTableView.Columns.Clear();

            GridBoundColumn C_NameColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(C_NameColumn);
            C_NameColumn.DataField = "C_Name";
            C_NameColumn.HeaderText = "课程名称";
            C_NameColumn.DataType = typeof(string);
            C_NameColumn.UniqueName = "C_Name";
            C_NameColumn.SortExpression = "C_Name";
            C_NameColumn.AllowSorting = true;

            GridBoundColumn T_NameColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(T_NameColumn);
            T_NameColumn.DataField = "T_Name";
            T_NameColumn.HeaderText = "上课教师";
            T_NameColumn.DataType = typeof(string);
            T_NameColumn.UniqueName = "T_Name";
            T_NameColumn.SortExpression = "T_Name";

            GridBoundColumn C_StartTimeColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(C_StartTimeColumn);
            C_StartTimeColumn.DataField = "C_StartTime";
            C_StartTimeColumn.HeaderText = "上课时间";
            C_StartTimeColumn.DataType = typeof(string);
            C_StartTimeColumn.UniqueName = "C_StartTime";
            C_StartTimeColumn.SortExpression = "C_StartTime";

            GridBoundColumn C_EndTimeColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(C_EndTimeColumn);
            C_EndTimeColumn.DataField = "C_EndTime";
            C_EndTimeColumn.HeaderText = "下课时间";
            C_EndTimeColumn.DataType = typeof(string);
            C_EndTimeColumn.UniqueName = "C_EndTime";
            C_EndTimeColumn.SortExpression = "C_EndTime";

            GridBoundColumn CreateTimeColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(CreateTimeColumn);
            CreateTimeColumn.DataField = "CreateTime";
            CreateTimeColumn.HeaderText = "创建时间";
            CreateTimeColumn.DataType = typeof(string);
            CreateTimeColumn.UniqueName = "CreateTime";
            CreateTimeColumn.SortExpression = "CreateTime";

            GridBoundColumn UpdateTimeColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(UpdateTimeColumn);
            UpdateTimeColumn.DataField = "UpdateTime";
            UpdateTimeColumn.HeaderText = "更新时间";
            UpdateTimeColumn.DataType = typeof(string);
            UpdateTimeColumn.UniqueName = "UpdateTime";
            UpdateTimeColumn.SortExpression = "UpdateTime";

            //GridBoundColumn S_IDColumn = new GridBoundColumn();
            //GridView.MasterTableView.Columns.Add(S_IDColumn);
            //S_IDColumn.DataField = "S_ID";
            //S_IDColumn.HeaderText = "操作用户";
            //S_IDColumn.DataType = typeof(string);
            //S_IDColumn.UniqueName = "S_ID";
            //S_IDColumn.SortExpression = "S_ID";

            GridBoundColumn StatusColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(StatusColumn);
            StatusColumn.DataField = "StatusStr";
            StatusColumn.HeaderText = "状态";
            StatusColumn.DataType = typeof(string);
            StatusColumn.UniqueName = "StatusStr";
            StatusColumn.SortExpression = "StatusStr";

            GridBoundColumn TaobaoLinkColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(TaobaoLinkColumn);
            TaobaoLinkColumn.DataField = "TaobaoLink";
            TaobaoLinkColumn.HeaderText = "淘宝链接";
            TaobaoLinkColumn.DataType = typeof(string);
            TaobaoLinkColumn.UniqueName = "TaobaoLink";
            TaobaoLinkColumn.SortExpression = "TaobaoLink";

            GridBoundColumn CommentColumn = new GridBoundColumn();
            GridView.MasterTableView.Columns.Add(CommentColumn);
            CommentColumn.DataField = "Comment";
            CommentColumn.HeaderText = "描述";
            CommentColumn.DataType = typeof(string);
            CommentColumn.UniqueName = "Comment";
            CommentColumn.SortExpression = "Comment";
        }

        protected void GridView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            UserEntity ue = Session[Utils.UserSession] as UserEntity;
            GridView.MasterTableView.DataSource = new ScheduleBiz().GetUserScheduleLog(ue.ID);
        }

        protected void GridView_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = GridView.FilterMenu;
            foreach (RadMenuItem item in menu.Items)
            {
                switch (item.Text)
                {
                    case "NoFilter": item.Text = "消除过滤"; break;
                    case "Contains": item.Text = "包含"; break;
                    case "DoesNotContain": item.Text = "不包含"; break;
                    case "StartsWith": item.Text = "开始于"; break;
                    case "EndsWith": item.Text = "结束于"; break;
                    case "EqualTo": item.Text = "等于"; break;
                    case "NotEqualTo": item.Text = "不等于"; break;
                    case "GreaterThan": item.Text = "大于"; break;
                    case "LessThan": item.Text = "小于"; break;
                    case "GreaterThanOrEqualTo": item.Text = "大于等于"; break;
                    case "LessThanOrEqualTo": item.Text = "小于等于"; break;
                    case "IsEmpty": item.Text = "为空"; break;
                    case "NotIsEmpty": item.Text = "不为空"; break;
                    case "IsNull": item.Text = "无效数据"; break;
                    case "NotIsNull": item.Text = "有效数据"; break;
                    case "Between": item.Text = "之间"; break;
                    case "NotBetween": item.Text = "之外"; break;
                    default: item.Enabled = false; break;
                }
            }
        }
    }
}