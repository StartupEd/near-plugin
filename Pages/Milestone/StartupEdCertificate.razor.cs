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

namespace StartupEd.UX.Incubation.Pages.Milestone
{
    public partial class StartupEdCertificate : BaseComponent
    {
        [Parameter] public Model_Certificate Certificate { get; set; }
        [Parameter] public bool IsCertificateShow { get; set; }


        public void ShowCertificateModal()
        {
            IsCertificateShow = true;
        }

        protected async Task OnButtonCloseClick(bool value)
        {
            // await module.InvokeAsync<object>("HideModal", "confirmationModal");
            IsCertificateShow = false;
        }
    }
}
