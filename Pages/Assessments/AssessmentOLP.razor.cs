using Microsoft.AspNetCore.Components;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StartupEd.Lib.Engine;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using StartupEd.Engine.MarketNetwork;


namespace StartupEd.UX.Incubation
{
    public partial class AssessmentOLP : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string name { get; set; }
        public Assessment Model = new Assessment();
        public CS_Assessment ModelService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public bool ButtonClick { get; set; } = true;
        public bool IsIPRegistered { get; set; } = false;
        public DateTime LocalDate = DateTime.UtcNow;
        public string AssessmentStatus { get; set; }
        public bool ShowAlertBox { get; set; } = false;
        private OneUX_SweetAlertBox SweetAlertConfirmation { get; set; }
        protected void ShowSweetAlertModal()
        {
            SweetAlertConfirmation.ShowSweetAlert();
        }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationAssessmentOlpHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //State = LoadingContainerState.Loading;
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //ModelService = new Assessment_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notificationAssessmentOlpHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                //await LoadData();
                //await notificationAssessmentOlpHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        protected async Task LoadData()
        {
            Model = await ModelService.GetAssessmentById(Id);
            if (Model.AssessmentRegistration != null)
            {
                if (!Model.AssessmentRegistration.Exists(x => x.UserId == UXUtility.LoggedInUser.Id))
                {
                    ButtonClick = false;
                    SLog.Write(Model.AssessmentDates.RegistrationStart.ToLocalTime().Date.ToString());
                    SLog.Write(Model.AssessmentDates.RegistrationEnd.ToLocalTime().Date.ToString());
                    SLog.Write(LocalDate.Date.ToString());
                }
            }
            else
            {
                ButtonClick = false;
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }
        public bool IsAsseOlpNotiConnected => notificationAssessmentOlpHubConnection.State == HubConnectionState.Connected;
        Task SendAssessmentOlpNotification() => notificationAssessmentOlpHubConnection.SendAsync("SendNotification", Model.AuthorId, "Assessment-Registered");
        public async void ApplyButtonClick()
        {
            try
            {
                AssessmentRegistration Reg = new AssessmentRegistration();
                Reg.UserId = UXUtility.LoggedInUser.Id;
                Reg.UserName = UXUtility.LoggedInUser.Name;
                Reg.EmailId = UXUtility.LoggedInUser.Email;
                Reg.ApplyDate = DateTime.UtcNow;
                Model.AssessmentRegistration.Add(Reg);
                if (await ModelService.AssessmentRegistration(Model))
                {
                    await AssesmentRecentActivity(Model.Core.Id, Model.Core.Name, "You have applied for assessment " + Model.Core.Name);

                    //ToastService.ShowSuccess("You have registered successfully");
                    //navigationManager.NavigateTo("/assessments");
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.AuthorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Assessment";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has registered in Assessment " + Model.Name;
                    //NotificationModel.Url = "assessment/insight/" + Model.Id + "/" + Model.Name + "/insight";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsAsseOlpNotiConnected) await SendAssessmentOlpNotification();
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        private async Task DeleteAssessment()
        {
            bool result = false;
            result = await ModelService.RemoveAssessment(Model.Id);
            if (result == true)
            {
                await AssesmentRecentActivity(Model.Core.Id, Model.Core.Name, "You have deleted your assessment " + Model.Core.Name);
                //ToastService.ShowInfo("Assessment has been deleted!");
                //navigationManager.NavigateTo("/assessments");
            }
            else
            {
                throw new Exception("Exception: Assessment Not Deleted..");
            }
        }

        protected async Task AssesmentRecentActivity(string Id, string Title, string description)
        {
            UserActivity userActivity = new UserActivity();
            userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            userActivity.Activities.Add(new Model_Activity { FeatureId = Id, Feature = Title, Description = description });
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
