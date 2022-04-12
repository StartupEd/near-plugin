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
using System.Text.RegularExpressions;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages
{
    public partial class  NetworkLandingPage : BaseComponent
    {
        [Parameter]
        public EventCallback<ChangeEventArgs> FilterChanged { get; set; }
        private Post Model = new Post();
        private CS_Post ClientService;
        public bool IsPosted { get; set; } = false;
        public CS_Event ModelService { get; set; }

        public List<Community> RecommendedGroups { get; set; } = new List<Community>();
        public CS_Community GroupService { get; set; }

        private HubConnection hubConnection;
        private HubConnection notificationFeedHubConnection;
        //private Notification_ClientService NotificationClientService { get; set; }
        private CS_User UserService { get; set; }
        public List<User> UserConnections = new List<User>();
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                SLog.Write("NLP OnInitialized");
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);

                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);

                await LoadData();

                //CachedUser uc = new CachedUser { FullName = UXUtility.LoggedInUser.Name, Status = true };
                //await SetHeartBeat(UXUtility.LoggedInUser.Id, uc);
                hubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub").Build();

                notificationFeedHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                //notificationFeedHubConnection.On<string, string>("ReceiveNotification", (user, message) =>
                //{
                //    if (user == UXUtility.LoggedInUser.Id && message != "Feed")
                //    {
                //        ToastService.ShowInfo(message);
                //    }
                //});
                await notificationFeedHubConnection.StartAsync();
                await hubConnection.StartAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }

        }


        protected async Task LoadData()
        {

            RecommendedGroups = await GroupService.GetRecommendedCommunityAsync(UXUtility.LoggedInUser.Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }
        private string ConvertUrlsToLinks(string msg)
        {
            string regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.Replace(msg, "<a href=\"$1\" title=\"Click to open in a new window or tab\" target=\"&#95;blank\">$1</a>").Replace("href=\"www", "href=\"http://www");
        }
        private async Task SubmitPost()
        {
            try
            {
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                Model.AuthorId = UXUtility.LoggedInUser.Id;
                Model.Core.Name = UXUtility.LoggedInUser.Name;
                Model.Summary = await JSRuntime.InvokeAsync<string>("getPostBoxValue", "ideaComposer_editor");
                Model.Summary = ConvertUrlsToLinks(Model.Summary);
                if (Model.Summary != "<p><br></p>")
                {
                    //NotifySlack("New Post: '" + Model.Summary + "'\nBy: " + UXUtility.LoggedInUser.Name + "<" + UXUtility.LoggedInUser.Email + ">");
                    HttpResponseMessage response = await ClientService.PostAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                        //XFeed = XFeed.LoadingContainerState.Loaded;
                        if (IsConnected) await SendMessage();
                        ToastService.ShowSuccess("IDEA Posted!", "Thank you");
                        UserConnections = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                        foreach (var u in UserConnections)
                        {
                            //Notification NotificationModel = new Notification();
                            //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                            //NotificationModel.ReceiverId = u.Id;
                            //NotificationModel.SentAt = DateTime.UtcNow;
                            //NotificationModel.NotificationType = "feed";
                            //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has posted on feed";
                            //NotificationModel.Url = "post/" + response.Content.ReadAsStringAsync().Result;
                            //await NotificationClientService.PostAsync(NotificationModel);
                            //if (IsNotiConnected) await SendFeedNotification();
                        }
                        UserActivity userActivity = new UserActivity();
                        //Activity Module = new Activity();
                        //Module.CreateActivity("Feed", response.Content.ReadAsStringAsync().Result, UXUtility.LoggedInUser.Name, "You have posted new idea on Feed");
                        //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                        //userActivity.Activities.Add(Module);
                        //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                    }
                    else
                    {
                        SLog.Write("Posting Failed");
                        ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                    }
                }
                //XFeed = XFeed.LoadingContainerState.Loaded;

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                ToastService.ShowError(ex.Message, "Post Failed Exception");
            }
        }

        public async Task SetHeartBeat(string Id, CachedUser uc)
        {
            //Cache_ClientService cacheService = new Cache_ClientService(UXUtility.Engines.MarketNetwork);
            //await cacheService.SelfSetValue(Id, JsonConvert.SerializeObject(uc));
            //await cacheService.SelfSetValue("OnlineUsers", Id);     // ToDo migrate to LPUSH
            //SLog.Write("Cache Updated");
        }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;
        Task SendMessage() => hubConnection.SendAsync("SendMessage");

        public bool IsNotiConnected => notificationFeedHubConnection.State == HubConnectionState.Connected;
        Task SendFeedNotification() => notificationFeedHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Feed");

        public Dictionary<string, object> tagAttributes { get; set; } = new Dictionary<string, object>() { { "class", "form-control selectpicker" }, { "Name", "DatewiseIdea" }, { "Id", "DateFilter" }, { "data-width", "50%" } };
        public void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
        public void DisposeNotification()
        {
            _ = notificationFeedHubConnection.DisposeAsync();
        }

    }
}
