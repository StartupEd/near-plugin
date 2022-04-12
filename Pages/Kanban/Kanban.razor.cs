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
    public partial class Kanban : BaseComponent
    {
        private MyTask Model = new MyTask();
        private Project PModel = new Project();
        private List<MyTask> AllTask = new List<MyTask>();
        public List<User> TeamMembers = new List<User>();
        public CS_MyTask ModelService { get; set; }
        public CS_Project ClientService { get; set; }
        public CS_Profile ProfileService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        string LastUpdatedTask = "";
        int y = 0;
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public List<Profile> Profiles = new List<Profile>();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                PModel = await ClientService.GetProjectsFromId(Id);
                AllTask = await ModelService.GetTaskFromServer(Id);
                TeamMembers = await ClientService.GetTeamMemberInfo(PModel.Core.Id);
                Profiles = await ProfileService.GetProfileListofUser(TeamMembers);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Task Failed Exception");
            }
        }

        void HandleStatusUpdated(Model_MyTask updatedJob)
        {
            LastUpdatedTask = updatedJob.Core.Name;

        }

        public async Task UpdateTaskList()
        {

        }

        protected async Task LoadData(string arg)
        {
            AllTask = await ModelService.GetTaskFromServer(Id);
            PModel = await ClientService.GetProjectsFromId(Id);
            StateHasChanged();
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Project","project/create" },
            {"Task", $"/kanban/" }
    };
    }
}
