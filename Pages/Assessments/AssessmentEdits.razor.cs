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
using StartupEd.UX.One.Shared_Classes;
using Newtonsoft.Json;
using System.Net.Http;

namespace StartupEd.UX.Incubation
{
    public partial class AssessmentEdits : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string name { get; set; }
        public Assessment Model = new Assessment();
        public Question Question { get; set; }
        public CS_Assessment ModelService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public DateTime LocalDate = DateTime.UtcNow;
        public string AssessmentStatus { get; set; }
        public List<Question> QuestionList = new List<Question>();
        public string RoundContainer { get; set; } = "d-none";
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ModelService = new CS_Assessment(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                await LoadData();

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
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        public void AddQuestion()
        {
            RoundContainer = "d-block";
            Question = new Question();
            Question.value = QuestionType.ShortAnswer;
            //Question.MultipleChoiceOption.Add("1", "Option 1");
            StateHasChanged();
        }
        public async void SaveQuestion()
        {

            int index = Model.Questions.FindIndex(item => item.Id == Question.Id);
            if (index != -1)
            {
                Model.Questions[index] = Question;
            }
            else
            {
                System.Guid guid = System.Guid.NewGuid();
                Question.Id = guid.ToString();
                Model.Questions.Add(Question);
            }
            RoundContainer = "d-none";

            StateHasChanged();
        }
        public void CancelQuestion()
        {
            RoundContainer = "d-none";
        }
        public bool Validation = true;
        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label font-weight-bold";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-8";
        public string FormElementStyle { get; set; } = "row";
        public DateRange Dates = new DateRange();
        public async void SubmitModel()
        {
            try
            {
                //var Object = await JSRuntime.InvokeAsync<object>("getAssessmentDateValues", "Assessment");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Model.Name == "" || Model.Name == null || Model.Core.Description == "" || Model.Core.Description == null || Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartAssessmentDate == "" || Dates.EndAssessmentDate == "" || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartAssessmentDate == null || Dates.EndAssessmentDate == null)
                {
                    ////ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Model.AssessmentDates.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Model.AssessmentDates.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Model.AssessmentDates.AssessmentStart = Convert.ToDateTime(Dates.StartAssessmentDate);
                Model.AssessmentDates.AssessmentEnd = Convert.ToDateTime(Dates.EndAssessmentDate);
                Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                HttpResponseMessage response = await ModelService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    await AssesmentRecentActivity(Model.Core.Id, Model.Name, "You have made changes in assessment " + Model.Name);
                    //ToastService.ShowSuccess("Assessment updated successfully!", "");
                    //navigationManager.NavigateTo("/assessments");
                }
                else
                {
                    SLog.Write("Assessment Creation  Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");

                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment creation Failed Exception");
            }
        }

        protected async Task AssesmentRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(new Model_Activity { Type = "Assessment", FeatureId = Id, Feature = Title, Description = description });
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
