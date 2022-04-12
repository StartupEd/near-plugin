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
using Microsoft.JSInterop;
using StartupEd.UX.One.Shared_Classes;
using Newtonsoft.Json;
using System.Net.Http;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class Assessments : BaseComponent
    {
        public Model_Assessment Model = new Model_Assessment();
        public Question Question { get; set; }
        public int length { get; set; }
        public List<int> item = new List<int>();
        public List<Question> QuestionList = new List<Question>();
        public CS_Assessment ClientService { get; set; }

        public string RoundContainer { get; set; } = "d-none";
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_Assessment(ClientExchange, EngineServices.MarketNetwork);

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
        public async void AddQuestion()
        {
            RoundContainer = "d-block";
            Question = new Question();
            Question.value = QuestionType.ShortAnswer;
            //await JSRuntime.InvokeVoidAsync("AssessmentBootstrapSelect", "TypeSelectId");
            StateHasChanged();
        }
        public async void SaveQuestion()
        {
            if (Question.Name == "" || Question.Name == null)
            {
                //ToastService.ShowWarning("Please Try Again :)", "Question name can not be blank.");
                return;
            }
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
            await JSRuntime.InvokeVoidAsync("removeAssessmentInputSelect", "TypeSelectId");
            StateHasChanged();
        }
        public void CancelQuestion()
        {
            RoundContainer = "d-none";
        }

        public bool Validation = true;
        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label font-weight-bold";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-9";
        public string FormElementStyle { get; set; } = "row";

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
            { "Assessments","/assessments" }
    };

        public DateRange Dates = new DateRange();
        public async void SubmitModel()
        {
            try
            {
                var Object = await JSRuntime.InvokeAsync<object>("getAssessmentDateValues", "Assessment");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Model.Name == "" || Model.Name == null || Model.Description == "" || Model.Description == null || Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartAssessmentDate == "" || Dates.EndAssessmentDate == "" || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartAssessmentDate == null || Dates.EndAssessmentDate == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Model.AssessmentDates.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Model.AssessmentDates.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Model.AssessmentDates.AssessmentStart = Convert.ToDateTime(Dates.StartAssessmentDate);
                Model.AssessmentDates.AssessmentEnd = Convert.ToDateTime(Dates.EndAssessmentDate);
                Model.AuthorId = UXUtility.LoggedInUser.Id;
                Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //HttpResponseMessage response = await ClientService.PostAsync(Model, "CreateAssessment");
                //if (response.IsSuccessStatusCode)
                //{
                //    ToastService.ShowSuccess("Assessment created successfully!", "");
                //    navigationManager.NavigateTo("/assessments");
                //}
                //else
                //{
                //    SLog.Write("Assessment Creation  Failed");
                //    ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");

                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Assessment creation Failed Exception");
            }
        }
    }
}
