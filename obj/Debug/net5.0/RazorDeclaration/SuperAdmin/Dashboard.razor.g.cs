// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.SuperAdmin
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 13 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Grouping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.BPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Others;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.InProgress;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Milestone;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Maps;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Card;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Flix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Grid;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.List;

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Modal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.PdfViewer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.Lib.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.Engine.MarketNetwork;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Content;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Inbox;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Root;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Dashboard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Feeds;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.One.Components.Custom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.One.Components.Metronics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/Dashboard.razor"
using System.ComponentModel.DataAnnotations;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/root/dashboard")]
    public partial class Dashboard : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 187 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/SuperAdmin/Dashboard.razor"
       

    private string deleteUserId { get; set; }
    private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
    private User_ClientService UserService { get; set; }
    private UserActivity_ClientService ActivityClientService { get; set; }

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
            UserService = new User_ClientService(EngineServices.MarketNetwork);
            ActivityClientService = new UserActivity_ClientService(EngineServices.MarketNetwork);
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
        try {
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
                        RecentActivities = await ActivityClientService.GetUserRecentActivityByFilterAsync(activityFilter);
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
            UserAccounts = await UserService.GetNetworkUserForControlPanel(Utility.GetDisplayName((Enum)filterOption));
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
                    RecentActivities = await ActivityClientService.GetUserRecentActivityByFilterAsync(activityFilter);
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
                UserAccounts = await UserService.GetNetworkUserForControlPanel(Utility.GetDisplayName((Enum)filterOption));
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



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService LoginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
