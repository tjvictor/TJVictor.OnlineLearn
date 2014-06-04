<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CourseOrderForm.aspx.cs" Inherits="TJVictor.OnlineLearn.Website.CourseOrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
        <script type="text/javascript">
            function ClientAppointmentDoubleClick(sender, eventArgs) {
                var wind = $find('<%=EidtorWindow.ClientID %>');
                if (wind) {
                    var originalUrl = wind.get_navigateUrl();
                    wind.set_navigateUrl('ScheduleUpdateForm.aspx?S_ID=' + eventArgs.get_appointment().get_id());
                    wind.show();
                }
            }

            function EidtorWindow_ClientClose(sender, args) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="LogScheduler" />
                    <telerik:AjaxUpdatedControl ControlID="RadAjaxManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="LogScheduler">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="LogScheduler" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <div style="width: 900px; margin-top: 20px;">
        <telerik:RadScheduler ID="LogScheduler" runat="server" DataKeyField="ID" DataSubjectField="C_Name"
            OverflowBehavior="Expand" DataStartField="C_StartTime" DataEndField="C_EndTime"
            DayStartTime="00:00:00" DataDescriptionField="T_Name" DayEndTime="23:59:59" ShowFullTime="true"
            ShowAllDayRow="false" AllowDelete="false" AllowInsert="false" ShowFooter="false"
            ReadOnly="true" AllowEdit="false" AppointmentStyleMode="Default" OnClientAppointmentDoubleClick="ClientAppointmentDoubleClick"
            ShowHeader="true" SelectedView="WeekView" OnAppointmentCreated="LogScheduler_AppointmentCreated"
            CustomAttributeNames="Status">
            <AdvancedForm Modal="true" />
            <TimeSlotContextMenuSettings EnableDefault="false" />
            <AppointmentContextMenuSettings EnableDefault="false" />
            <WeekView HeaderDateFormat="yyyy/MM/dd" />
            <DayView HeaderDateFormat="yyyy/MM/dd" />
            <MonthView HeaderDateFormat="yyyy/MM/dd" />
            <TimelineView HeaderDateFormat="yyyy/MM/dd" />
            <AppointmentTemplate>
                <div class="rsAptSubject">
                    <%# Eval("Subject")%>
                </div>
                <%# Eval("Description")%>
            </AppointmentTemplate>
        </telerik:RadScheduler>
    </div>
    <div>
        <telerik:RadWindowManager ID="RadWindowManager" runat="server">
            <Windows>
                <telerik:RadWindow ID="EidtorWindow" runat="server" EnableShadow="true" Behaviors="Default"
                    VisibleStatusbar="false" VisibleOnPageLoad="false" Modal="true"
                    ShowOnTopWhenMaximized="true" ShowContentDuringLoad="false"
                    OnClientClose="EidtorWindow_ClientClose">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </div>
</asp:Content>
