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
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;
using StartupEd.Lib.Service;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace StartupEd.UX.Incubation
{
    public partial class XFeed : BaseComponent
    {
        [Parameter] public string UserId { get; set; }
        private List<Post> Posts;
        private NetworkFeedService Service;
        private HubConnection hubConnection;
        public int PostQty = 3000;
        List<Post> posts = new List<Post>();
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        private ReactionListModel OpenReactionList { get; set; }
        private Post PostModel { get; set; } = new Post();
        public int countdata = 0;
        public List<CachedUser> cachedUsers { get; set; }
        public List<CachedUser> cachedUserss { get; set; }
        //public List<Reaction<Post>> reactions { get; set; }
        public CS_User CS_User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                if (UserId == null)
                {
                    if (UXUtility.LoggedInUser.Id != null)
                    {
                        Service = new NetworkFeedService(EngineServices.MarketNetwork, UXUtility.LoggedInUser.Id);
                    }
                    else
                    {
                        //Service = new NetworkFeedService(ClientExchange, EngineServices.MarketNetwork);
                    }
                    cachedUsers = new List<CachedUser>();
                    cachedUserss = new List<CachedUser>();
                    //reactions = new List<Reaction<Post>>();
                    CS_User = new CS_User(ClientExchange, EngineServices.MarketNetwork);


                }
                else
                {
                    Service = new NetworkFeedService(EngineServices.MarketNetwork, UserId);
                }
                hubConnection = new HubConnectionBuilder()
                .WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub")
                .Build();

                hubConnection.On("ReceiveMessage", () =>
                {
                    CallLoadData();
                    StateHasChanged();
                });

                await hubConnection.StartAsync();
                await LoadData();
                // this is for progessive Loading
                //Posts = await Service.GetPostsFromServerWithProgressive(UserId, 0, 100);
                State = LoadingContainerState.Loaded;
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        private void CallLoadData()
        {
            Task.Run(async () =>
            {
                await LoadData();
            });
        }

        //async ValueTask<ItemsProviderResult<Post>> LoadPost(ItemsProviderRequest request)
        //{
        //    Console.WriteLine(request.StartIndex);

        //    var requestQty = Math.Min(request.Count, PostQty - request.StartIndex);
        //    var dummyposts = await Service.GetPostsFromServerWithProgressive(UserId, request.StartIndex, requestQty, request.CancellationToken);
        //    return new ItemsProviderResult<Post>(dummyposts, PostQty);
        //}






        protected async Task LoadData()
        {
            if (UXUtility.LoggedInUser.Id == UserId || UserId == null || UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
            {
                Posts = await Service.GetPostsFromServer(UserId);
            }
            else
            {
                Posts = await Service.GetPostsFromServer(UserId, UXUtility.LoggedInUser.Id);
            }
            await LoadCacheForUser();

        }

        protected async Task CallReactionListModel(string PostId)
        {
            PostModel = Posts.Find(p => p.Id == PostId);
            cachedUserss = await GetReactions(PostModel);
            OpenReactionList.ShowModal();
            StateHasChanged();
        }

        public void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }

        public async Task LoadCacheForUser()
        {
            try
            {
                foreach (var Item in Posts)
                {
                    if (!cachedUsers.Any(cu => cu.UserId == Item.Core.CreatorId))
                    {
                        if (Item.Core.CreatorId != null)
                        {
                            CachedUser cachedUser = await CS_User.CacheUser(Item.Core.CreatorId);
                            cachedUsers.Add(cachedUser);
                        }
                    }
                }
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        public async Task<List<CachedUser>> GetReactions(Post model)
        {
            foreach (var item in model.Reactions)
            {
                if (!cachedUserss.Any(cu => cu.UserId == item.AuthorId))
                {
                    CachedUser cachedUser = await CS_User.CacheUser(item.AuthorId);
                    cachedUserss.Add(cachedUser);
                }

            }
            return cachedUserss;

        }
    }
}
