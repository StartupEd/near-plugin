using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Shared_Classes;
using System.Collections.Generic;
using StartupEd.Lib.Engine;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace StartupEd.UX.Incubation
{
    public partial class HostEvent : BaseComponent
    {
        public Event Model = new Event();
        public CS_Event ClientService { get; set; }
        public CS_FileUploader UploaderService { get; set; }
        public string RoundContainer { get; set; } = "d-none";
        public string RoundCard { get; set; } = "d-none";
        public string RoundTitle { get; set; }
        public string RoundDescription { get; set; }
        public Round Round { get; set; }
        public bool Validation = true;

        public List<IncubationProgram> Programs { get; set; } = new List<IncubationProgram>();
        public CS_IncubationProgram ProgramService { get; set; }
        //private Notification_ClientService NotificationClientService { get; set; }
        //private HubConnection notificationEventsHubConnection;
        private CS_User UserService { get; set; }
        public List<User> UserConnections = new List<User>();

        IJSObjectReference module;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/wizards/event_wizard.js");
                    await module.InvokeVoidAsync("Wizard", "event", "eventForm");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                UploaderService = new CS_FileUploader(ClientExchange, EngineServices.MarketNetwork);
                ProgramService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                Programs = await ProgramService.GetIncubationProgramByUserId(UXUtility.LoggedInUser.Id);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //notificationEventsHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                //await notificationEventsHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
        //public bool IsEvtNotiConnected => notificationEventsHubConnection.State == HubConnectionState.Connected;
        //Task SendEventNotification() => notificationEventsHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Events");
        public async void Imagebase64(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return;
            }
            Model.Core.ImageUrl = await UploaderService.UploadFile(src, Model.GetType());
            StateHasChanged();
        }

        public DateRange Dates = new DateRange();

        public async void SubmitModel()
        {
            try
            {
                bool Name = await ClientService.CheckName(Model.Name);
                Model.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "eventDetail");
                var Object = await JSRuntime.InvokeAsync<object>("getDateValues", "Events");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartEventDate == "" || Dates.EndEventDate == "" || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartEventDate == null || Dates.EndEventDate == null || Model.Core.Description == "<p><br></p>" || Model.Name == null || Model.ContactInfo.Email == null || Model.ContactInfo.Name == null || Model.ContactInfo.PhoneNo == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Model.Deadlines.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Model.Deadlines.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Model.Deadlines.EventStart = Convert.ToDateTime(Dates.StartEventDate);
                Model.Deadlines.EventEnd = Convert.ToDateTime(Dates.EndEventDate);

                if (!UXUtility.IsEmailValid(Model.ContactInfo.Email))
                {
                    //ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                    return;
                }
                if (!UXUtility.IsNumberOrDigit(Model.ContactInfo.PhoneNo))
                {
                    //ToastService.ShowWarning("Please enter valid phone number.", "Alphabet Not Allowed");
                    return;
                }
                //if (Name)
                //{
                //    ToastService.ShowError("Events Title is Already taken");
                //}
                //else
                //{
                Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                UserConnections = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                HttpResponseMessage response = await ClientService.PostAsync(Model, "AddEvents");
                if (response.IsSuccessStatusCode)
                {

                    //foreach (var u in UserConnections)
                    //{
                    //    Notification NotificationModel = new Notification();
                    //    NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //    NotificationModel.ReceiverId = u.Id;
                    //    NotificationModel.SentAt = DateTime.UtcNow;
                    //    NotificationModel.NotificationType = "event";
                    //    NotificationModel.Message = UXUtility.LoggedInUser.Name + " has created new event " + Model.Name;
                    //    NotificationModel.Url = "events";
                    //    await NotificationClientService.PostAsync(NotificationModel);
                    //    if (IsEvtNotiConnected) await SendEventNotification();
                    //}
                    //ToastService.ShowSuccess("Event created successfully!", "");
                    //navigationManager.NavigateTo("/events");

                }
                else
                {
                    SLog.Write("Event Creation Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");

                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Event Failed Exception");
            }
        }


        public async void AddRound()
        {
            RoundContainer = "d-block";
            Round = new Round();
            //Round.ClearRoundValue();
            StateHasChanged();
        }

        public void CancelRound()
        {
            RoundContainer = "d-none";
            RoundCard = "d-none";
        }

        public async void SaveRound()
        {
            var Object = await JSRuntime.InvokeAsync
    <object>("getDateValues", "Rounds");
            Dates = Newtonsoft.Json.JsonConvert.DeserializeObject<DateRange>(Object.ToString());
            if (Dates.StartRoundDate == "" || Dates.EndRoundDate == "" || Dates.StartRoundDate == null || Dates.EndRoundDate == null)
            {
                //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                return;
            }
            Round.StartDate = Convert.ToDateTime(Dates.StartRoundDate);
            Round.EndDate = Convert.ToDateTime(Dates.EndRoundDate);
            int index = Model.Rounds.FindIndex(item => item.Id == Round.Id);
            if (index != -1)
            {
                Model.Rounds[index] = Round;
            }
            else
            {
                System.Guid guid = System.Guid.NewGuid();
                Round.Id = guid.ToString();
                Model.Rounds.Add(Round);
            }

            RoundCard = "d-block";
            RoundContainer = "d-none";
            StateHasChanged();
        }

        public void UpdateRound(Round data)
        {
            RoundContainer = "d-block";
            Round = data;
        }

        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label font-weight-bold";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-8";
        public string FormElementStyle { get; set; } = "row";


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
       { "Events","/events" }
    };


        public List<string> EventType = new List<string>() { "Competition", "Workshop", "Hackthon", "Webinars", "Festivals", "Scholarship", "Quizzes", "Led by Social Issue" };
        //public void DisposeNotification()
        //{
        //    _ = notificationEventsHubConnection.DisposeAsync();
        //}
    }
}
