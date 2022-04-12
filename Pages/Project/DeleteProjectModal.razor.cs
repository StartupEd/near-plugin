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
    public partial class DeleteProjectModal : BaseComponent
    {
        [Parameter] public Project Modal { get; set; }
        [Parameter] public EventCallback<string> LoadData { get; set; }
        public CS_Project ClientServices { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public string args { get; set; }
        HttpResponseMessage response { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientServices = new CS_Project(ClientExchange, EngineServices.MarketNetwork);

            }
            catch (Exception ex)
            {
                SLog.Write(ex);

            }
        }

        public async Task DeleteProject()
        {
            await module.InvokeAsync<object>("HideModal", "exampleModalCenter1");
            try
            {

                response = await ClientServices.DeleteAsync(Modal.Core.Id);
                if (response.IsSuccessStatusCode)
                {
                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("Project", Modal.Core.Id, Modal.Name, "You have deleted project " + Modal.Name);
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                    //ToastService.ShowSuccess("Project is Deleted!!");
                    LoadData.InvokeAsync(args);
                }
                else
                {
                    //ToastService.ShowError(response.ReasonPhrase, "Oops something went wrong!!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }

        }
        IJSObjectReference module = null;
        protected override async void OnInitialized()
        {

            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");


        }
    }
}
