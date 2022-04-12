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
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class IncubationCard : BaseComponent
    {
        [Parameter] public IncubationProgram Model { get; set; }
        [Parameter]
        public EventCallback<string> GetId { get; set; }
        public CS_IncubationProgram ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public Registration RegisteredUser = new Registration();
        public string data { get; set; }
        public bool ButtonClick { get; set; } = false;
        public DateTime LocalDate = DateTime.UtcNow;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationIncApplyHubConnection;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //ClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                //RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                //bool Registered = await RegistrationService.CheckUserRegisterationInIncubationProgram(Model.Core.Id);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notificationIncApplyHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationIncApplyHubConnection.StartAsync();
                //if (Registered)
                //{
                //    ButtonClick = true;
                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "");
            }
        }
        public bool IsIncApplyNotiConnected => notificationIncApplyHubConnection.State == HubConnectionState.Connected;
        Task SendIncApplyNotification() => notificationIncApplyHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Incubation program registration");
        public async void ApplyButtonClick()
        {
            try
            {
                RegisteredUser.EmailId = UXUtility.LoggedInUser.Email;
                RegisteredUser.Name = UXUtility.LoggedInUser.Name;
                RegisteredUser.ApplyDate = DateTime.UtcNow;
                //RegisteredUser.IncubationProgramId = Model.Core.Id;
                HttpResponseMessage response = await RegistrationService.PostAsync(RegisteredUser);
                if (response.IsSuccessStatusCode)
                {
                    await IncubationProgramRecentActivity(Model.Id, Model.Name, "You have recently applied for Incubation Program " + Model.Name);
                    ButtonClick = true;
                    StateHasChanged();
                    //ToastService.ShowSuccess("User Registered Successfully");
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.Core.CreatorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Incubation program";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has registered in Incubation program " + Model.Name;
                    //NotificationModel.Url = $"incubationprogram/{Model.Id}/{Model.Name.ToLower()}/insight";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    if (IsIncApplyNotiConnected) await SendIncApplyNotification();
                }
                else
                {
                    SLog.Write("User Registration  Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }
        protected void CallShowSweetAlertDeleteProgram(string Id)
        {
            GetId.InvokeAsync(Id);
        }

        protected async Task IncubationProgramRecentActivity(string Id, string Title, string description)
        {
            UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Incubation Program", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
