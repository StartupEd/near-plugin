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
    public partial class CreateJobPosting : BaseComponent
    {
        [Parameter]
        public string BusinessPageId { get; set; }
        [Parameter]
        public string BusinessPageName { get; set; }
        public JobPosting Model = new JobPosting();
        public CS_JobPosting ClientService { get; set; }
        public List<BusinessPage> Pages = new List<BusinessPage>();
        public CS_BusinessPage PageService { get; set; }
        public List<Select> JobTitles { get; set; } = new List<Select>();
        public bool Validation = true;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationJobPostingHubConnection;

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
                PageService = new CS_BusinessPage(ClientExchange, EngineServices.MarketNetwork);
                Pages = await PageService.GetBusinessPageByUserId(UXUtility.LoggedInUser.Id);
                //UploaderService = new FileUploader_ClientService<BusinessPage>(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationJobPostingHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationJobPostingHubConnection.StartAsync();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }
        public bool IsNotiJobPostingConnected => notificationJobPostingHubConnection.State == HubConnectionState.Connected;
        Task SendJobPostingNotification() => notificationJobPostingHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Job Posting Notification");

        private async void SubmitInfo()
        {
            try
            {
                var x = await JSRuntime.InvokeAsync<Object>("getName", "jobTitle");

                JobTitles = JsonConvert.DeserializeObject<List<Select>>(x.ToString());
                if (JobTitles != null)
                {
                    foreach (var title in JobTitles)
                    {
                        Model.Name = title.Text;
                    }
                }
                Model.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "jobPostingAbout");
                if (Model.Description == "<p><br></p>" || Model.Name == null || Model.EmploymentType == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                HttpResponseMessage response = await ClientService.PostAsync(Model, $"AddJobs/{BusinessPageId}");
                if (response.IsSuccessStatusCode)
                {
                    Model_BusinessPage BPage = new Model_BusinessPage(); 
                    var BPagesList = Pages.Where(p => p.Id == BusinessPageId).ToList();
                    if (BPagesList.Count > 0)   //I have done -Vikash becoz throwing IndexOutOfRangeException
                    {
                        BPage = BPagesList[0];
                        foreach (var PageLikedUser in BPage.PageLikes)
                        {
                            if (BPage.Core.CreatorId != null && BPage.Core.CreatorId != PageLikedUser.AuthorId)
                            {
                                //Notification NotificationModel = new Notification();
                                //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                                //NotificationModel.ReceiverId = PageLikedUser.AuthorId;
                                //NotificationModel.SentAt = DateTime.UtcNow;
                                //NotificationModel.NotificationType = "Business Page";
                                //NotificationModel.Message = BPage.Name + " is looking for : " + Model.Name;
                                //NotificationModel.Url = "businesspage/" + BPage.Id + "/" + BPage.Name;
                                //await NotificationClientService.PostAsync(NotificationModel);
                                //if (IsNotiJobPostingConnected) await SendJobPostingNotification();
                            }
                        }
                    }

                    //ToastService.ShowSuccess("Job posting created successfully!");
                    //navigationManager.NavigateTo($"/businesspage/{BusinessPageId}/{BusinessPageName}");
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
