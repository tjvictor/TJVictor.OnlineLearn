using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Reflection;

namespace TJVictor.OnlineLearn.Website
{
    public partial class WebGridView : System.Web.UI.UserControl
    {
        public delegate void GridViewToolBarAddHandle(object sender, RadToolBarEventArgs e);
        public delegate void GridViewToolBarModifyHandle(object sender, RadToolBarEventArgs e);
        public delegate void GridViewToolBarDeleteHandle(object sender, RadToolBarEventArgs e);
        public delegate void GridViewInitialHandle(object sender, RadToolBarEventArgs e);

        public event GridViewToolBarAddHandle GridViewToolBarAddEvent;
        public event GridViewToolBarModifyHandle GridViewToolBarModifyEvent;
        public event GridViewToolBarDeleteHandle GridViewToolBarDeleteEvent;
        public event GridViewInitialHandle GridViewInitialEvent;

        public RadGrid RadGridView { get { return GridView; } }
        public RadToolBar RadWebToolBar { get { return ToolBar; } }
        public string EditFormUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitGridView();
            }
        }

        private void InitGridView()
        {
            GridView.MasterTableView.Columns.Clear();
            if (GridViewInitialEvent != null)
            {
                GridViewInitialEvent(null, null);
                return;
            }
            GridView.MasterTableView.AllowFilteringByColumn = ((CheckBox)ToolBar.FindItemByValue("DataFilter").FindControl("DataFilter")).Checked;
        }

        protected void GridView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }

        protected void ToolBar_Click(object sender, RadToolBarEventArgs e)
        {
            //CheckBox exportAllData = ToolBar.FindItemByValue("ExportAllData").FindControl("ExportAllData") as CheckBox;
            //if (exportAllData != null)
            //{
            //    if (exportAllData.Checked)
            //        GridView.ExportSettings.IgnorePaging = true;
            //    else
            //        GridView.ExportSettings.IgnorePaging = false;
            //}
            switch (e.Item.Value)
            {
                case "Add": if (GridViewToolBarAddEvent != null) GridViewToolBarAddEvent(sender, e); break;
                case "Modify": if (GridViewToolBarModifyEvent != null) GridViewToolBarModifyEvent(sender, e); break;
                case "Delete": if (GridViewToolBarDeleteEvent != null) GridViewToolBarDeleteEvent(sender, e); 
                    //DeleteGridViewItem(); 
                    GridView.MasterTableView.Rebind(); break;
                case "ExportToExcel": GridView.MasterTableView.ExportToExcel(); break;
                case "ExportToWord": GridView.MasterTableView.ExportToWord(); break;
                case "ExportToCSV": GridView.MasterTableView.ExportToCSV(); break;
                case "ExportToPdf": GridView.MasterTableView.ExportToPdf(); break;
                default: break;
            }
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

        private void DeleteGridViewItem()
        {
        }
    }
}