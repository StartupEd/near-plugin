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
    public partial class CreateProject : BaseComponent
    {
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        private string SearchString { get; set; }
        public Project Model = new Project();
        public Project DeleteModel = new Project();
        public CS_Project ClientServices { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public List<Project> Projects = new List<Project>();
        public enum LoadingContainerState { Loading, Loaded, Error };
        public LoadingContainerState State;
        public string DeleteProjectId { get; set; }

        public async void ShowSweetAlertModal(string Id)
        {
            if (Id != null)
            {
                DeleteProjectId = Id;
            }
            ShowSweetAlertBox.ShowSweetAlert();
        }
        protected void DeleteSimulationAsync(bool Value)
        {
            if (Value)
            {
                DeleteProject(DeleteProjectId);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ClientServices = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                Projects = await ClientServices.GetProjectsFromTeamMemberId(UXUtility.LoggedInUser.Id);
                State = LoadingContainerState.Loaded;
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadData(string arg)
        {
            Projects = await ClientServices.GetProjectsFromTeamMemberId(UXUtility.LoggedInUser.Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected async Task DeleteProject(string Id)
        {

            try
            {
                var response = ClientServices.DeleteAsync(Id);

                Project project = Projects.Where(pr => pr.Id == Id).FirstOrDefault();
                await ProjectUserActivity(Id, project.Name, "You have deleted project " + project.Name);
                Projects.Remove(project);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }

        }

        protected bool IsVisible(Project Projects)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Projects.Name.ToUpper().StartsWith(SearchString) || Projects.Name.ToLower().StartsWith(SearchString))
                return true;



            return false;
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


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Project","project/create" }
    };
    }
}
