using StartupEd.UX.One;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using StartupEd.Engine.MarketNetwork;
using Microsoft.JSInterop;
using StartupEd.Lib.Core;

namespace StartupEd.UX.Incubation.Pages.CRMFiles.Partials
{
    public class CRMBase : BaseComponent
    {
        public CRM Model = new CRM();
        public List<CRM> Models = new List<CRM>();
        public CS_CRM _ClientService { get; set; }

        public IJSObjectReference module = null;
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
        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        }

        public async Task CreateCRM(CRM Model) 
        {
            try
            {
                Model.CreatorName = UXUtility.LoggedInUser.Name;
                var response = await _ClientService.PostAsync(Model, "Create");
                if (response.IsSuccessStatusCode) 
                {
                    await module.InvokeAsync<object>("HideModal", "crmModal");
                    await LoadCRMs();
                    //One.Services.ToastService.ShowSuccess("Project Created", "");
                }
                else 
                {

                }
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
            catch(Exception ex) 
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
