// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.ProfileTemplates
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
#line 18 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine;

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
#line 54 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

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
#line 78 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Feeds;

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Event;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/_Imports.razor"
using StartupEd.UX.Incubation.Pages.IncubationPrograms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Grouping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/ProfileTemplate.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/profile/{UserId}")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/{ProfileUrl}")]
    public partial class ProfileTemplate : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 374 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/ProfileTemplates/ProfileTemplate.razor"
      
    //var Request = ConnectionList.Find(cr => (cr.FromUserId == UXUtility.LoggedInUser.Id && cr.ToUserId == UserDetails.Id && cr.Status == "Created") || (cr.FromUserId == UserDetails.Id && cr.ToUserId == UXUtility.LoggedInUser.Id && cr.Status == "Created"))

    [Parameter] public string UserId { get; set; }
    [Parameter] public string ProfileUrl { get; set; }
    private Profile ProfileDetails = new Profile();
    private User UserDetails = new User();
    private List<Groups> MyGroups { get; set; }
    private List<Events> AllEvent { get; set; } = new List<Events>();
    private List<IncubationProgram> IPList { get; set; } = new List<IncubationProgram>();
    private List<BusinessPage> Pages { get; set; } = new List<BusinessPage>();
    private List<ConnectionRequest> ConnectionList = new List<ConnectionRequest>();

    private IncubationProgram_ClientService ProgramService { get; set; }
    private Events_ClientService EventService { get; set; }
    private Group_ClientService GroupService { get; set; }
    private User_ClientService UserService { get; set; }
    private Profile_ClientService ProfileService { get; set; }
    private BusinessPage_ClientService PageService { get; set; }
    private ConnectionRequest_ClientService ConnectionService { get; set; }
    public DateTime LocalDate = DateTime.UtcNow;
    public enum LoadingContainerState { Loading, Loaded, Error }
    public LoadingContainerState State;
    private string SearchString { get; set; }

    public bool Error { get; set; } = false;

    private string ConnectButton { get; set; } = "Connect";
    public int i = 0, b;
    private Notification_ClientService NotificationClientService { get; set; }
    private HubConnection notificationProfileViewHubConnection;

    protected async override Task OnInitializedAsync()
    {
        try
        {
            State = LoadingContainerState.Loading;
            ProfileService = new Profile_ClientService(EngineServices.MarketNetwork);
            UserService = new User_ClientService(EngineServices.MarketNetwork);
            EventService = new Events_ClientService(EngineServices.MarketNetwork);
            ProgramService = new IncubationProgram_ClientService(EngineServices.MarketNetwork);
            ConnectionService = new ConnectionRequest_ClientService(EngineServices.MarketNetwork);
            GroupService = new Group_ClientService(EngineServices.MarketNetwork);
            PageService = new BusinessPage_ClientService(EngineServices.MarketNetwork);
            NotificationClientService = new Notification_ClientService(EngineServices.MarketNetwork);
            notificationProfileViewHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();
            await SyncLoggedInUserStatus();
            if (ProfileUrl != null)
            {
                ProfileDetails = await ProfileService.GetDefaultProfileByProfileUrl(ProfileUrl);
                if (ProfileDetails != null)
                {
                    UserId = ProfileDetails.Core.CreatorId;
                }
                else
                {
                    Error = true;
                }
            }
            await notificationProfileViewHubConnection.StartAsync();
            if(UserId != UXUtility.LoggedInUser.Id)
            {
                await SendProfileViewNotification();
            }

        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
        }
    }

    public bool IsProViewNotiConnected => notificationProfileViewHubConnection.State == HubConnectionState.Connected;
    Task SendProviewNotification() => notificationProfileViewHubConnection.SendAsync("SendNotification", UserId, "Your profile viewed");

    private async Task SendProfileViewNotification()
    {
        Notification NotificationModel = new Notification();
        NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
        NotificationModel.ReceiverId = UserId;
        NotificationModel.SentAt = DateTime.UtcNow;
        NotificationModel.NotificationType = "Profile view";
        NotificationModel.Message = UXUtility.LoggedInUser.Name + " viewed your profile";
        NotificationModel.Url = "profile/" + UXUtility.LoggedInUser.Id;
        await NotificationClientService.PostAsync(NotificationModel);
        //NotificationService notificationService = new NotificationService(EngineServices.MarketNetwork);
        //await notificationService.SendAsync(NotificationModel);
        if (IsProViewNotiConnected) await SendProviewNotification();
    }

    protected async override Task OnParametersSetAsync()
    {
        try
        {
            var id = UXUtility.LoggedInUser.Id;
            ProfileDetails = await ProfileService.GetProfileByUserId(UserId);
            Pages = await PageService.GetBusinessPageByUserId(UserId);
            UserDetails = await UserService.GetUserById(UserId);
            AllEvent = await EventService.GetAllEventsByUserId(UserId);
            MyGroups = await GroupService.GetGroupForMemberOrCreator(UserId);
            IPList = await ProgramService.GetIncubationProgramByUserId(UserId);
            ConnectionList = await ConnectionService.GetAllConnectionRequestsForUser(id);

            if (ConnectionList != null && ConnectionList.Count != 0 && UserDetails != null)
            {
                foreach (var Request in ConnectionList)
                {
                    if (Request.FromUserId == UXUtility.LoggedInUser.Id && Request.ToUserId == UserDetails.Core.Id && Request.Status == ConnectionStatus.Created)
                    {
                        ConnectButton = "Cancel Request";
                    }
                    else if (Request.FromUserId == UserDetails.Core.Id && Request.ToUserId == UXUtility.LoggedInUser.Id && Request.Status == ConnectionStatus.Created)
                    {
                        ConnectButton = "Confirm Request";
                    }
                }
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
            if (id != UserId)
            {
                try
                {
                    var Date = DateTime.UtcNow.ToLocalTime().ToString("dd MMM yy");
                    if (!ProfileDetails.ProfileViews.ContainsKey(Date))
                    {
                        List<string> UserIds = new List<string>();
                        UserIds.Add(id);
                        ProfileDetails.ProfileViews.Add(Date, UserIds);
                    }
                    else
                    {
                        if (!ProfileDetails.ProfileViews[Date].Contains(id))
                        {
                            ProfileDetails.ProfileViews[Date].Add(id);
                        }
                        else
                        {
                            return;
                        }
                    }
                    HttpResponseMessage response = await ProfileService.PutAsync(ProfileDetails);

                    if (response.IsSuccessStatusCode)
                    {

                        SLog.Write("Success");
                    }
                    else
                    {
                        ToastService.ShowError("Updation Failed! and Profile Name is taken");
                    }

                }
                catch (Exception ex)
                {
                    SLog.Write(ex.Message);
                }
            }
        }
        catch (Exception e)
        {
            SLog.Write(e.Message);
            State = LoadingContainerState.Error;
        }
    }

    protected async Task ButtonClickCallBackEvent()
    {
        UserDetails = await UserService.GetUserById(UserId);
        ProfileDetails = await ProfileService.GetProfileByUserId(UserId);
        ConnectionList = await ConnectionService.GetAllConnectionRequestsForUser(UXUtility.LoggedInUser.Id);
        StateHasChanged();
        if (ConnectionList != null && ConnectionList.Count != 0 && UserDetails != null)
        {
            foreach (var Request in ConnectionList)
            {
                if (Request.FromUserId == UXUtility.LoggedInUser.Id && Request.ToUserId == UserDetails.Core.Id && Request.Status == ConnectionStatus.Created)
                {
                    ConnectButton = "Cancel Request";
                }
                else if (Request.FromUserId == UserDetails.Core.Id && Request.ToUserId == UXUtility.LoggedInUser.Id && Request.Status == ConnectionStatus.Created)
                {
                    ConnectButton = "Confirm Request";
                }
                else
                {
                    ConnectButton = "Connect";
                }
            }
        }
    }

    protected async Task CommunityButtonClickCallBack()
    {
        MyGroups = await GroupService.GetGroupForMemberOrCreator(UserId);
        StateHasChanged();
    }

    public void GetSearchedString(string str)
    {

        SearchString = str;
        StateHasChanged();
    }


    protected bool IsVisible(BaseModel Model)
    {
        if (string.IsNullOrEmpty(SearchString))
        {
            return true;
        }

        if (Model.Name.ToUpper().StartsWith(SearchString) || Model.Name.ToLower().StartsWith(SearchString))
        {
            return true;
        }
        return false;
    }

    public Dictionary<string, string> ProfileTabs = new Dictionary<string, string>() {
        {"About","fas fa-user"},
        {"Ideas","fas fa-lightbulb"},
        {"Community","fas fa-users"},
        {"Events","fas fa-calendar-alt"},
        {"Incubation Programs","fas fa-user-graduate"},
        {"Business Pages","fab fa-font-awesome-flag"}
    //{"Entrepreneurial Education","fas fa-book"},
};

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
