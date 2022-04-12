using Microsoft.AspNetCore.Components;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StartupEd.Lib.Engine;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BPage
{
    public partial class HostBusinessPage :BaseComponent
    {
        public BusinessPage Model = new BusinessPage();
        public CS_BusinessPage ClientService { get; set; }
        public CS_FileUploader UploaderService { get; set; }
        public bool Validation = true;
        public List<Select> IncubationServices { get; set; } = new List<Select>();
        //private Notification_ClientService NotificationClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        private HubConnection notificationBPageHubConnection;
        private CS_User UserService { get; set; }
        public List<User> UserConnections = new List<User>();

        IJSObjectReference module = null;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/wizards/businessPage_wizard.js");
                    await module.InvokeVoidAsync("Wizard", "businessPage", "businessPageForm");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }


        public async Task HandleClick()
        {
            Model.Category = await module.InvokeAsync<string>("SelectPage");
        }


        public async void Imagebase64(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return;
            }
            Model.Core.ImageUrl = await UploaderService.UploadFile(src, Model.GetType());
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Model.Category = "STARTUP";
                ClientService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                UploaderService = new CS_FileUploader(ClientExchange,EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                notificationBPageHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationBPageHubConnection.StartAsync();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }
        public bool IsBPageNotiConnected => notificationBPageHubConnection.State == HubConnectionState.Connected;
        Task SendBPageNotification() => notificationBPageHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Business page");

        private async void SubmitInfo()
        {
            try
            {
                Model.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "businessPageAbout");
                if (Model.Core.Description == "<p><br></p>" || Model.Name == null || Model.ContactInfo.Email == null || Model.ContactInfo.PhoneNo == null || Model.ContactInfo.Name == null)
                {
                    ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                if (!UXUtility.IsNumberOrDigit(Model.ContactInfo.PhoneNo))
                {
                    ToastService.ShowWarning("Please enter valid phone number.", "Only Numbers Allowed");
                    return;
                }
                if (!UXUtility.IsEmailValid(Model.ContactInfo.Email))
                {
                    ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                    return;
                }
                if (Model.Category == "INCUBATOR")
                {
                    var x = await JSRuntime.InvokeAsync<Object>("getName", "incubationServices");

                    IncubationServices = JsonConvert.DeserializeObject<List<Select>>(x.ToString());
                    if (IncubationServices != null)
                    {
                        foreach (var Name in IncubationServices)
                        {
                            Model.IncubatorDetails.IncubationServices.Add(Name.Text);
                        }
                    }
                }

                HttpResponseMessage response = await ClientService.PostAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    await BusinessPageRecentActivity(response.Content.ReadAsStringAsync().Result, Model.Name, "You have created new Business Page" + Model.Name);

                    UserConnections = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                    foreach (var u in UserConnections)
                    {
                        Notification NotificationModel = new Notification();
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = u.Id;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Business page";
                        //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has created new Business page " + Model.Name;
                        //NotificationModel.Url = "businesspage/" + Model.Id + "/" + Model.Name;
                        //await NotificationClientService.PostAsync(NotificationModel);
                        if (IsBPageNotiConnected) await SendBPageNotification();
                    }
                    ToastService.ShowSuccess("Business Page Created Successfully!");
                    navigationManager.NavigateTo("/businesspage");
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

        public async void GetLocation(PhysicalAddress Address)
        {
            Model.Address.Country = Address.Country;
            Model.Address.City = Address.City;
            Model.Address.State = Address.State;
            Model.Address.Zip = Address.Zip;
            StateHasChanged();
        }

        protected async Task BusinessPageRecentActivity(string Id, string Title, string description)
        {
            UserActivity userActivity = new UserActivity();
            Model_Activity Module = new Model_Activity();
            //Module.CreateActivity("Business Page", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }

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


        public List<string> FocusIndustry = new List<string>() { "Agriculture", "Art", "Automotive", "Beauty", "Blockchain", "CleanTechnology", "Construction", "Consulting", "Consumer", "Cryptocurrency", "Design", "DigitalMarketing", "Ecommerce", "Education", "EducationTechnology", "Energy", "Enterprises",
        "Entertainment",  "Events", "Fashion", "Finance", "FinTech", "HealthWellness", "Healthcare", "Hospitality", "HumanResource", "InformationTechnology", "InternetOfThings", "LifeScience",
        "Logistics", "Manufacturing", "Media", "MedicalDevices", "MobileApplications", "Platforms", "Realestate", "Restaurant", "Retail", "SocialNetwork", "Sports", "Startups", "Technology", "Travel", "TravelTourism", "UxDesign", "WebDevelopment",
        "Websites" };

        public List<string> IncubationServiceList = new List<string>() { "B-Plan", "Business Launch", "Business Management", "Funding", "Co-working Space",
        "Training & Education", "Networking Activities" ,"Marketing Assistance","Market Research","Served affiliate or Virtual clients"};

        public List<string> StartupStage = new List<string>() { "Ideation", "Validation", "Early Traction", "Scaling" };


        public string InputElementStyle { get; set; } = "form-control form-control-lg form-control-solid";
        public string LabelStyle { get; set; } = "";
        public string ElementWrapperStyle { get; set; } = "";
        public string FormElementStyle { get; set; } = "";
    }
}
