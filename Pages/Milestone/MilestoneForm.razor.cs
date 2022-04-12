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
    public partial class MilestoneForm : BaseComponent
    {
        private List<Event> AppliedEvents = new List<Event>();
        private CS_Event EventService { get; set; }
        private List<Certificate> ClaimedCertificates { get; set; }
        private Certificate Model = new Certificate();
        private CS_Certificate CertificateService { get; set; }

        public string BtnDesabled { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
            CertificateService = new CS_Certificate(ClientExchange, EngineServices.MarketNetwork);

            AppliedEvents = await EventService.GetAllEventsByUserId(UXUtility.LoggedInUser.Id);

            ClaimedCertificates = await CertificateService.GetClaimedCertificatesFromServer(UXUtility.LoggedInUser.Id);

        }

        private async Task ClaimForCertificate(string eventId)
        {
            Model_Event MyEvent = AppliedEvents.Find(e => e.Core.Id == eventId);
            Model.EventId = eventId;
            Model.EventName = MyEvent.Name;
            Model.Core.ImageUrl = MyEvent.Core.ImageUrl;
            Model.Name = UXUtility.LoggedInUser.Name;
            HttpResponseMessage response = await CertificateService.PostAsync(Model, "ClaimCertificate");
            if (response.IsSuccessStatusCode)
            {
                SLog.Write("Certificate has been claimed..!!");
                //ToastService.ShowEmpty("Certificate has been claimed..!");
            }

            //BtnDesabled = "desabled";
        }




        public Dictionary<string, object> tagAttributes { get; set; } =
        new Dictionary<string, object>()
        {
        { "class", "form-control kt_selectpicker" }
                                                };

    }
}
