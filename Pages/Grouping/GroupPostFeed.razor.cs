using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.SignalR.Client;

namespace StartupEd.UX.Incubation
{
    public partial class GroupPostFeedusing : BaseComponent , IDisposable
    {
        [Parameter] public string GroupId { get; set; }
        [Parameter] public EventCallback<int> TotalPosts { get; set; }
        private ReactionListModel OpenReactionList { get; set; }
        private Post PostModel { get; set; } = new Post();
        private List<Post> Posts { get; set; } = new List<Post>();
        private CS_Post PostService { get; set; }
        public CS_User CS_User { get; set; }
        public List<CachedUser> cachedUsers { get; set; }
        public List<CachedUser> cachedUserss { get; set; }
        private HubConnection hubConnection;

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            State = LoadingContainerState.Loading;
            try
            {
                PostService = new CS_Post(ClientExchange,EngineServices.MarketNetwork);
                CS_User = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                cachedUsers = new List<CachedUser>();
                cachedUserss = new List<CachedUser>();
                hubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub").Build();

                hubConnection.On("ReceiveMessage", () =>
                {
                    CallLoadData();
                    StateHasChanged();
                });

                await hubConnection.StartAsync();
                await LoadData();
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

        protected async Task LoadData()
        {
            Posts = await PostService.GetCommunityPostsFromServer(GroupId);
            await TotalPosts.InvokeAsync(Posts.Count);
            await LoadCacheForUser();
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected async Task CallReactionListModel(string PostId)
        {
            PostModel = Posts.Find(p => p.Id == PostId);
            cachedUserss = await GetReactions(PostModel);
            OpenReactionList.ShowModal();
            StateHasChanged();
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


        public void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
    }
}
