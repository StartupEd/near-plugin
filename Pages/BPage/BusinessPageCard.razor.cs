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
using System.Net.Http;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BPage
{
    public partial class BusinessPageCard : BaseComponent
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Parameter] public BusinessPage Page { get; set; }
        [Parameter] public EventCallback<string> GetId { get; set; }
       public CS_BusinessPage PageService { get; set; }
       // private Notification_ClientService NotificationClientService { get; set; }
       // private UserActivity_ClientService Activity_ClientService { get; set; }
        private HubConnection notificationBPLikeHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
               // Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                PageService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationBPLikeHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationBPLikeHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                ToastService.ShowError(ex.Message, "Events Exception");
            }
        }
        public bool IsNotiBPLikeConnected => notificationBPLikeHubConnection.State == HubConnectionState.Connected;
        Task SendBPageLikeNotification() => notificationBPLikeHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Business Page Notification");

        public async Task PageLikes()
        {
            try
            {
                if (UXUtility.LoggedInUser.Id != null)
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

                    HttpResponseMessage message = await PageService.PutAsync(Page);
                    if (message.IsSuccessStatusCode)
                    {
                        await BusinessPageRecentActivity(Page.Id, Page.Name, "You have recently liked Business Page" + Page.Name);
                        if (Page.Core.CreatorId != null && Page.Core.CreatorId != UXUtility.LoggedInUser.Id)
                        {
                            Notification NotificationModel = new Notification();
                            //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                            //NotificationModel.ReceiverId = Page.Core.CreatorId;
                            //NotificationModel.SentAt = DateTime.UtcNow;
                            //NotificationModel.NotificationType = "Business Page";
                            //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has liked your business page " + "'" + Page.Name + "'";
                            //NotificationModel.Url = "businesspage/" + Page.Id + "/" + Page.Name;
                            //await NotificationClientService.PostAsync(NotificationModel);
                            if (IsNotiBPLikeConnected) await SendBPageLikeNotification();
                        }
                        ToastService.ShowSuccess("Page Like Successfuly");
                    }
                }
                else
                {
                    Navigation.NavigateTo($"login?returnUrl={Navigation.Uri}");
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
                HttpResponseMessage message = await PageService.PutAsync(Page);
                if (message.IsSuccessStatusCode)
                {
                    ToastService.ShowSuccess("Page UnLike Successfuly");

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

        protected void CallShowSweetAlertDeletePage(string Id)
        {
            GetId.InvokeAsync(Id);
        }
    }
}
