<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CourseLogForm.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.CourseLogForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 900px; margin-top: 20px;" >
        <telerik:RadGrid ID="GridView" runat="server" PageSize="10" AllowPaging="true" AllowFilteringByColumn="true"
            OnInit="GridView_Init" EnableViewState="true" OnNeedDataSource="GridView_NeedDataSource"
            AllowSorting="true" GroupingEnabled="false"  AutoGenerateColumns="false"
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
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>
