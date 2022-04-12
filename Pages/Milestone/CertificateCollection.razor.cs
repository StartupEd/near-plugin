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
using StartupEd.UX.Incubation.Pages.Milestone;

namespace StartupEd.UX.Incubation
{
    public partial class CertificateCollection : BaseComponent
    {
        private StartupEdCertificate ConfirmDisplayCertificate { get; set; }
        private List<Certificate> AllCertificates = new List<Certificate>();
        private CS_Certificate CertificateService { get; set; }
        private Model_Certificate MyCertificate { get; set; } = new Model_Certificate();

        protected override async Task OnInitializedAsync()
        {
            CertificateService = new CS_Certificate(ClientExchange, EngineServices.MarketNetwork);

            AllCertificates = await CertificateService.GetClaimedCertificatesFromServer(UXUtility.LoggedInUser.Id);
            StateHasChanged();
        }

        protected void DisplayCertificateModal(string eventId)
        {
            MyCertificate = AllCertificates.Find(opt => opt.EventId == eventId);
            ConfirmDisplayCertificate.ShowCertificateModal();
        }
    }
}
