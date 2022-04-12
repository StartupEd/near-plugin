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
    public partial class ProfileSettings : BaseComponent
    {
        public Profile Model { get; set; } = new Profile();
        public CS_Profile ClientService { get; set; }
        public CS_FileUpload UploaderService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        [Parameter]
        public string UserId { get; set; }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                UploaderService = new CS_FileUpload(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetProfileByUserId(UserId);
            }
            catch (NullReferenceException e)
            { SLog.Write(e.Message); }

        }

        public async void Imagebase64(string src)
        {
            //Model.Core.ImageUrl = await UploaderService.
            //StateHasChanged();
        }

        public void SpaceRemove(ChangeEventArgs args)
        {
            Model.ProfileUrl = Model.ProfileUrl.Replace(" ", "_");
            StateHasChanged();
        }

        private async void SubmitInfoAsync()
        {
            try
            {
                Model.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "userAbout");
                if (Model.Core.Description == "<p><br></p>")
                {
                    ToastService.ShowWarning("Please Try Again :)", "Oops! Empty About Section");
                    return;
                }

                if (Model.ProfileUrl.Trim() == "")
                {
                    ToastService.ShowError("Oops! Empty Profile Address");
                    return;
                }

                HttpResponseMessage response = await ClientService.PutAsync(Model);

                if (response.IsSuccessStatusCode)
                {
                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("Profile Details", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name, "You have recently updated profile information.");
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);

                    ToastService.ShowSuccess("Success");
                    LoginService.UpdateLocalStorageForImage(Model.Core.ImageUrl);

                    navigationManager.NavigateTo($"settings/address/{UserId}");
                }
                else
                {
                    ToastService.ShowError("Updation Failed! and Profile Name is taken");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        public async void HandleClick()
        {
            navigationManager.NavigateTo($"settings/personal/{UserId}");
        }

        public List<string> FocusIndustry = new List<string>() { "Agriculture", "Art", "Automotive", "Beauty", "Blockchain", "CleanTechnology", "Construction", "Consulting", "Consumer", "Cryptocurrency", "Design", "DigitalMarketing", "Ecommerce", "Education", "EducationTechnology", "Energy", "Enterprises",
        "Entertainment",  "Events", "Fashion", "Finance", "FinTech", "HealthWellness", "Healthcare", "Hospitality", "HumanResource", "InformationTechnology", "InternetOfThings", "LifeScience",
        "Logistics", "Manufacturing", "Media", "MedicalDevices", "MobileApplications", "Platforms", "Realestate", "Restaurant", "Retail", "SocialNetwork", "Sports", "Startups", "Technology", "Travel", "TravelTourism", "UxDesign", "WebDevelopment",
        "Websites", "Others"};



        public List<string> InvestorType = new List<string>() { "Angel", "Syndicate Lead", "Seed Fund", "VC", "All" };

        public List<string> FounderStage = new List<string>() { "Would-be Founder", "Early Stage Founder", "Serial Founder" };


        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";
    }
}
