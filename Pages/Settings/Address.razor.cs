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

namespace StartupEd.UX.Incubation.Pages.Settings
{
    public partial class Address : BaseComponent
    {
        [Parameter]
        public string UserId { get; set; }
        private User Model = new User();
        private CS_User ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        private GeoLocation GeoLocation = new GeoLocation();


        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
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
                //Coordinate Location = await ClientService.GetCoordinates(region);
                //Model.Address.Lattitude = Location.Latitude;
                //Model.Address.Longitude = Location.Longitude;

                HttpResponseMessage response = await ClientService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("Profile Details", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name, "You have recently updated Address on profile");
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
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
        "Afghanistan","Åland Islands","Albania","Algeria","American Samoa","Andorra","Angola","Anguilla","Antarctica","Antigua and Barbuda",
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
    }
}
