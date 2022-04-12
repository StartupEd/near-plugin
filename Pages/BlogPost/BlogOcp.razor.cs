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
using Newtonsoft.Json;
using StartupEd.UX.One.Shared_Classes;
using System.Net.Http;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BlogPost
{
    public partial class BlogOcp : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public Blog Model = new Blog();
        public CS_Blog ClientService { get; set; }
        public CS_FileUploader UploaderService { get; set; }
       // private UserActivity_ClientService Activity_ClientService { get; set; }
        public bool Validation = true;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Blog(ClientExchange,EngineServices.MarketNetwork);
                UploaderService = new CS_FileUploader(ClientExchange,EngineServices.MarketNetwork);
                if(Id != null)
                {
                    Model = await ClientService.GetBlogById(Id);
                }
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
                Model.BlogSummary = await JSRuntime.InvokeAsync<string>("getEditorValue", "blogSummary");
                if (Model.BlogSummary == "<p><br></p>" || Model.Name == null)
                {
                    ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                if(Id != null)
                {
                    HttpResponseMessage response = await ClientService.PutAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                        await BlogsRecentActivity(Model.Id, Model.Name, "You have made changes in Blog " + Model.Name);

                        ToastService.ShowSuccess("Blog Updated Successfully!");
                        navigationManager.NavigateTo("/blog");
                    }
                    else
                    {
                        ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
                    }
                }
            else
                {
                    Model.AuthorName = UXUtility.LoggedInUser.Name;
                    HttpResponseMessage response = await ClientService.PostAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                        await BlogsRecentActivity(response.Content.ReadAsStringAsync().Result, Model.Name, "You have recently posted a new Blog " + Model.Name);

                        ToastService.ShowSuccess("Blog Created Successfully!");
                        navigationManager.NavigateTo("/blog");
                    }
                    else
                    {
                        ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
                    }
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        protected async Task BlogsRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Model_Activity Module = new Model_Activity();
            //Module.CreateActivity("Blogs", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }

        public async void Imagebase64(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return;
            }
            Model.CoverImage = await UploaderService.UploadFile(src, Model.GetType());
            StateHasChanged();
        }


        public string InputElementStyle { get; set; } = "form-control-lg";
        public string LabelStyle { get; set; } = "";
        public string ElementWrapperStyle { get; set; } = "";
        public string FormElementStyle { get; set; } = "";
    }
}
