// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Commerce
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
#line 20 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Engine.MarketNetwork;

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
#line 26 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Chat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Alerts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Pricing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Contact;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 68 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/upgradeold")]
    public partial class Upgrade : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 324 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Commerce/Upgrade.razor"
  Subscriptions subscription = new Subscriptions();
        public class Subscriptions
        {
            public string Persona { get; set; }
            public string Currency { get; set; }
            public double BasicMonth { get; set; }
            public double BeginnerMonth { get; set; }
            public double ProfessionalMonth { get; set; }
            public double BasicAnnual { get; set; }
            public double BeginnerAnnual { get; set; }
            public double ProfessionalAnnual { get; set; }
            public EventCallback<string> OnPersonaChange { get; set; }

            public void CalculateSubscription()
            {
                if (Persona != "select" && Currency != "select")
                {
                    if (Persona == "demand" && Currency == "dollar")
                    {
                        BasicMonth = 11.99;
                        BasicAnnual = 119.88;
                        BeginnerMonth = 27.99;
                        BeginnerAnnual = 311.88;
                        ProfessionalMonth = 31.99;
                        ProfessionalAnnual = 359.88;
                    }
                    if (Persona == "demand" && Currency == "rupees")
                    {
                        BasicMonth = 900;
                        BasicAnnual = 8400;
                        BeginnerMonth = 2200;
                        BeginnerAnnual = 22200;
                        ProfessionalMonth = 2500;
                        ProfessionalAnnual = 24000;
                    }
                    if (Persona == "supply" && Currency == "rupees")
                    {
                        BasicMonth = 9000;
                        BasicAnnual = 84000;
                        BeginnerMonth = 22000;
                        BeginnerAnnual = 222000;
                        ProfessionalMonth = 25000;
                        ProfessionalAnnual = 240000;
                    }
                    if (Persona == "supply" && Currency == "dollar")
                    {
                        BasicMonth = 119;
                        BasicAnnual = 1188;
                        BeginnerMonth = 279;
                        BeginnerAnnual = 3108;
                        ProfessionalMonth = 329;
                        ProfessionalAnnual = 3588;
                    }
                    
                }


            }
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
