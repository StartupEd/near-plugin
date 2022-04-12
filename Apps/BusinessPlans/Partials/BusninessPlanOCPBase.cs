using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Lib.Engine;
using StartupEd.Lib.Core;
using System.Net.Http;
using StartupEd.UX.One.Services;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class BusninessPlanOCPBase : BaseComponent
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        [Parameter]
        public string BplanId { get; set; }
        [Parameter]
        public string BplanName { get; set; }
        public User User = new User();
        public BusinessPlan Model = new BusinessPlan();
        public CS_BusinessPlan BPlanService;
        public CS_User UserService { get; set; }
        public CS_Event EventService { get; set; }
        public List<Event> AllEvents { get; set; } = new List<Event>();
        private List<Select> SelectIndustry { get; set; } = new List<Select>();
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SLog.Write("On Init Called.....");
            Model.Name = UXUtility.LoggedInUser.Name;
            Model.Email = UXUtility.LoggedInUser.Email;

            //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
            UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
            BPlanService = new CS_BusinessPlan(ClientExchange, EngineServices.MarketNetwork);
            if (BplanId != null)
            {
                Model = await BPlanService.GetBusinessPlanById(BplanId);
            }

            AllEvents = await EventService.GetEventsForApps("BusinessPlan", UXUtility.LoggedInUser.Id);
            User = await UserService.GetUserById(UXUtility.LoggedInUser.Id);
            if (User.ContactNo.Mobile != null)
            {
                Model.ContactNo = User.ContactNo.Mobile;
            }

        }

        public async Task SubmitModel()
        {
            try
            {
                //Model.EventName = "DTU Startup Weekend 2020";
                //Model.StartVenture = await JSRuntime.InvokeAsync<DateTime>("DatePickerValue", "startVenture");
                //Model.StartOperations = await JSRuntime.InvokeAsync<DateTime>("DatePickerValue", "startOperations");
                HttpResponseMessage response;
                if (BplanId != null)
                {
                    if (!UXUtility.IsNumberOrDigit(Model.ContactNo))
                    {
                        //ToastService.ShowWarning("Please enter valid contact number.", "Alphabet Not Allowed");
                        return;
                    }
                    if (string.IsNullOrEmpty(Model.VentureName))
                    {
                        //ToastService.ShowWarning("Please fill venture name", "Please Try Again :)");
                        return;
                    }
                    if (!UXUtility.IsEmailValid(Model.Email))
                    {
                        //ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                        return;
                    }
                    var data = await JSRuntime.InvokeAsync<Object>("getName", "industryList");
                    SelectIndustry = JsonConvert.DeserializeObject<List<Select>>(data.ToString());
                    if (SelectIndustry != null)
                    {
                        foreach (var industry in SelectIndustry)
                        {
                            Model.FocusIndustry = industry.Text;
                        }
                    }
                    response = await BPlanService.PutAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                       // await BusinessPlanUserActivity(BplanId, Model.VentureName, "You have made changes into Business Plan " + Model.VentureName);
                       // ToastService.ShowSuccess("Business Plan Updated Successfully", "");
                    }
                    else
                    {
                       // ToastService.ShowSuccess("Oops something went wrong!!", "");
                    }
                }
                else
                {
                    if (!UXUtility.IsNumberOrDigit(Model.ContactNo))
                    {
                        //ToastService.ShowWarning("Please enter valid contact number.", "Alphabet Not Allowed");
                        return;
                    }
                    if (string.IsNullOrEmpty(Model.VentureName))
                    {
                        //ToastService.ShowWarning("Please fill venture name", "Please Try Again :)");
                        return;
                    }
                    if (!UXUtility.IsEmailValid(Model.Email))
                    {
                       // ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                        return;
                    }

                    var data = await JSRuntime.InvokeAsync<Object>("getName", "industryList");
                    SelectIndustry = JsonConvert.DeserializeObject<List<Select>>(data.ToString());
                    if (SelectIndustry != null)
                    {
                        foreach (var industry in SelectIndustry)
                        {
                            Model.FocusIndustry = industry.Text;
                        }
                    }
                    response = await BPlanService.PostAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                        //await BusinessPlanUserActivity(response.Content.ReadAsStringAsync().Result, Model.VentureName, "You have created new Business Plan " + Model.VentureName);
                        //ToastService.ShowSuccess("Business Plan Saved Successfully", "");
                    }
                    else
                    {
                        //ToastService.ShowError(await response.Content.ReadAsStringAsync(), "Error");
                    }
                }
                await BPlanService.BPlanConfirmationEmail(UXUtility.LoggedInUser.Email, UXUtility.LoggedInUser.Name);
                _NavigationManager.NavigateTo("/businessplan");
                //NotifySlack("New Business Plan Posted: '" + Model.VentureName + Model.ProblemStatement + Model.MyMarketType + "'\nBy: " + UXUtility.LoggedInUser.Name + "<" + UXUtility.LoggedInUser.Email + ">");
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Error");
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            IJSObjectReference module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/wizards/bplan_wizard.js");
            await module.InvokeVoidAsync("Wizard", "businessPlanWizard", "businessPlanForm");
        }

       

        public List<string> FocusIndustry = new List<string>() { "Agriculture", "Art", "Automotive", "Beauty", "Blockchain", "CleanTechnology", "Construction", "Consulting", "Consumer", "Cryptocurrency", "Design", "DigitalMarketing", "Ecommerce", "Education", "EducationTechnology", "Energy", "Enterprises",
        "Entertainment",  "Events", "Fashion", "Finance", "FinTech", "HealthWellness", "Healthcare", "Hospitality", "HumanResource", "InformationTechnology", "InternetOfThings", "LifeScience",
        "Logistics", "Manufacturing", "Media", "MedicalDevices", "MobileApplications", "Platforms", "Realestate", "Restaurant", "Retail", "SocialNetwork", "Sports", "Startups", "Technology", "Travel", "TravelTourism", "UxDesign", "WebDevelopment",
        "Websites" };

        public List<string> MyLifeCycle = new List<string>() {
    "Idea Stage","In MVP Development","Has Traction","Has Revenue", "Is Funded"};

        public List<string> MyCurrency = new List<string>(){
        "Dollar","Rupees","Euro","Pounds","Yen"};

        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
            {
        { "Business Plan","/businessplan" }
    };
    }
}
