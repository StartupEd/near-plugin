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
    public partial class EditJobPosting : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public JobPosting Model = new JobPosting();
        public CS_JobPosting ClientService { get; set; }
        public List<Select> JobTitles { get; set; } = new List<Select>();
        public bool Validation = true;
        public List<string> JobName { get; set; } = new List<string>();

        IJSObjectReference module = null;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/wizards/businessPage_wizard.js");
                    await module.InvokeVoidAsync("Wizard", "jobPosting", "jobPostingForm");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }



        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                ClientService = new CS_JobPosting(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetJobPostingbyId(Id);
                JobName.Add(Model.Name);
                if (!JobTitle.Contains(Model.Name))
                {
                    JobTitle.Add(Model.Name);
                }
                Name = Model.Name.Replace(',', ' ');
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }

        private async void SubmitInfo()
        {
            try
            {
                Model.Name = string.Empty;
                var x = await JSRuntime.InvokeAsync<Object>("getName", "jobTitle");

                JobTitles = JsonConvert.DeserializeObject<List<Select>>(x.ToString());
                if (JobTitle != null)
                {
                    foreach (var title in JobTitles)
                    {
                        Model.Name = title.Text;
                    }
                }
                Model.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "jobPostingAbout");
                if (Model.Description == "<p><br></p>" || string.IsNullOrEmpty(Model.Name) || Model.EmploymentType == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }

                HttpResponseMessage response = await ClientService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    //ToastService.ShowSuccess("Job posting updated successfully!");
                    //navigationManager.NavigateTo($"/job/{Id}/{Name}");
                }
                else
                {
                    //ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }


        public List<string> JobTitle = new List<string>(){
        "Assistant","Åssociate","Senior Software Engineer","Software Engineer","Software developer","Junior Developer","Programmer analyst","Software Testing Officer","Quality Testing Officer","Team Leader",
        "Senior Project Manager","Project Director/Project Head" };


        public List<string> EmploymentType = new List<string>() { "Full-Time", "Part-Time", "Contract", "Internship" };


        public string InputElementStyle { get; set; } = "form-control-lg";
        public string LabelStyle { get; set; } = "";
        public string ElementWrapperStyle { get; set; } = "";
        public string FormElementStyle { get; set; } = "";

    }
}
