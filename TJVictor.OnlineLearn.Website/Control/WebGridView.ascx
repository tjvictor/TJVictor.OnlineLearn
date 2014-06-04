<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGridView.ascx.cs"
    Inherits="TJVictor.OnlineLearn.Website.WebGridView" %>
<telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                var manager = Sys.WebForms.PageRequestManager.getInstance();
                manager.add_pageLoaded(function () { InitToolbarButton(); });
            }
            );
        })($telerik.$);

        function InitToolbarButton() {
            var toolBar = $find('<%=ToolBar.ClientID %>');
            var grid = $find('<%=GridView.ClientID %>');
            if (toolBar && grid) {
                GridViewRowSelected(grid, null);
            }
        }

        function GridViewRowSelected(sender, events) {
            var toolBar = $find('<%=ToolBar.ClientID %>');
            var viewBtn = toolBar.findItemByValue('View');
            var modifyBtn = toolBar.findItemByValue('Modify');
            var deleteBtn = toolBar.findItemByValue('Delete');
            if (sender.get_masterTableView().get_selectedItems().length == 1) {
                EnableControl(viewBtn, true);
                EnableControl(modifyBtn, true);
                EnableControl(deleteBtn, true);
            }
            else {
                EnableControl(viewBtn, false);
                EnableControl(modifyBtn, false);
                EnableControl(deleteBtn, false);
            }
        }
        function EnableControl(obj, enable) {
            if (obj)
                obj.set_enabled(enable);
        }

        function getOuterHTML(obj) {
            if (typeof (obj.outerHTML) == "undefined") {
                var divWrapper = document.createElement("div");
                var copyOb = obj.cloneNode(true);
                divWrapper.appendChild(copyOb);
                return divWrapper.innerHTML
            }
            else
                return obj.outerHTML;
        }

        function PrintRadGrid() {
            var previewWnd = window.open('about:blank', 'Print', 'toolbar=no,menubar=no,scrollbar=no,resizable=no;location=no,status=no,width=' + screen.availWidth + ',height=' + screen.availHeight + ',left=0,top=0', false);
            var sh = '<%= Page.ClientScript.GetWebResourceUrl(GridView.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",GridView.Skin.ToLower() == "default" ? ConfigurationManager.AppSettings["Telerik.Skin"] : GridView.Skin)) %>';
            var shBase = '<%= Page.ClientScript.GetWebResourceUrl(GridView.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
            var shBase1 = '<%= Page.ClientScript.GetWebResourceUrl(GridView.GetType(),"Telerik.Web.UI.Skins.ComboBox.css") %>';
            var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
            styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link>";
            styleStr += "<link href = '" + shBase1 + "' rel='stylesheet' type='text/css'></link></head>";
            var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= GridView.ClientID %>').get_element()) + "</body></html>";
            previewWnd.document.open();
            previewWnd.document.write(htmlcontent);
            previewWnd.document.close();
            previewWnd.print();

            if (!$telerik.isChrome) {
                //previewWnd.close();
            }
        }

        function ToolBar_ClientClick(sender, args) {
            switch (args.get_item().get_value()) {
                case "Print":
                    PrintRadGrid();
                    break;
                case "ExportToExcel":
                case "ExportToWord":
                case "ExportToCSV":
                case "ExportToPdf":
                    DisableAjaxRequest();
                    break;
                case "Add":
                    var wind = $find('<%=EidtorWindow.ClientID %>');
                    if (wind) {
                        var originalUrl = wind.get_navigateUrl();
                        wind.set_navigateUrl('<%=EditFormUrl %>?IsEdit=false');
                        wind.show();
                        //wind.autoSize(true);
                    }
                    break;
                case "Modify":
                    var wind = $find('<%=EidtorWindow.ClientID %>');
                    var grid = $find('<%=GridView.ClientID %>');
                    if (wind && grid) {
                        var originalUrl = wind.get_navigateUrl();
                        var selectedItemID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue("ID");
                        wind.set_navigateUrl('<%=EditFormUrl %>?IsEdit=true&SelectedItemID=' + selectedItemID);
                        wind.show();
                    }
                    break;

                default: break;
            }
        }

        function ToolBar_ClientClicking(sender, args) {
            switch (args.get_item().get_value()) {
                case "Delete":
                    if (!confirm('真的要删除这条数据吗?'))
                        args.set_cancel(true);
                    break;
            }
        }

        function DisableAjaxRequest() {
            var amp = $find('<%= RadAjaxManager.GetCurrent(this.Page).ClientID %>');
            if (amp) {
                amp.set_enableAJAX(false);
            }
        }

        function ShowHideFilter(checked) {
            var grid = $find('<%=GridView.ClientID %>');
            if (grid) {
                if (checked)
                    grid.get_masterTableView().showFilterItem();
                else
                    grid.get_masterTableView().hideFilterItem();
            }
        }

        function EidtorWindow_ClientClose(sender, args) {
            var grid = $find('<%=GridView.ClientID %>');
            grid.get_masterTableView().rebind();
        }
    </script>
</telerik:RadScriptBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ToolBar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridView" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="GridView">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridView" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadToolBar ID="ToolBar" runat="server" Width="100%" OnButtonClick="ToolBar_Click"
    OnClientButtonClicking="ToolBar_ClientClicking" OnClientButtonClicked="ToolBar_ClientClick">
    <Items>
        <telerik:RadToolBarButton Text="增加" Value="Add" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton Text="修改" Value="Modify" Enabled="false" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton Text="删除" Value="Delete" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton IsSeparator="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton Text="打印" Value="Print" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton IsSeparator="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton Value="DataFilter">
            <ItemTemplate>
                <asp:CheckBox ID="DataFilter" runat="server" AutoPostBack="false" Text="数据过滤" OnClick="ShowHideFilter(this.checked);" />
            </ItemTemplate>
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton IsSeparator="true">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<telerik:RadGrid ID="GridView" runat="server" PageSize="10" AllowPaging="true" AllowFilteringByColumn="true"
    OnInit="GridView_Init" EnableViewState="true" OnNeedDataSource="GridView_NeedDataSource"
    AllowSorting="true" GroupingEnabled="false" AllowMultiRowSelection="true" AutoGenerateColumns="false"
    EnableLinqExpressions="false">
    <MasterTableView runat="server" AllowMultiColumnSorting="true" AllowSorting="true"
        Width="100%" EnableViewState="true" ShowHeadersWhenNoRecords="true" ShowHeader="true"
        AllowFilteringByColumn="true" DataKeyNames="ID" ClientDataKeyNames="ID" IsFilterItemExpanded="false"
        EnableHeaderContextMenu="true">
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
    </MasterTableView>
    <HeaderContextMenu AutoScrollMinimumHeight="500" EnableAutoScroll="true">
    </HeaderContextMenu>
    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="false">
        <Selecting AllowRowSelect="true" />
        <Resizing AllowResizeToFit="true" AllowColumnResize="false" AllowRowResize="false"
            EnableRealTimeResize="true" ClipCellContentOnResize="true" ResizeGridOnColumnResize="true" />
        <ClientEvents OnRowSelected="GridViewRowSelected" OnRowDeselected="GridViewRowSelected" />
    </ClientSettings>
</telerik:RadGrid>
<telerik:RadWindowManager ID="RadWindowManager" runat="server">
    <Windows>
        <telerik:RadWindow ID="EidtorWindow" runat="server" EnableShadow="true" Behaviors="Default"
            VisibleStatusbar="false"  VisibleOnPageLoad="false"
            Modal="true" ShowOnTopWhenMaximized="true" ShowContentDuringLoad="false" ReloadOnShow="true"
            AutoSize="true" OnClientClose="EidtorWindow_ClientClose">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
