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
    public partial class KanbanModal : BaseComponent
    {
        private MyTask Model = new MyTask();
        public List<string> DefalutStatus = new List<string>() { "To do", "Progress", "Done" };
        private CS_MyTask ModelService;
        [Parameter] public List<User> TeamMembers { get; set; }
        [Parameter] public string ProjectId { get; set; }
        [Parameter] public string ProjectName { get; set; }
        [Parameter] public EventCallback<string> OnTaskUpdate { get; set; }
        public string arg { get; set; }
        IJSObjectReference module = null;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationKanbanHubConnection;

        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        }

        protected override async Task OnInitializedAsync()
        {
            Members = TeamMembers;
            //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
            notificationKanbanHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
            await notificationKanbanHubConnection.StartAsync();
        }
        public bool IsNotKanConnected => notificationKanbanHubConnection.State == HubConnectionState.Connected;
        Task SendKanbanNotification() => notificationKanbanHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Kanban Notification");

        private async Task SubmitModel()
        {
            try
            {
                ModelService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                Model.Name = UXUtility.LoggedInUser.Name;
                Model.Status = DefalutStatus[0];
                Model.ProjectId = ProjectId;
                Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                Model.TaskAssignUserName = TeamMembers.Where(tm => tm.Core.Id == Model.TaskAssignUserId).FirstOrDefault().Name;
                HttpResponseMessage response = await ModelService.PostAsync(Model, "CreateTask");
                if (response.IsSuccessStatusCode)
                {
                    if (Model.TaskAssignUserId != null && Model.TaskAssignUserId != "" && Model.TaskAssignUserId != UXUtility.LoggedInUser.Id)
                    {

                        //Notification NotificationModel = new Notification();
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = Model.TaskAssignUserId;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Task";
                        //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has assigned you a new task";
                        //NotificationModel.Url = "project/kanban/" + Model.ProjectId;
                        //INotificationService notificationService = new NotificationService(UXUtility.Engines.MarketNetwork);
                        //await NotificationClientService.PostAsync(NotificationModel);
                        //await notificationService.SendAsync(NotificationModel);
                        if (IsNotKanConnected) await SendKanbanNotification();
                    }
                    await OnTaskUpdate.InvokeAsync(arg);
                    await module.InvokeAsync<object>("HideModal", "kanbanModal");

                    //ToastService.ShowSuccess("Task Created", "Success");
                    Model.Clear();
                }

                else
                {
                    SLog.Write("Task Creation  Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Task Failed Exception");
            }
        }

        public List<string> PriorityList = new List<string>() { "Highest", "High", "Medium", "Low", "Lowest" };
        public void DisposeNotification()
        {
            _ = notificationKanbanHubConnection.DisposeAsync();
        }
    }
}
