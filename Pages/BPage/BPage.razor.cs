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

namespace StartupEd.UX.Incubation.Pages.BPage
{
    public partial class BPage : BaseComponent
    {
        public BusinessPage Model = new BusinessPage();
        public CS_BusinessPage ClientService { get; set; }

        [Parameter] public string Id { get; set; }
        [Parameter] public string PageName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                Model = await ClientService.GetBusinessPageById(Id);
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }
    }
}
