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
    public partial class Social : BaseComponent
    {
        public Profile Model = new Profile();
        private CS_Profile ProfileService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                Model = await ProfileService.GetProfileByUserId(UserId);
            }
            catch (NullReferenceException e)
            { SLog.Write(e.Message); }

        }


        private async void SubmitInfoAsync()
        {
            try
            {
                HttpResponseMessage response;

                response = await ProfileService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrWhiteSpace(Model.Social.FacebookUrl) || (!string.IsNullOrWhiteSpace(Model.Social.LinkedInUrl)) || (!string.IsNullOrWhiteSpace(Model.Social.TwitterUrl)) || (!string.IsNullOrWhiteSpace(Model.Social.InstagramUrl)) || (!string.IsNullOrWhiteSpace(Model.Social.GithubUrl)))
                    {
                        //UserActivity userActivity = new UserActivity();
                        //Activity Module = new Activity();
                        //Module.CreateActivity("Social Links", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name, "You have recently updated Social Links on profile");
                        //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                        //userActivity.Activities.Add(Module);
                        //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                    }
                    navigationManager.NavigateTo($"settings/educationdetails/{UserId}");
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
        public async void HandleClick()
        {
            navigationManager.NavigateTo($"settings/address/{UserId}");
        }


        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";
    }
}
