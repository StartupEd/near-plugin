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
    public partial class SearchedEventCard : BaseComponent
    {
        [Parameter] public Model_Event Model { get; set; }
        public Registration RegisteredUser { get; set; }
        public CS_Event ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public bool ButtonClick = false;
        public DateTime LocalDate = DateTime.UtcNow;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                RegisteredUser = new Registration();
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                if (Model.Core.Id != null)
                {
                    bool Registered = await RegistrationService.GetRegistrationDetailAsync(Model.Core.Id);
                    if (Registered)
                    {
                        ButtonClick = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                ToastService.ShowError(ex.Message, "Events Exception");
            }
        }
    }
}
