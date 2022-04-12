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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class HostIncubationProgram : BaseComponent
    {
        public IncubationProgram Model = new IncubationProgram();
        public List<User> ConnectedUsers = new List<User>();
        public CS_User UserService { get; set; }
        public CS_IncubationProgram ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        //public CS_Uploader UploaderService { get; set; }
        public string StageContainer { get; set; } = "d-none";
        public string StageCard { get; set; } = "d-none";
        public string StageTitle { get; set; }
        public string StageDescription { get; set; }
        public Stage Stage { get; set; }
        public bool Validation = true;
        public DateRange Dates = new DateRange();
        public List<Select> IncubatorNames { get; set; } = new List<Select>();
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationIncubationwHubConnection;
        public string IncProgrmaId { get; set; }
        IJSObjectReference module = null;
        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/wizards/program_wizard.js");
            await module.InvokeVoidAsync("Wizard", "incubationProgramWizard", "incubationProgramForm");
        }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                //ClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //UploaderService = new FileUploader_ClientService<IncubationProgram>(ClientExchange, EngineServices.MarketNetwork);
                //UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //ConnectedUsers = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notificationIncubationwHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                //await notificationIncubationwHubConnection.StartAsync();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }

        }
        public bool IsIncNotiConnected => notificationIncubationwHubConnection.State == HubConnectionState.Connected;
        Task SendIncNotification() => notificationIncubationwHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Incubation program");
        public async void Imagebase64(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return;
            }
            Model.Core.ImageUrl = await UploaderService.UploadFile(src,GetType());
            StateHasChanged();
        }



        public async void SubmitModel()
        {
            try
            {
                Model.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "programDescription");
                var Object = await JSRuntime.InvokeAsync<object>("getProgramDateValues", "Programs");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartProgramDate == "" || Dates.EndProgramDate == null || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartProgramDate == null || Dates.EndProgramDate == null || Model.Core.Description == "<p><br></p>" || Model.Name == null || Model.ContactInfo.Email == null || Model.ContactInfo.PhoneNo == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Model.Deadlines.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Model.Deadlines.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Model.Deadlines.ProgramStart = Convert.ToDateTime(Dates.StartProgramDate);
                Model.Deadlines.ProgramEnd = Convert.ToDateTime(Dates.EndProgramDate);
                if (!UXUtility.IsEmailValid(Model.ContactInfo.Email))
                {
                //    ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                    return;
                }
                if (!UXUtility.IsNumberOrDigit(Model.ContactInfo.PhoneNo))
                {
                    //ToastService.ShowWarning("Please enter valid phone number.", "Only Numbers Allowed");
                    return;
                }

                var x = await JSRuntime.InvokeAsync<Object>("getName", "incubatorName");
                IncubatorNames = JsonConvert.DeserializeObject<List<Select>>(x.ToString());
                if (IncubatorNames != null)
                {
                    foreach (var Name in IncubatorNames)
                    {
                        Model.Incubators.Add(Name.Id, Name.Text);
                    }
                }

                if (Model.Incubators == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                else
                {
                    Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    HttpResponseMessage response = await ClientService.PostAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                        IncProgrmaId = response.Content.ReadAsStringAsync().Result;
                        //await SendIncubationProgramNotification();
                        //await IncubationProgramRecentActivity(IncProgrmaId, Model.Name, "You have recently created new Incubation Program " + Model.Name);
                        //ToastService.ShowSuccess("Program created successfully!");
                        //navigationManager.NavigateTo("/incubationprogram");
                    }
                    else
                    {
                        SLog.Write("Program Creation  Failed");
                        //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                    }

                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);

            }
        }
        private async Task SendIncubationProgramNotification()
        {
            foreach (var u in ConnectedUsers)
            {
                //Notification NotificationModel = new Notification();
                //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                //NotificationModel.ReceiverId = u.Id;
                //NotificationModel.SentAt = DateTime.UtcNow;
                //NotificationModel.NotificationType = "Incubation program";
                //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has created new Incubation program " + Model.Name;
                //NotificationModel.Url = "incubationprogram/" + IncProgrmaId + "/" + Model.Name;
                //await NotificationClientService.PostAsync(NotificationModel);
                if (IsIncNotiConnected) await SendIncNotification();
            }
            foreach (var Name in IncubatorNames)
            {
                //Notification NotificationModel = new Notification();
                //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                //NotificationModel.ReceiverId = Name.Id;
                //NotificationModel.SentAt = DateTime.UtcNow;
                //NotificationModel.NotificationType = "Incubation program";
                //NotificationModel.Message = "You are added as Incubators in Incubation program " + Model.Name;
                //NotificationModel.Url = "incubationprogram/" + IncProgrmaId + "/" + Model.Name;
                //await NotificationClientService.PostAsync(NotificationModel);
                if (IsIncNotiConnected) await SendIncNotification();
            }
        }

        public void ShowStageForm()
        {
            StageContainer = "d-block";
            Stage = new Stage();
            Stage.ClearStageValue();
            StateHasChanged();
        }

        public void CancelStage()
        {
            StageContainer = "d-none";
            StageCard = "d-none";
            Stage.ClearStageValue();
        }

        public void UpdateStage(Stage data)
        {
            StageContainer = "d-block";
            Stage = data;
        }

        public async void SaveStage()
        {
            var Object = await JSRuntime.InvokeAsync<object>("getProgramDateValues", "Stages");
            Dates = Newtonsoft.Json.JsonConvert.DeserializeObject<DateRange>(Object.ToString());
            if (Dates.StartStageDate == "" || Dates.EndStageDate == "" || Dates.StartStageDate == null || Dates.EndStageDate == null)
            {
                //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                return;
            }
            Stage.StartDate = Convert.ToDateTime(Dates.StartStageDate);
            Stage.EndDate = Convert.ToDateTime(Dates.EndStageDate);

            int index = Model.Stages.FindIndex(item => item.Name == Stage.Name); ;
            if (index != -1)
            {
                Model.Stages[index] = Stage;
            }
            else
            {
                Model.Stages.Add(Stage);
            }
            StageCard = "d-block";
            StageContainer = "d-none";
            StateHasChanged();
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


        public string InputElementStyle { get; set; } = "form-control form-control-lg form-control-solid";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label font-weight-bold";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
       { "Incubation Programs","/incubationprogram" }
    };

    }
}
