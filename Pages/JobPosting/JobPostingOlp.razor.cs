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
    public partial class JobPostingOlp : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public JobPosting Model = new JobPosting();
        public BusinessPage Page = new BusinessPage();
        public CS_BusinessPage PageService { get; set; }
        public CS_JobPosting ClientService { get; set; }
        public string JobId { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlertForJobPosting { get; set; }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        public void DisableApplybtn()
        {
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ClientService = new CS_JobPosting(ClientExchange, EngineServices.MarketNetwork);
                PageService = new CS_BusinessPage(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetJobPostingbyId(Id);
                List<string> ValueList = new List<string>(Model.DataProperty.ObjectLinks["BusinessPage"]);
                Page = await PageService.GetBusinessPageById(ValueList[0]);
                State = LoadingContainerState.Loaded;
                if (Model != null)
                {
                    Name = Model.Name.Replace(',', ' ');
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
                State = LoadingContainerState.Error;
            }
        }

        public void ShowSweetAlert2(string Id)
        {
            if (Id != null)
            {
                JobId = Id;
            }
            ConfirmSweetAlertForJobPosting.ShowSweetAlert();
        }

        protected async void DeleteJobPostingAsync(bool Value)
        {
            if (Value)
            {
                try
                {
                    HttpResponseMessage message = await ClientService.DeleteAsync(JobId);
                    if (message.IsSuccessStatusCode)
                    {
                        //ToastService.ShowSuccess("Job Posting Deleted Successfuly");
                        //navigationManager.NavigateTo($"/businesspage/{Page.Id}/{Page.Name}");

                    }
                }
                catch (Exception ex)
                {
                    //ToastService.ShowError(ex.Message, "Exception");
                }
            }
        }
    }
}
