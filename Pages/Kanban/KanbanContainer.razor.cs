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
    public partial class KanbanContainer : BaseComponent
    {
         [Parameter] public List<MyTask> TaskList { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback<MyTask> OnStatusUpdated { get; set; }
    [Parameter] public CS_MyTask ModelService { get; set; }
    [Parameter] public Project PModel { get; set; }
    [Parameter] public List<User> TeamMembers { get; set; }
    [Parameter] public EventCallback<string> OnTaskListUpdate { get; set; }
    public MyTask Payload { get; set; }

    //private Notification_ClientService NotificationClientService { get; set; }
    private HubConnection notificationtaskUpHubConnection;
    protected override async Task OnInitializedAsync()
    {
        //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
        notificationtaskUpHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
        await notificationtaskUpHubConnection.StartAsync();
    }
    public bool IsNottaskUpConnected => notificationtaskUpHubConnection.State == HubConnectionState.Connected;
    Task SendtaskUpNotification() => notificationtaskUpHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Kanban Notification");
    public async Task UpdateTask(string newStatus)
    {
        var task = TaskList.SingleOrDefault(x => x.Id == Payload.Id);
        var OldStatus = task.Status;
        if (task != null)
        {
            task.Status = newStatus;
            HttpResponseMessage response = await ModelService.PutAsync(task, "UpdateTaskStatus");
            await OnStatusUpdated.InvokeAsync(Payload);
            if (OldStatus != newStatus)
            {
                //Notification NotificationModel = new Notification();
                //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                //if (task.Core.CreatorId == UXUtility.LoggedInUser.Id)
                //{
                //    NotificationModel.ReceiverId = task.TaskAssignUserId;
                //}
                //else
                //{
                //    NotificationModel.ReceiverId = task.Core.CreatorId;
                //}
                //NotificationModel.SentAt = DateTime.UtcNow;
                //NotificationModel.NotificationType = "Task";
                //NotificationModel.Message = UXUtility.LoggedInUser.Name + " changed task from " + OldStatus + " to " + newStatus;
                //NotificationModel.Url = "project/kanban/" + task.ProjectId;
                //await NotificationClientService.PostAsync(NotificationModel);
                //if (IsNottaskUpConnected) await SendtaskUpNotification();
            }
        }
    }
    }
}
