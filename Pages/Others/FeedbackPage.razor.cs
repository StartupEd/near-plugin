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
    public partial class FeedbackPage : BaseComponent
    {
        public Feedback Model { get; set; }
        public CS_Feedback ModelService;
        private bool IsSubmit { get; set; } = false;

        protected override void OnInitialized()
        {
            Model = new Feedback();
            Model.Name = UXUtility.LoggedInUser.Name;
            Model.Email = UXUtility.LoggedInUser.Email;
            ModelService = new CS_Feedback(ClientExchange, EngineServices.MarketNetwork);

        }
        // CustomerScore();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await JSRuntime.InvokeVoidAsync("scoreSlider");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
        public async Task SubmitFeedback()
        {
            try
            {
                IsSubmit = true;
                HttpResponseMessage response = await ModelService.PostAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    await ModelService.PostAsync(Model, "FeedbackEmail");
                    //ToastService.ShowSuccess("Your Feedback Has Been Successfully Recorded");
                    //navigationManager.NavigateTo("/home");
                    IsSubmit = false;
                }
                else
                {
                    //ToastService.ShowError(await response.Content.ReadAsStringAsync(), "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Error");
            }


        }

        protected override async Task OnInitializedAsync()
        {
            await SyncLoggedInUserStatus();
        }


    }
}
