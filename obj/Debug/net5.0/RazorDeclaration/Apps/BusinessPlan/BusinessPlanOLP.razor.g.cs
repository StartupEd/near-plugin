// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Apps.BusinessPlan
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 13 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.Engine.MarketNetwork;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core.Modules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core.Entities.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Grouping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.BPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Others;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.InProgress;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Milestone;

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Chat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Maps;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Alerts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Pricing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Contact;

#line default
#line hidden
#nullable disable
#nullable restore
#line 68 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.BrowserFlix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Modal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.PdfViewer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 78 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 83 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Feeds;

#line default
#line hidden
#nullable disable
#nullable restore
#line 84 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/businessplan/{BplanId}/{BplanName}")]
    public partial class BusinessPlanOLP : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 237 "/Users/ankitbuti/workspace/bigapple_corev5/UXs/Incubation/Apps/BusinessPlan/BusinessPlanOLP.razor"
      
    private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
    [Parameter] public string BplanId { get; set; }
    [Parameter] public string BplanName { get; set; }
    public BusinessPlan Model = new BusinessPlan();
    public BusinessPlan_ClientService BPlanService;
    private UserActivity_ClientService Activity_ClientService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Activity_ClientService = new UserActivity_ClientService(EngineServices.MarketNetwork);
            BPlanService = new BusinessPlan_ClientService(EngineServices.MarketNetwork);
            if (BplanId != null)
            {
                Model = await BPlanService.GetBusinessPlanById(BplanId);
            }

        }
        catch (Exception ex)
        {
            SLog.Write(ex);
            ToastService.ShowError(ex.Message, "BusinessPlan Exception");
        }
    }

    protected void CallDeleteBPlanAlert()
    {
        ShowSweetAlertBox.ShowSweetAlert();
    }
    protected void DeleteBPlanAsync(bool Value)
    {
        if (Value)
        {
            DeleteBplan(Value);
        }
    }

    public async void DeleteBplan(bool Value)
    {
        try
        {
            if (!string.IsNullOrEmpty(BplanId))
            {
                UserActivity userActivity = new UserActivity();
                Activity Module = new Activity();
                Module.CreateActivity("Business Plan", BplanId, Model.VentureName, "You have deleted Business Plan " + Model.VentureName);
                userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                userActivity.Activities.Add(Module);
                await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
            }
            List<BusinessPlan> BP = new List<BusinessPlan>();
            BP = await BPlanService.DeleteObject(BP, BplanId);
            if (BP != null)
            {
                ToastService.ShowSuccess("Bplan Deleted Successfuly");
                navigationManager.NavigateTo("businessplan");
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message, "Deletion Failed");
        }
    }

    public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Business Plan","/businessplan" }
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
