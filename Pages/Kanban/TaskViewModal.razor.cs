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
    public partial class TaskViewModal : BaseComponent
    {
        [Parameter] public List<User> TeamMembers { get; set; }
        public Guid Guid = Guid.NewGuid();
        public string ModalDisplay = "none;";
        public string ModalClass = "";
        public bool ShowBackdrop = false;
        [Parameter] public MyTask Model { get; set; }
        [Parameter] public EventCallback<string> OnTaskUpdate { get; set; }
        public CS_MyTask ModelService { get; set; }
        public bool Editable { get; set; }
        public string arg { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        //private Notification_ClientService NotificationClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        private HubConnection notificationTaskHubConnection;
        public string OldTaskAssignUserId { get; set; }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Console.WriteLine(Model.TaskPriority);
                ModelService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationTaskHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();
                Members = TeamMembers;
                if (Model.TaskAssignUserId == UXUtility.LoggedInUser.Id || Model.Core.CreatorId == UXUtility.LoggedInUser.Id)
                {
                    Editable = false;
                }
                else
                {
                    Editable = true;
                }
                OldTaskAssignUserId = Model.TaskAssignUserId;
                await notificationTaskHubConnection.StartAsync();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }

        }
        public bool IsNotiTaskConnected => notificationTaskHubConnection.State == HubConnectionState.Connected;
        Task SendTaskNotification() => notificationTaskHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Kanban Notification");

        public void Open()
        {
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public void Close()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }

        public async Task TaskUpdate()
        {

            User user = Members.Where(us => us.Id == Model.TaskAssignUserId).FirstOrDefault();
            Model.TaskAssignUserName = user.Name;

            HttpResponseMessage response = await ModelService.PutAsync(Model, "UpdateTask");
            if (response.IsSuccessStatusCode)
            {
                if (Model.TaskAssignUserId != null && Model.TaskAssignUserId != "" && Model.TaskAssignUserId != UXUtility.LoggedInUser.Id)
                {
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.TaskAssignUserId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Task";
                    //if (OldTaskAssignUserId == Model.TaskAssignUserId)
                    //{
                    //    NotificationModel.Message = UXUtility.LoggedInUser.Name + " updated a task";
                    //}
                    //else
                    //{
                    //    NotificationModel.Message = UXUtility.LoggedInUser.Name + " has assigned you a new task";
                    //}

                    //NotificationModel.Url = "project/kanban/" + Model.ProjectId;
                    //await NotificationClientService.PostAsync(NotificationModel);
                    if (IsNotiTaskConnected) await SendTaskNotification();
                }
                OnTaskUpdate.InvokeAsync(arg);
                //ToastService.ShowSuccess("Task Updated", "Success");
                Close();

            }

            else
            {
                SLog.Write("Task Creation  Failed");
                //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
            }
        }

        public async Task UpdateData(MyTask Model)
        {
            HttpResponseMessage response = await ModelService.PutAsync(Model);
            if (response.IsSuccessStatusCode)
            {
                OnTaskUpdate.InvokeAsync(arg);
                //ToastService.ShowSuccess("Task Updated", "Success");
                Close();

            }

            else
            {
                SLog.Write("Task Creation  Failed");
                //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
            }
        }
        public async Task TaskDelete()
        {
            HttpResponseMessage response = await ModelService.DeleteAsync(Model.Id);
            if (response.IsSuccessStatusCode)
            {
                //UserActivity userActivity = new UserActivity();
                //Activity Module = new Activity();
                //Module.CreateActivity("Kanban Task", Model.Core.Id, Model.Core.Name, "You have deleted task titled " + Model.Core.Name);
                //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //userActivity.Activities.Add(Module);
                //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                //OnTaskUpdate.InvokeAsync(arg);
                //ToastService.ShowSuccess("Task Deleted", "Success");
                Close();

            }

            else
            {
                SLog.Write("Task Creation  Failed");
                //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
            }
        }
        public List<string> PriorityList = new List<string>() { "Highest", "High", "Medium", "Low", "Lowest" };
    }
}
