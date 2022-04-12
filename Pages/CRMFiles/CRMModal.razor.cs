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
    public partial class CRMModal : BaseComponent
    {
        public CRM Model = new CRM();
    [Parameter] public CS_CRM _ClientService { get; set; }
    [Parameter] public EventCallback LoadDataCallBack { get; set; }
    public IJSObjectReference module = null;
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
            await 
                LoadDataCallBack.InvokeAsync();
            if (response.IsSuccessStatusCode)
            {
                await module.InvokeAsync<object>("HideModal", "crmModal");
                ToastService.ShowSuccess("CRM Created");
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
    }
}
