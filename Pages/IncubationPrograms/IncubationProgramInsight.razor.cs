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
    public partial class IncubationProgramInsight : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public IncubationProgram Model = new IncubationProgram();
        public List<Registration> RegisteredUser { get; set; } = new List<Registration>();
        public CS_IncubationProgram ClientService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public int SNo = 1;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetIncubationProgramById(Id);
                RegisteredUser = await RegistrationService.GetRegisteredUsersForIncubationProgram(Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

    }
}
