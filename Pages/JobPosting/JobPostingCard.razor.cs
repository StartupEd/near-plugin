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
    public partial class JobPostingCard : BaseComponent
    {
        [Parameter]
        public JobPosting Model { get; set; }
        [Parameter]
        public BusinessPage Page { get; set; }
        [Parameter]
        public EventCallback<string> GetJobId { get; set; }
        public CS_JobPosting ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public Registration UserRegistered = new Registration();
        public List<Registration> RModel { get; set; } = new List<Registration>();
        public bool IsApplied { get; set; } = false;

        public DateTime LocalDate = DateTime.UtcNow;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_JobPosting(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            RModel = await RegistrationService.GetRegisteredUsersForJobPosting(Model.Id);
            IsApplied = await RegistrationService.CheckUserRegisterationInJobPosting(Model.Id);
            StateHasChanged();
        }

        protected void CallShowSweetAlertDeleteJobPosting(string Id)
        {
            GetJobId.InvokeAsync(Id);
        }
    }
}
