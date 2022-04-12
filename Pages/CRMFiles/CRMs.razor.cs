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
using System.Net.Http;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.CRMFiles
{
    public partial class CRMs : BaseComponent
    {
        public List<CRM> Models = new List<CRM>();
        public CS_CRM _ClientService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                _ClientService = new CS_CRM(ClientExchange, EngineServices.MarketNetwork);
                await LoadCRMs();
                await base.OnInitializedAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        public async Task LoadCRMs()
        {
            try
            {
                Models = await _ClientService.GetCRMsUsingCreatorId(UXUtility.LoggedInUser.Id);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
