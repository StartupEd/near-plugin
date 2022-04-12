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
    public partial class AssessmentResultView :BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string UserId { get; set; }
        public Model_Assessment Model = new Model_Assessment();
        public CS_Assessment ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

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
                //ToastService.ShowError(ex.Message, "Assessment Exception");
            }
        }
        protected async Task LoadData()
        {
            Model = await ModelService.GetAssessmentById(Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }
    }
}
