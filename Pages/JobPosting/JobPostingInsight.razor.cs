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
using StartupEd.UX.One.Components.Layout.Base;
using System.Linq;
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class JobPostingInsight : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }


        public List<Registration> ApplicationList = new List<Registration>();
        //public List<File> ResumeList = new List<File>();
        public CS_FileUploader FileService { get; set; }
        public CS_FileUpload ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public int y = 1;

        protected override async Task OnInitializedAsync()
        {
            FileService = new CS_FileUploader(ClientExchange, EngineServices.MarketNetwork);
            ModelService = new CS_FileUpload(ClientExchange, EngineServices.MarketNetwork);
            RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
            ApplicationList = await RegistrationService.GetRegisteredUsersForJobPosting(Id);
            //ResumeList = await ModelService.GetResumesByJobPostingId(Id);
        }
    }
}
