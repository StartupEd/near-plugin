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
using System.Net.Http;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
namespace StartupEd.UX.Incubation
{
    public partial class BPageOLP : BaseComponent
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string PageName { get; set; }
        public string BusinessPageRoute { get; set; }
        public BusinessPage Page { get; set; } = new BusinessPage();
        public List<JobPosting> Jobs = new List<JobPosting>();
        public CS_BusinessPage ClientService { get; set; }
        public CS_JobPosting Service { get; set; }
        public bool ShowAlertBox { get; set; } = false;
        public string ProgramView { get; set; } = "Flix";
        private string SearchString { get; set; }
        public string JobId { get; set; }
        public bool IsApplied { get; set; } = false;
        private OneUX_SweetAlertBox ConfirmSweetAlertForBPage { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlertForJobPosting { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        //private Notification_ClientService NotificationClientService { get; set; }
       // private UserActivity_ClientService Activity_ClientService { get; set; }
        private HubConnection notificationBPOLikeHubConnection;

        public void DisableApplybtn()
        {

            StateHasChanged();
        }

        public void ToggleView(string view)
        {

            ProgramView = view;
            StateHasChanged();
        }


        public void ShowSweetAlert1()
        {
            ConfirmSweetAlertForBPage.ShowSweetAlert();
        }

        public void ShowSweetAlert2(string Id)
        {
            if (Id != null)
            {
                JobId = Id;
            }
            ConfirmSweetAlertForJobPosting.ShowSweetAlert();
        }



        protected override async Task OnInitializedAsync()
        {
            try
            {
                PageName = PageName.Replace(" ", "-");
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                ClientService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                Service = new CS_JobPosting(ClientExchange,EngineServices.MarketNetwork);
               // NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationBPOLikeHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationBPOLikeHubConnection.StartAsync();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }
        public bool IsNotiBPOLikeConnected => notificationBPOLikeHubConnection.State == HubConnectionState.Connected;
        Task SendBPageOLikeNotification() => notificationBPOLikeHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Business Page Notification");

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                if (Id != null)
                {
                    Page = await ClientService.GetBusinessPageById(Id);
                    BusinessPageRoute = $"/{Page.Id}/{Page.Name}/job/create";
                    Jobs = await Service.GetJobPostingsByBusinessPageId(Id);
                    Jobs = Jobs.OrderByDescending(x => x.Core.CreatedAt).ToList();
                    State = LoadingContainerState.Loaded;
                    StateHasChanged();
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
            }
        }

        public async void DeleteBPage(bool IsValue)
        {
            try
            {
                if (IsValue)
                {
                    HttpResponseMessage message = await ClientService.DeleteAsync(Page.Id);
                    if (message.IsSuccessStatusCode)
                    {
                        await BusinessPageRecentActivity(Page.Id, Page.Name, "You have recently deleted Business Page" + Page.Name);

                       // ToastService.ShowSuccess("Page Deleted Successfuly");
                        _NavigationManager.NavigateTo("/businesspage");
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Page Exception");
            }
        }

        public Dictionary<string, string> Tabs = new Dictionary<string, string>() {
        {"About","fas fa-user"},
        {"Jobs","fas fa-lightbulb"},
        //{"Events","fas fa-calendar-alt"},
       // {"Incubation Programs","fas fa-user-graduate"}
        //{"Entrepreneurial Education","fas fa-book"},
    };


        public async Task PageLikes()
        {
            try
            {

                if (!Page.PageLikes.Any(pl => pl.AuthorId == UXUtility.LoggedInUser.Id))
                {
                    Like like = new Like();
                    like.AuthorId = UXUtility.LoggedInUser.Id;
                    like.AuthorName = UXUtility.LoggedInUser.Name;
                    like.LikedAt = DateTime.Now;
                    like.IsLiked = true;
                    like.AuthorEmail = UXUtility.LoggedInUser.Email;
                    Page.PageLikes.Add(like);
                }
                else
                {
                    Page.PageLikes.Where(pl => pl.AuthorId == UXUtility.LoggedInUser.Id).
                       Select(c => { c.IsLiked = true; c.LikedAt = DateTime.Now; return c; }).ToList();
                }

                HttpResponseMessage message = await ClientService.PutAsync(Page);
                if (message.IsSuccessStatusCode)
                {
                    await BusinessPageRecentActivity(Page.Id, Page.Name, "You have recently liked Business Page" + Page.Name);

                    //ToastService.ShowSuccess("Page Like Successfuly");
                    if (Page.Core.CreatorId != null && Page.Core.CreatorId != UXUtility.LoggedInUser.Id)
                    {
                        //Notification NotificationModel = new Notification();
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = Page.Core.CreatorId;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Business Page";
                        //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has liked your business page " + "'" + Page.Name + "'";
                        //NotificationModel.Url = "businesspage/" + Page.Id + "/" + Page.Name;
                        //await NotificationClientService.PostAsync(NotificationModel);
                        if (IsNotiBPOLikeConnected) await SendBPageOLikeNotification();
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public async Task PageUnLike()
        {
            if (Page.PageLikes.Any(pl => pl.AuthorId == UXUtility.LoggedInUser.Id))
            {
                Page.PageLikes.Where(pl => pl.AuthorId == UXUtility.LoggedInUser.Id).
                    Select(c => { c.IsLiked = false; c.LikedAt = DateTime.Now; return c; }).ToList();
                HttpResponseMessage message = await ClientService.PutAsync(Page);
                if (message.IsSuccessStatusCode)
                {
                    //ToastService.ShowSuccess("Page UnLike Successfuly");

                }

            }
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected bool IsVisible(Model_JobPosting Job)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Job.Name.ToUpper().StartsWith(SearchString) || Job.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }


        protected async void DeleteJobPostingAsync(bool Value)
        {
            if (Value)
            {
                try
                {
                    Jobs = await Service.DeleteObject(Jobs, JobId);
                    await BusinessPageRecentActivity(Page.Id, Page.Name, "You have recently deleted a job " + Jobs.Find(j => j.Id == JobId).Name + " from Business Page" + Page.Name);
                    if (Jobs != null)
                    {
                        StateHasChanged();
                        //ToastService.ShowSuccess("Job Posting Deleted Successfuly");
                    }
                }
                catch (Exception ex)
                {
                   // ToastService.ShowError(ex.Message, "Deletion Failed");
                }

            }
        }

        protected async Task BusinessPageRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(new Model_Activity { FeatureId = Id, Feature = Title, Description = description });
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
