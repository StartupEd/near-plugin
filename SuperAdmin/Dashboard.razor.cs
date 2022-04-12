using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.UX.One.Shared_Classes;
using System.Collections.Generic;
using StartupEd.UX.One.Components.Layout.Base;
using System.Linq;
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.SuperAdmin
{
    public partial class Dashboard : BaseComponent
    {
        private string deleteUserId { get; set; }
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        private CS_User UserService { get; set; }
        //private UserActivity_ClientService ActivityClientService { get; set; }

        private List<UserFullInfo> UserAccounts { get; set; } = new List<UserFullInfo>();
        private List<string> RegistrationPeriod { get; set; } = new List<string>();
        private List<int> RegistrationData { get; set; } = new List<int>();

        public List<User> UserModels { get; set; }
        private UserActivity RecentActivities { get; set; } = new UserActivity();
        private string SelectedUserId { get; set; }

        IJSObjectReference chartModule;
        protected enum LoadingContainerState { Loading, Loaded, Error }
        protected LoadingContainerState State;
        protected LoadingContainerState ActivityState;
        protected LoadingContainerState TodoState;
        protected LoadingContainerState ConnState;
        protected LoadingContainerState EventState;

        private FilterOption filterOption { get; set; } = FilterOption.Today;
        private FilterOption activityOption { get; set; } = FilterOption.Today;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //ActivityClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                UserModels = new List<User>();

                await LoadUserAccountsFromServer();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                // Fetching Total UserModel from Server for Activity Dropdown
                UserModels = await UserService.GetAllUserModelsFromServer(UXUtility.LoggedInUserRole.ToString());
                if (UserModels != null)
                {
                    ActivityState = LoadingContainerState.Loading;
                    SelectedUserId = UserModels[0].Id;
                    if (!string.IsNullOrEmpty(SelectedUserId))
                    {
                        if (!string.IsNullOrEmpty(activityOption.ToString()))
                        {
                            ActivityFilter activityFilter = new ActivityFilter();
                            activityFilter.UserId = SelectedUserId;
                            activityFilter.FilterOption = activityOption;
                            //RecentActivities = await ActivityClientService.GetUserRecentActivityByFilterAsync(activityFilter);
                        }
                    }
                    ActivityState = LoadingContainerState.Loaded;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ActivityState = LoadingContainerState.Error;
            }
        }

        protected async Task LoadUserAccountsFromServer()
        {
            State = LoadingContainerState.Loading;
            if (!string.IsNullOrEmpty(filterOption.ToString()))
            {
                //UserAccounts = await UserService.GetNetworkUserForControlPanel(Utility.GetDisplayName((Enum)filterOption));
                await AccountRegistrationChartAsync(filterOption);
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        public async Task LoadUserActivitiesFromServer(string ActivityUserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedUserId))
                {
                    if (!string.IsNullOrEmpty(activityOption.ToString()))
                    {
                        ActivityFilter activityFilter = new ActivityFilter();
                        activityFilter.UserId = ActivityUserId;
                        activityFilter.FilterOption = activityOption;
                        //RecentActivities = await ActivityClientService.GetUserRecentActivityByFilterAsync(activityFilter);
                    }
                }
                ActivityState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                ActivityState = LoadingContainerState.Error;
            }
        }


        async Task OnChangeForUserAccounts(ChangeEventArgs e)
        {
            try
            {
                filterOption = (FilterOption)Enum.Parse(typeof(FilterOption), e.Value.ToString());
                if (!string.IsNullOrEmpty(filterOption.ToString()))
                {
                    //UserAccounts = await UserService.GetNetworkUserForControlPanel(Utility.GetDisplayName((Enum)filterOption));
                }
                await AccountRegistrationChartAsync(filterOption);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task AccountRegistrationChartAsync(FilterOption filterOption)
        {
            try
            {
                if (UserAccounts != null && UserAccounts.Count != 0)
                {
                    DateTime today = DateTime.UtcNow.ToLocalTime();
                    if (!string.IsNullOrEmpty(filterOption.ToString()))
                    {
                        if (filterOption == FilterOption.Last7Days)
                        {
                            RegistrationPeriod.Clear();
                            RegistrationData.Clear();
                            for (DateTime date = today; date >= today.AddDays(-6); date = date.AddDays(-1))
                            {
                                RegistrationPeriod.Add(date.ToString("dd MMM"));
                                if (UserAccounts != null && UserAccounts.Count != 0)
                                {
                                    RegistrationData.Add(UserAccounts.FindAll(data => data.UserModel.Core.CreatedAt.ToShortDateString() == date.ToShortDateString()).Count);
                                }
                            }
                        }
                        if (filterOption == FilterOption.Last30Days)
                        {
                            RegistrationPeriod.Clear();
                            RegistrationData.Clear();
                            for (DateTime date = today; date >= today.AddDays(-30); date = date.AddDays(-1))
                            {
                                RegistrationPeriod.Add(date.ToString("dd MMM"));
                                if (UserAccounts != null && UserAccounts.Count != 0)
                                {
                                    RegistrationData.Add(UserAccounts.FindAll(data => data.UserModel.Core.CreatedAt.ToShortDateString() == date.ToShortDateString()).Count);
                                }
                            }
                        }
                        RegistrationPeriod.Reverse();
                        RegistrationData.Reverse();
                    }
                    if (RegistrationPeriod.Count > 0 && RegistrationData.Count > 0)
                    {
                        await chartModule.InvokeVoidAsync("LineCharts", "registredAccountChart", RegistrationPeriod, RegistrationData, "Account Registration Chart", "300", "Render");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        async Task OnChangeUserNameForActivity(ChangeEventArgs arg)
        {
            try
            {
                SelectedUserId = arg.Value.ToString();
                if (!string.IsNullOrEmpty(SelectedUserId.ToString()))
                {
                    await LoadUserActivitiesFromServer(SelectedUserId);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ToastService.ShowError(ex.Message, "Exception");
            }
        }

        async Task OnChangeActivityFilter(ChangeEventArgs e)
        {
            try
            {
                activityOption = (FilterOption)Enum.Parse(typeof(FilterOption), e.Value.ToString());
                if (!string.IsNullOrEmpty(activityOption.ToString()))
                {
                    if (!string.IsNullOrEmpty(SelectedUserId.ToString()))
                    {
                        await LoadUserActivitiesFromServer(SelectedUserId);
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ToastService.ShowError(ex.Message, "Exception");
            }
        }








        protected void CallDeleteGroupAlert(string groupId)
        {
            if (groupId != null)
            {
                deleteUserId = groupId;
                ShowSweetAlertBox.ShowSweetAlert();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    IJSObjectReference module2 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "https://s1.startuped.net/incubation_cloud/assets/plugins/custom/leaflet/leaflet.bundle.js");
                    IJSObjectReference _bundle = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "https://s1.startuped.net/incubation_cloud/assets/plugins/custom/draggable/draggable.bundle.js");
                    IJSObjectReference _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/dashboard/dragablewidget.js");
                    await _module.InvokeVoidAsync("DragableWidget");
                    IJSObjectReference module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                    await module.InvokeVoidAsync("BootstrapSelectpicker");
                    chartModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/linechart.js");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
    }
}
