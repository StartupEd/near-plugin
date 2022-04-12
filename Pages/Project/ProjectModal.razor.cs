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


namespace StartupEd.UX.Incubation
{
    public partial class ProjectModal : BaseComponent
    {
        public List<string> DefaultStatus = new List<string>() { "To do", "Progress", "Done" };
        private Project Model = new Project();
        public CS_Project ClientServices { get; set; }
        public List<string> ProjectType = new List<string>() { "Basic Kanban" };
        [Parameter] public EventCallback<string> OnProjectUpdated { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public string arg { get; set; }
        public List<Model_Event> AllEvents { get; set; } = new List<Model_Event>();
        HttpResponseMessage response { get; set; }

        IJSObjectReference module = null;

        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientServices = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                AllEvents = await ClientServices.GetEventsName(UXUtility.LoggedInUser.Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        public async Task SubmitModel()
        {
            try
            {
                if (Model.Name == null || Model.ProjectType == null)
                {
                    //ToastService.ShowWarning("Your project name or project type is empty");
                    return;
                }

                Model.BoardStatus = DefaultStatus;
                Model.Core.CreatorId = UXUtility.LoggedInUser.Core.Id;
                Model.TeamId.Admins.Add(UXUtility.LoggedInUser.Id);
                Model.TeamId.Members.Add(UXUtility.LoggedInUser.Id);

                response = await ClientServices.PostAsync(Model);
                await ProjectUserActivity(response.Content.ReadAsStringAsync().Result, Model.Name, "You have created a project " + Model.Core.Name + " which is kind of " + Model.ProjectType);

                //if (Model.EventId == "none" || string.IsNullOrEmpty(Model.EventId))
                //{
                //    response = await ClientServices.PostAsync(Model);
                //    await ProjectUserActivity(response.Content.ReadAsStringAsync().Result, Model.Name, "You have created a project " + Model.Core.Name + " which is kind of " + Model.ProjectType);
                //}
                //else
                //{
                //    response = await ClientServices.PostAsync(Model, "Create");

                //    await ProjectUserActivity(response.Content.ReadAsStringAsync().Result, Model.Name, "You have created a project " + Model.Core.Name + " related to an event " + AllEvents.Find(e => e.Core.Id == Model.EventId).Name);
                //}

                if (response.IsSuccessStatusCode)
                {
                    await OnProjectUpdated.InvokeAsync(arg);
                    await module.InvokeAsync<object>("HideModal", "projectModal");
                    Model.Core.Name = null;

                    //ToastService.ShowSuccess("Project Created", "");
                }
                else
                {
                    SLog.Write("Project Creation  Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Oops something went wrong!!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }

        }

        protected async Task ProjectUserActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Project", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
