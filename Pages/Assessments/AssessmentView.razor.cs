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
    public partial class AssessmentView : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        public Assessment Model = new Assessment();
        public CS_Assessment ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        //public AssessmentAnswerModel AnsM = new AssessmentAnswerModel();
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationAssessmentViewHubConnection;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ModelService = new CS_Assessment(ClientExchange, EngineServices.MarketNetwork);
                //await LoadData();
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notificationAssessmentViewHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationAssessmentViewHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        public bool IsAsseAnsNotiConnected => notificationAssessmentViewHubConnection.State == HubConnectionState.Connected;
        Task SendAssessmentAnsNotification() => notificationAssessmentViewHubConnection.SendAsync("SendNotification", Model.AuthorId, "Assessment answer submission");

        protected async Task LoadData()
        {
            Model = await ModelService.GetAssessmentById(Id);
            foreach (var Q in Model.Questions)
            {
                //AssessmentAnswer Ans = new AssessmentAnswer();
                AssessmentAnswerKey Key = new AssessmentAnswerKey();
                Key.UserId = UXUtility.LoggedInUser.Id;
                Key.QuestionId = Q.Id;
                Key.value = Q.value;
                Key.ShortAnswer = "";
                Key.IsAttempted = false;
                Key.MlcKey = "";
                Key.MultipleChoiceOption = Q.MultipleChoiceOption;
                Model.AssessmentAnswerKey.Add(Key);
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }
        public async void SubmitAnswerModel()
        {
            try
            {
                if (await ModelService.AssessmentAnswerSubmission(Model))
                {
                    //ToastService.ShowSuccess("You have submitted assessment successfully");
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Model.AuthorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Assessment";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has attempted the Assessment " + Model.Name;
                    //NotificationModel.Url = "assessment/" + Model.Id + "/" + Model.Name + "/insight";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsAsseAnsNotiConnected) await SendAssessmentAnsNotification();
                    //navigationManager.NavigateTo("/assessments");

                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        void InputChangeAsync(string ShortAnswer, string QuestionId)
        {
            for (int i = 0; i < Model.AssessmentAnswerKey.Count; i++)
            {
                if (Model.AssessmentAnswerKey[i].UserId == UXUtility.LoggedInUser.Id && Model.AssessmentAnswerKey[i].QuestionId == QuestionId)
                {
                    Model.AssessmentAnswerKey[i].ShortAnswer = ShortAnswer;
                    Model.AssessmentAnswerKey[i].IsAttempted = true;
                }
            }
        }
        void MlcOptionClicked(string QuestionId, string Key)
        {
            for (int i = 0; i < Model.AssessmentAnswerKey.Count; i++)
            {
                if (Model.AssessmentAnswerKey[i].UserId == UXUtility.LoggedInUser.Id && Model.AssessmentAnswerKey[i].QuestionId == QuestionId)
                {
                    Model.AssessmentAnswerKey[i].MlcKey = Key;
                    Model.AssessmentAnswerKey[i].IsAttempted = true;
                }
            }
        }
    }
}
