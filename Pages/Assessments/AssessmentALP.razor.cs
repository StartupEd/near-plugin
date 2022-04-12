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
    public partial class AssessmentALP : BaseComponent
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        public List<Assessment> AllAssessment = new List<Assessment>();
        public CS_Assessment ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private string SearchString { get; set; }
        public string ProgramView { get; set; } = "Flix";

        public void ToggleView(string view)
        {

            ProgramView = view;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ModelService = new CS_Assessment(ClientExchange, EngineServices.MarketNetwork);
                await LoadData();

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadData()
        {
            AllAssessment = await ModelService.GetAllAssessment();
            AllAssessment = AllAssessment.OrderByDescending(a => a.AssessmentDates.AssessmentEnd.Date).ToList();
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected bool IsVisible(Model_Assessment Assessment)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Assessment.Name.ToUpper().StartsWith(SearchString) || Assessment.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Assessments","/assessments" }
    };
    }
}
