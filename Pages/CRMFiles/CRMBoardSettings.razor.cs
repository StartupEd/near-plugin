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
    public partial class CRMBoardSettings : BaseComponent
    {
        [Parameter] public string CRMId { get; set; }
        public CRM Model = new CRM();
        public CS_CRM _ClientService { get; set; }
        public Board board1 = new Board();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                _ClientService = new CS_CRM(ClientExchange, EngineServices.MarketNetwork);
                await LoadCRM();
                await base.OnInitializedAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public async Task LoadCRM()
        {
            try
            {
                Model = await _ClientService.GetCRMById(CRMId);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        public async Task UpdateModel()
        {
            try
            {
                // HttpResponseMessage response = await _ClientService.PutAsync(Model);

                //if (response.IsSuccessStatusCode)
                //{
                //    ToastService.ShowSuccess("Project Updated!");
                //    navigationManager.NavigateTo($"project/kanban/{Id}");
                //}
                //else
                //{
                //    ToastService.ShowError("Updation Failed!");
                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }

        }


        public void UpdateBoards(List<Board> board, EventArgs e)
        {

        }

        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";
    }
}
