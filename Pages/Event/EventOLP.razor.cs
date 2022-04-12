using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;
using System.Collections.Generic;


namespace StartupEd.UX.Incubation
{
    public partial class EventOLP : BaseComponent
    {
        public NavigationManager navigationManager;
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string EventName { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        private OneUX_SweetAlertBox ConfirmGoToProgram { get; set; }
        public Event Model = new Event();
        public IncubationProgram Incubation_Model = new IncubationProgram();
        public List<Registration> RModel = new List<Registration>();
        public CS_Event ModelService { get; set; }
        public CS_IncubationProgram Incubation_ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public string RoundStatus { get; set; }
        public string EventStatus { get; set; }
        public DateTime LocalDate = DateTime.UtcNow;
        public Registration RegistrationModel = new Registration();
        public Registration UserRegistered = new Registration();
        public bool ButtonClick { get; set; } = true;
        public bool IsIPRegistered { get; set; } = false;

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected void CallShowSweetAlertDeleteEvent()
        {
            ConfirmSweetAlert.ShowSweetAlert();
        }

        protected async void DeleteEventAsync(bool Value)
        {
            if (Value)
            {
                try
                {
                    HttpResponseMessage message = await ModelService.DeleteAsync(Model.Id);
                    if (message.IsSuccessStatusCode)
                    {
                        //ToastService.ShowSuccess("Event Deleted Successfuly");
                        navigationManager.NavigateTo("/events");

                    }
                }
                catch (Exception ex)
                {
                    //ToastService.ShowError(ex.Message, "Events Exception");
                }
            }
        }

        protected void ShowSweetAlert2()
        {
            ConfirmGoToProgram.ShowSweetAlert();
        }

        protected void GoToProgram()
        {
            navigationManager.NavigateTo($"incubation-program/detail/{Model.IncubationProgramId}");
        }


        //private CS_Notification NotificationClientService { get; set; }
        //private HubConnection notiEventsRegHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                EventName = EventName.Replace(" ", "-");
                State = LoadingContainerState.Loading;
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                Incubation_ModelService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                Model = await ModelService.GetEventById(Id);
                UserRegistered = await RegistrationService.CheckUserIsRegisteredOrNot(Id);
                IsIPRegistered = await RegistrationService.CheckUserRegisterationInIncubationProgram(Model.IncubationProgramId);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notiEventsRegHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await LoadData();
                if (Model.IncubationProgramId != null)
                {
                    Incubation_Model = await Incubation_ModelService.GetIncubationProgramById(Model.IncubationProgramId);
                }
                if (UserRegistered == null)
                {
                    ButtonClick = false;
                }
                //await notiEventsRegHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        //public bool IsEvtRegNotiConnected => notiEventsRegHubConnection.State == HubConnectionState.Connected;
        //Task SendEventRegNotification() => notiEventsRegHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Event-Registered");

        protected async Task LoadData()
        {
            RModel = await RegistrationService.GetRegisteredUserForEvents(Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        public async void ApplyButtonClick()
        {
            try
            {
                RegistrationModel.EmailId = UXUtility.LoggedInUser.Email;
                RegistrationModel.Name = UXUtility.LoggedInUser.Name;
                RegistrationModel.ApplyDate = DateTime.UtcNow;
                RegistrationModel.EventsId = Model.Core.Id;
                HttpResponseMessage response = await RegistrationService.PostAsync(RegistrationModel, "User");
                if (response.IsSuccessStatusCode)
                {
                    ButtonClick = true;
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.Core.CreatorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Event-Registration";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has registered in event " + Model.Name;
                    //NotificationModel.Url = "event/" + Model.Core.Id + "/" + Model.Name;
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsEvtRegNotiConnected) await SendEventRegNotification();
                    //await LoadData();
                    //ToastService.ShowSuccess("User Registered Successfully");

                }
                else
                {
                    SLog.Write("User Registration Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        //public void DisposeNotification()
        //{
        //    _ = notiEventsRegHubConnection.DisposeAsync();
        //}
    }
}
