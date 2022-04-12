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
    public partial class AddColumnModal : BaseComponent
    {
        [Parameter]
        public Project Modal { get; set; }
        [Parameter] public EventCallback<string> OnStatusUpdated { get; set; }
        public CS_Project ClientServices { get; set; }
        public string ColumnName;
        public string ModalClass { get; set; }
        public string arg { get; set; }
        public async Task SubmitModel()
        {
            await module.InvokeAsync<object>("HideModal", "exampleModalCenter");
            try
            {
                ClientServices = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                if (ColumnName != null)
                {
                    Modal.BoardStatus.Add(ColumnName);
                    HttpResponseMessage response = await ClientServices.PutAsync(Modal);
                    if (response.IsSuccessStatusCode)
                    {
                        await OnStatusUpdated.InvokeAsync(arg);
                        ColumnName = null;

                        if (ModalClass == string.Empty)
                        {
                            ModalClass = "modal";
                            ModalClass = string.Empty;
                        }
                        //ToastService.ShowSuccess("New Column Add Successfully");
                    }
                    else
                    {
                        SLog.Write("Column Adding  Failed");
                        //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                    }
                }
                else
                {
                    //ToastService.ShowError("Please Enter Column Name");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        public void ClearColumnName()
        {
            ColumnName = null;
        }


        IJSObjectReference module = null;
        protected override async void OnInitialized()
        {

            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");


        }
    }
}
