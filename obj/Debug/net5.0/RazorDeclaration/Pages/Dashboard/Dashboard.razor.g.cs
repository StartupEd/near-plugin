// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Dashboard
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core.Modules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Grouping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.BPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Others;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.InProgress;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Milestone;

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Chat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Alerts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Pricing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Contact;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.BrowserFlix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Dashboard/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Dashboard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Dashboard/_Imports.razor"
using StartupEd.Engine.MarketNetwork;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Dashboard/Dashboard.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/dashboard")]
    public partial class Dashboard : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 164 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Dashboard/Dashboard.razor"
       
    IJSObjectReference module = null;
    IJSObjectReference module1 = null;

    protected override async void OnInitialized()
    {
        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/draggablecards.js");
        module1 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "https://s1.startuped.net/incubation_cloud/assets/plugins/custom/draggable/draggable.bundle.js");
        await module.InvokeVoidAsync("Draggable");

    }

    private string SearchString { get; set; }
    private User MyDetails { get; set; } = new User();
    private HubConnection hubConnection;

    // **For- Sent Connection Requests **
    public List<User> SentRequestUser { get; set; }
    public List<Profile> SentRequestProfile { get; set; }
    /** here::Client Service Call for User & Profile....**/
    private User_ClientService UserClientService { get; set; }
    private Profile_ClientService ProfileClientService { get; set; }

    private ConnectionRequest_ClientService RequestService { get; set; }
    private ConnectionRequest RequestModel = new ConnectionRequest();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserClientService = new User_ClientService(EngineServices.MarketNetwork);
            ProfileClientService = new Profile_ClientService(EngineServices.MarketNetwork);

            MyDetails = await UserClientService.GetUserById(UXUtility.LoggedInUser.Id); // If Cache Service will Work properly, can Remove this Rest Call

            SentRequestUser = await UserClientService.GetSentConnectionRequestsFromServer(UXUtility.LoggedInUser.Id);
            SentRequestProfile = await ProfileClientService.GetProfileForSentConnectionRequest(UXUtility.LoggedInUser.Id);
            StateHasChanged();

            hubConnection = new HubConnectionBuilder()
        .WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();

            await hubConnection.StartAsync();
        }
        catch (Exception e)
        {
            SLog.Write(e.Message);
            ToastService.ShowError("Not Able To Find Connections!", "Connection Exception!");
        }
    }


    protected async Task CancelRequestCallBack(bool value)
    {
        if (value)
        {
            //System.Threading.Thread.Sleep(2000);
            SentRequestUser = await UserClientService.GetSentConnectionRequestsFromServer(UXUtility.LoggedInUser.Id);
            SentRequestProfile = await ProfileClientService.GetProfileForSentConnectionRequest(UXUtility.LoggedInUser.Id);
            StateHasChanged();
        }
    }

    protected bool IsViewSentRequestTabVisible(User Connection)
    {
        if (string.IsNullOrEmpty(SearchString))
            return true;

        if (Connection.Persona.ToLower().StartsWith(SearchString) || Connection.Persona.ToUpper().StartsWith(SearchString))
            return true;

        if (Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString))
            return true;

        return false;
    }

   

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
