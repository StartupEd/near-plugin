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
    public partial class BoardSettings : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        private MyTask Model = new MyTask();
        private Project PModel = new Project();
        public CS_MyTask ModelService { get; set; }
        public CS_Project ClientService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        string LastUpdatedTask = "";
        public string arg { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Project(ClientExchange, EngineServices.MarketNetwork);
                await LoadData(arg);

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Task Failed Exception");
            }
        }

        void HandleStatusUpdated(Model_MyTask updatedJob)
        {
            LastUpdatedTask = updatedJob.Core.Name;
        }

        protected async Task LoadData(string arg)
        {
            PModel = await ClientService.GetProjectsFromId(Id);
            StateHasChanged();
        }

        public async Task UpdateProject()
        {
            try
            {
                HttpResponseMessage response = await ClientService.PutAsync(PModel);

                if (response.IsSuccessStatusCode)
                {
                    //ToastService.ShowSuccess("Project Updated!");
                    //navigationManager.NavigateTo($"project/{Id}/{PModel.Name.ToLower()}/kanban");
                }
                else
                {
                    //ToastService.ShowError("Updation Failed!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }

        }

        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";
    }
}
