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
    public partial class IncubationProgramOLP : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string ProgramName { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        public IncubationProgram Model = new IncubationProgram();
        public List<Model_Event> EventModels { get; set; } = new List<Model_Event>();
        public List<Registration> RegistrationModels { get; set; } = new List<Registration>();
        public CS_IncubationProgram ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public CS_Event EventService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public Registration RegisteredUser = new Registration();
        public string StageStatus { get; set; }
        public string ProgramStatus { get; set; }
        //public DateTime LocalDate = Utility.ConvertUTCDateTimeToLocal(DateTime.UtcNow);
        public bool ButtonClick = false;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationIncAppliedHubConnection;

        protected void CallDeleteBPlanAlert()
        {
            ConfirmSweetAlert.ShowSweetAlert();
        }
        protected void DeleteIncubationProgAsync(bool Value)
        {
            if (Value)
            {
                DeleteProgram(Value);
            }
        }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProgramName = ProgramName.Replace(" ", "-");
                ClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetIncubationProgramById(Id);
                //EventModels = await EventService.GetEventsByIncubationProgramId(Model.Id);
                await LoadData();
                bool Registered = await RegistrationService.CheckUserRegisterationInIncubationProgram(Id);
                if (Registered)
                {
                    ButtonClick = true;
                }
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationIncAppliedHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationIncAppliedHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task LoadData()
        {
            RegistrationModels = await RegistrationService.GetRegisteredUsersForIncubationProgram(Id);
            StateHasChanged();
        }
        public bool IsIncAppliedNotiConnected => notificationIncAppliedHubConnection.State == HubConnectionState.Connected;
        Task SendIncAppliedNotification() => notificationIncAppliedHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Incubation program registration");

        public async void ApplyButtonClick()
        {
            try
            {
                RegisteredUser.EmailId = UXUtility.LoggedInUser.Email;
                RegisteredUser.Name = UXUtility.LoggedInUser.Name;
                RegisteredUser.ApplyDate = DateTime.UtcNow;
                // RegisteredUser.IncubationProgramId = Model.Core.Id;
                HttpResponseMessage response = await RegistrationService.PostAsync(RegisteredUser);
                if (response.IsSuccessStatusCode)
                {
                    await IncubationProgramRecentActivity(Model.Id, Model.Name, "You have recently applied for Incubation Program " + Model.Name);

                    ButtonClick = true;
                    await LoadData();
                    //ToastService.ShowSuccess("User Registered Successfully");
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.Core.CreatorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Incubation program";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has registered in Incubation program " + Model.Name;
                    //NotificationModel.Url = "incubation-program/insight/" + Model.Id;
                    //await NotificationClientService.PostAsync(NotificationModel);
                    if (IsIncAppliedNotiConnected) await SendIncAppliedNotification();
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


        public async void DeleteProgram(bool Value)
        {
            try
            {
                if (Value)
                {
                    HttpResponseMessage message = await ClientService.DeleteAsync(Model.Id);
                    if (message.IsSuccessStatusCode)
                    {
                        await IncubationProgramRecentActivity(Model.Id, Model.Name, "You have recently deleted Incubation Program " + Model.Name);
                        //ToastService.ShowSuccess("Deleted Successfuly");
                        //navigationManager.NavigateTo("/incubation-programs");
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task IncubationProgramRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Incubation Program", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
