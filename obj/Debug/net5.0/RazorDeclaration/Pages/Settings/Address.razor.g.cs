// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Settings
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
#line 4 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Settings/_Imports.razor"
using StartupEd.Lib.Engine.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(SettingsLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/settings/address/{userId}")]
    public partial class Address : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 86 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Settings/Address.razor"
      
    [Parameter]
    public string UserId { get; set; }
    private User Model = new User();
    private User_ClientService ClientService { get; set; }
    private UserActivity_ClientService Activity_ClientService { get; set; }

    private GeoLocation GeoLocation = new GeoLocation();


    protected async override Task OnInitializedAsync()
    {
        try
        {
            Activity_ClientService = new UserActivity_ClientService(EngineServices.MarketNetwork);
            ClientService = new User_ClientService(EngineServices.MarketNetwork);
            Model = await ClientService.GetUserById(UserId);
        }
        catch (NullReferenceException e)
        {
            SLog.Write(e.Message);
        }

    }

    public async void GetLocation(PhysicalAddress Address)
    {
        Model.Address.Country = Address.Country;
        Model.Address.City = Address.City;
        Model.Address.State = Address.State;
        Model.Address.Zip = Address.Zip;
        Model.Address.Lattitude = Address.Lattitude;
        Model.Address.Longitude = Address.Longitude;
        StateHasChanged();
    }

    private async void SubmitInfoAsync()
    {
        try
        {
            if (Model.Address.Zip != null)
            {
                if (!UXUtility.IsNumberOrDigit(Model.Address.Zip))
                {
                    ToastService.ShowWarning("Please enter valid Zip Code.", "Alphabet Not Allowed");
                    return;
                }
            }

            string region = Model.Address.City + " " + Model.Address.State + " " + Model.Address.Country + " " + Model.Address.Zip;
            Coordinate Location = await ClientService.GetCoordinates(region);
            Model.Address.Lattitude = Location.Latitude;
            Model.Address.Longitude = Location.Longitude;

            HttpResponseMessage response = await ClientService.PutAsync(Model);
            if (response.IsSuccessStatusCode)
            {
                UserActivity userActivity = new UserActivity();
                Activity Module = new Activity();
                Module.CreateActivity("Profile Details", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name, "You have recently updated Address on profile");
                userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                userActivity.Activities.Add(Module);
                await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                navigationManager.NavigateTo($"settings/social/{UserId}");
            }
            else
            {
                ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex);
        }
    }

    public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
    public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
    public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
    public string FormElementStyle { get; set; } = "row";


    public List<string> CountryList = new List<string>(){
        "Afghanistan","??land Islands","Albania","Algeria","American Samoa","Andorra","Angola","Anguilla","Antarctica","Antigua and Barbuda",
        "Argentina","Armenia","Aruba","Australia","Austria","Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium",
        "Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Bouvet Island","Brazil",
        "British Indian Ocean Territory","Brunei Darussalam","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon","Canada",
        "Cape Verde","Cayman Islands","Central African Republic","Chad","Chile","China","Christmas Island","Cocos (Keeling) Islands",
        "Colombia","Comoros","Congo","Congo, The Democratic Republic of TheCook Islands","Costa Rica","Cote D'ivoire","Croatia",
        "Cuba","Cyprus","Czech Republic","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador",
        "Equatorial Guinea","Eritrea","Estonia","Ethiopia","Falkland Islands (Malvinas)","Faroe Islands","Fiji","Finland","France",
        "French Guiana","French Polynesia","French Southern Territories","Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar",
        "Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guernsey","Guinea","Guinea-bissau","Guyana","Haiti",
        "Heard Island and Mcdonald Islands","Holy See (Vatican City State)","Honduras","Hong Kong","Hungary","Iceland","India",
        "Indonesia","Iran","Iraq","Ireland","Isle of ManIsrael","Italy","Jamaica","Japan","Jersey","Jordan","Kazakhstan","Kenya",
        "Kiribati","Korea","Kuwait","Kyrgyzstan","Lao People's Democratic Republic","Latvia","Lebanon","Lesotho","Liberia","Libyan Arab Jamahiriya",
        "Liechtenstein","Lithuania","Luxembourg","MacaoMacedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands",
        "Martinique","Mauritania","Mauritius","Mayotte","Mexico","Micronesia","Moldova","Monaco","Mongolia","Montenegro",
        "Montserrat","Morocco","Mozambique","Myanmar","Namibia","Nauru","Nepal","Netherlands","Netherlands Antilles","New Caledonia",
        "New Zealand","Nicaragua","Niger","Nigeria","NiueNorfolk Island","Northern Mariana Islands","Norway","Oman","Pakistan","Palau",
        "Palestinian Territory","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn","Poland","Portugal","Puerto Rico",
        "Qatar","Reunion","Romania","Russian Federation","Rwanda","Saint Helena","Saint Kitts and Nevis","Saint Lucia",
        "Saint Pierre and Miquelon","Saint Vincent and The Grenadines","SamoaSan Marino","Sao Tome and Principe","Saudi Arabia",
        "Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands","Somalia","South Africa",
        "South Georgia and The South Sandwich Islands","Spain","Sri Lanka","Sudan","Suriname","Svalbard and Jan Mayen","Swaziland",
        "Sweden","Switzerland","Syrian Arab Republic","Taiwan (ROC)","Tajikistan","Tanzania","Thailand","Timor-leste","Togo","Tokelau",
        "Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan","Turks and Caicos Islands","Tuvalu","Uganda","Ukraine","United Arab Emirates",
        "United Kingdom","United States","United States Minor Outlying Islands","Uruguay","Uzbekistan","Vanuatu","Venezuela","Vietnam",
        "Virgin Islands","Yemen","Zambia","Zimbabwe" };

    public async void HandleClick()
    {
        navigationManager.NavigateTo($"settings/profile/{UserId}");
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService LoginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
