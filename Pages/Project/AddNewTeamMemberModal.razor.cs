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
    public partial class AddNewTeamMemberModal : BaseComponent
    {
        [Parameter] public Project Model { get; set; }
        [Parameter] public EventCallback<string> LoadData { get; set; }
        public List<string> MemberStatus = new List<string>() { "Admin", "Member" };
        public string Permission { get; set; }
        public string EmailId { get; set; }
        public CS_Project ClientServices { get; set; }
        public List<User> ConnectedUsers = new List<User>();
        public CS_User UserService { get; set; }
        public string UserId { get; set; }
        public string arg { get; set; }
        //private Notification_ClientService NotificationClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        private HubConnection notificationAddPmHubConnection;

        IJSObjectReference module = null;
        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ClientServices = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ConnectedUsers = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                /*otificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);*/
                notificationAddPmHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationAddPmHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
        public bool IsNotiAddPmConnected => notificationAddPmHubConnection.State == HubConnectionState.Connected;
        Task SendAddPmNotification() => notificationAddPmHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Kanban Notification");
        public async Task SubmitModel()
        {

            await module.InvokeAsync<object>("HideModal", "newMemberModal");
            try
            {
                //User data = await ClientServices.GetUserFromServer(EmailId);
                if (Model.TeamId.Admins.Contains(UserId) || Model.TeamId.Members.Contains(UserId))
                {

                    //ToastService.ShowError("Email does not Exits or Its is Already Member of Project", "Failed");
                }
                else
                {
                    if (Permission == "Admin")
                    {
                        Model.TeamId.Admins.Add(UserId);
                        Model.TeamId.Members.Add(UserId);

                    }
                    else
                    {
                        Model.TeamId.Members.Add(UserId);
                    }
                    HttpResponseMessage response = await ClientServices.PutAsync(Model, $"AddTeamMember/{UserId}");
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadData.InvokeAsync(arg);
                        //ToastService.ShowSuccess("Adding New Member", "Success");
                        if (Model.TeamId.Members != null && Model.TeamId.Admins != null)
                        {
                            //    Notification NotificationModel = new Notification();
                            //    NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                            //    NotificationModel.ReceiverId = UserId;
                            //    NotificationModel.SentAt = DateTime.UtcNow;
                            //    NotificationModel.NotificationType = "Task";
                            //    if (Permission == "Admin")
                            //    {
                            //        await ProjectUserActivity(Model.Core.Id, Model.Core.Name, "You have added " + ConnectedUsers.Find(m => m.Core.Id == UserId).Name + " as Admin in project" + Model.Core.Name);
                            //        NotificationModel.Message = UXUtility.LoggedInUser.Name + " has added you as an admin in project " + Model.Name;
                            //    }
                            //    else
                            //    {
                            //        await ProjectUserActivity(Model.Core.Id, Model.Core.Name, "You have added " + ConnectedUsers.Find(m => m.Core.Id == UserId).Name + " as Member in project" + Model.Core.Name);
                            //        NotificationModel.Message = UXUtility.LoggedInUser.Name + " has added you as a member in project " + Model.Name;
                            //    }
                            //    NotificationModel.Url = "project/" + Model.Id + "/" + Model.Name;
                            //    await NotificationClientService.PostAsync(NotificationModel);
                            //    if (IsNotiAddPmConnected) await SendAddPmNotification();
                            //}
                        }
                        else
                        {
                            SLog.Write("Add Team Member  Failed");
                            //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                        }

                    }
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
