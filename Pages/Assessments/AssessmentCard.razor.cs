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
    public partial class AssessmentCard : BaseComponent
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        [Parameter]
        public Assessment Model { get; set; }
        public CS_Assessment ModelService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public bool ButtonClick { get; set; } = true;
        public DateTime LocalDate = DateTime.UtcNow;
        public bool IsUserRegistered { get; set; } = false;
        public bool ShowAlertBox { get; set; } = false;

        public async void ShowSweetAlert1()
        {
            ShowAlertBox = true;
        }
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationAssessmentCardHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ModelService = new CS_Assessment(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationAssessmentCardHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                if (Model.AssessmentRegistration != null)
                {
                    if (!Model.AssessmentRegistration.Exists(x => x.UserId == UXUtility.LoggedInUser.Id))
                    {
                        ButtonClick = false;
                    }
                    else
                    {
                        IsUserRegistered = true;
                    }
                }
                else
                {
                    ButtonClick = false;
                }
                await notificationAssessmentCardHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        public bool IsAsseNotiConnected => notificationAssessmentCardHubConnection.State == HubConnectionState.Connected;
        Task SendAssessmentCardNotification() => notificationAssessmentCardHubConnection.SendAsync("SendNotification", Model.AuthorId, "Assessment-Registered");

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

                    ButtonClick = true;
                    IsUserRegistered = true;
                    StateHasChanged();
                    //ToastService.ShowSuccess("You have registered successfully");
                    //navigationManager.NavigateTo("/assessments");
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.AuthorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Assessment";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has registered in Assessment " + Model.Name;
                    //NotificationModel.Url = "assessment/" + Model.Id + "/" + Model.Name + "/insight";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsAsseNotiConnected) await SendAssessmentCardNotification();
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
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
