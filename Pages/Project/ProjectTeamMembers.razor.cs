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
    public partial class ProjectTeamMembers : BaseComponent
    {
        [Parameter] public string ProjectId { get; set; }
        [Parameter] public string ProjectName { get; set; }
        public CS_Project ClientService { get; set; }
        private Project Model = new Project();
        private List<User> TeamMembers = new List<User>();
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        //IJSObjectReference module = null;
        //protected override async void OnInitialized()
        //{
        //    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        //}

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetProjectsFromId(ProjectId);
                TeamMembers = await ClientService.GetTeamMemberInfo(Model.Core.Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Task Failed Exception");
            }
        }

        protected async Task LoadData(string args)
        {
            Model = await ClientService.GetProjectsFromId(ProjectId);
            TeamMembers = await ClientService.GetTeamMemberInfo(Model.Core.Id);
            //await module.InvokeAsync<object>("HideModal", "kanbanModal");
        }

        public async Task ExitTeam(string Id, User user)
        {
            Model.TeamId.Admins.Remove(Id);
            Model.TeamId.Members.Remove(Id);
            TeamMembers.Remove(user);
            UpdateProject();

            await ProjectUserActivity(Model.Core.Id, Model.Core.Name, "You have removed team member" + TeamMembers.Find(m => m.Core.Id == Id).Name + " from project" + Model.Core.Name);

            if (UXUtility.LoggedInUser.Id == Id)
            {
                //navigationManager.NavigateTo("/project/create");
            }
            StateHasChanged();

        }
        public async Task MakeTeamAdmin(string Id)
        {
            Model.TeamId.Admins.Add(Id);
            await ProjectUserActivity(Model.Core.Id, Model.Core.Name, "You have added " + TeamMembers.Find(m => m.Core.Id == Id).Name + " as Admin in project" + Model.Core.Name);
            UpdateProject();
            StateHasChanged();
        }
        public async Task DismissTeamAdmin(string Id)
        {
            Model.TeamId.Admins.Remove(Id);
            await ProjectUserActivity(Model.Core.Id, Model.Core.Name, "You have removed " + TeamMembers.Find(m => m.Core.Id == Id).Name + " from Admin in project" + Model.Core.Name);
            UpdateProject();
            StateHasChanged();
        }

        public async Task UpdateProject()
        {
            try
            {
                HttpResponseMessage response = await ClientService.PutAsync(Model);

                if (response.IsSuccessStatusCode)
                {
                    //ToastService.ShowSuccess("Project Updated!");
                }
                else
                {
                    //ToastService.ShowError("Updation Failed!");
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


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Project","project/create" },
            {"Task","/kanban" },
        {"Team Members","/project/boardsettings" }
    };
    }
}
