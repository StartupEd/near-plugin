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
using StartupEd.UX.One.Services;

namespace StartupEd.UX.Incubation
{
    public partial class BPageInsight : BaseComponent
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string PageName { get; set; }
        public BusinessPage Page { get; set; } = new BusinessPage();
        public CS_BusinessPage ClientService { get; set; }
        public List<JobPosting> Jobs { get; set; }
        public CS_JobPosting Service { get; set; }
        public List<Model_BusinessPage> BP { get; set; } = new List<Model_BusinessPage>();
        public int y = 1;
        public int x = 1;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                Service = new CS_JobPosting(ClientExchange,EngineServices.MarketNetwork);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                if (Id != null)
                {
                    Page = await ClientService.GetBusinessPageById(Id);
                    BP.Add(Page);
                    Jobs = await Service.GetJobPostingsByBusinessPageId(Id);
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        public async void DownloadCsv()
        {
            //await ModelService.ExportCsvAsync(Model.Users);
        }
    }
}
