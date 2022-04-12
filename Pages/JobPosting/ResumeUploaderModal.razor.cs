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
using Microsoft.AspNetCore.Components.Forms;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class ResumeUploaderModal : BaseComponent
    {
        [Parameter]
        public JobPosting Posting { get; set; }
        [Parameter]
        public EventCallback Returncallback { get; set; }
        //public File Model = new File();
        public Registration RegistrationModel { get; set; }
        public CS_FileUpload UploaderService { get; set; }
        //public CS_FileUpload ModelService { get; set; }
        public CS_JobPosting ClientService { get; set; }
        public CS_Registration Service { get; set; }
        public JobApplicationTracking ApplicationTracking = new JobApplicationTracking();
        public bool DisplayFile { get; set; } = false;
        public string data { get; set; }
        public string fileurl { get; set; }
        public string InputId = "kt_uppy_5_input_control-";
        public bool value { get; set; } = true;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationJobHubConnection;
        private bool IsUploading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            UploaderService = new CS_FileUpload(ClientExchange, EngineServices.MarketNetwork);
            //ModelService = new CS_FileUpload(ClientExchange, EngineServices.MarketNetwork);
            Service = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
            ClientService = new CS_JobPosting(ClientExchange, EngineServices.MarketNetwork);
            InputId = InputId + Posting.Id;
            //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
            notificationJobHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
            await notificationJobHubConnection.StartAsync();
        }
        public bool IsNotiJobConnected => notificationJobHubConnection.State == HubConnectionState.Connected;
        Task SendJobNotification() => notificationJobHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Job Notification");
        IJSObjectReference module = null;

        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            try
            {
                var maxAllowedFiles = 1;
                foreach (var File in e.GetMultipleFiles(maxAllowedFiles))
                {
                    string Extension = File.Name.Substring(File.Name.LastIndexOf('.') + 1).ToLower();
                    if (Extension == "doc" || Extension == "docx" || Extension == "pdf")
                    {
                        var buffer = new byte[File.Size];
                        await File.OpenReadStream(5242880).ReadAsync(buffer);
                        //Model.Name = File.Name;
                        //Model.Type = File.ContentType;
                        //Model.Size = File.Size;
                        //data = Extension + "." + Convert.ToBase64String(buffer);
                        //await ReturnImgbase64.InvokeAsync(data);
                    }
                    else
                    {
                        SLog.Write("Format Exception: Uploaded file is not in correct format.");
                        //ToastService.ShowWarning("Format Exception: Uploaded file must be pdf/doc/docx format.");
                        return;
                    }
                }
                e = null;
                DisplayFile = true;
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowWarning("your File is grather the 5 Mb");
            }
        }

        public async void CancelFile()
        {
            DisplayFile = false;
        }

        public async Task SubmitModel()
        {
            try
            {
                IsUploading = true;
                RegistrationModel = new Registration();
                if (data == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Upload File .....");
                    return;
                }
                //Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //RegistrationModel.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //RegistrationModel.ApplyDate = DateTime.UtcNow;
                //RegistrationModel.Name = UXUtility.LoggedInUser.Name;
                //RegistrationModel.EmailId = UXUtility.LoggedInUser.Email;
                //Model.UploadUrl = await UploaderService.UploadFile(data);
                //ApplicationTracking.FileUploaderModel = Model;
                //ApplicationTracking.PostingModel = Posting;
                //ApplicationTracking.RegistrationModel = RegistrationModel;
                bool response = await ClientService.SubmitJobApplication(ApplicationTracking);
                if (response)
                {
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = Posting.Core.CreatorId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Jobs";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has applied for " + Posting.Name + " role";
                    //NotificationModel.Url = "job/" + Posting.Id + "/" + Posting.Name + "/insight";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsNotiJobConnected) await SendJobNotification();

                    //ToastService.ShowSuccess("Thanks for Applying!");
                    value = false;
                    IsUploading = false;
                    Returncallback.InvokeAsync();
                    await Task.Delay(1000);
                    await module.InvokeAsync<object>("HideModal", $"resumeUploader-{Posting.Core.Id}");
                }
                else
                {
                    SLog.Write("File UploadFailed Failed");
                    //ToastService.ShowError("Attachment upload failed.", "Upload Failed");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }
    }
}
