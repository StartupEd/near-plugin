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
    public partial class EventInsight : BaseComponent
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string EventName { get; set; }
        public Event Model  = new Event();
        public List<BusinessModel> BmcModels { get; set; }
        public List<BusinessPlan> BpModels { get; set; }
        public List<Registration> RegisteredUser { get; set; }
        public CS_Event ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        //public Apps data { get; set; }
        public int y = 1;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                Model = await ModelService.GetEventById(Id);
                RegisteredUser = await RegistrationService.GetRegisteredUserForEvents(Id);
                BmcModels = await ModelService.GetBusinessModelsForEvents(RegisteredUser);
                BpModels = await ModelService.GetBusinessPlansForEvents(RegisteredUser);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        public async void DownloadCsv()
        {
            //await ModelService.ExportCsvAsync(Model.Users);
        }
    }
}
