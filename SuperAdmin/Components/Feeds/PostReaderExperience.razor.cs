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
using StartupEd.UX.Incubation.Pages.FeedPage;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class PostReaderExperience : BaseComponent
    {
        [Parameter] public string CommunityId { get; set; } = null;
        [Parameter] public EventCallback<int> TotalPosts { get; set; }

        private ReactionListModel OpenReactionList { get; set; }
        private Post PostModel { get; set; } = new Post();

        private List<Post> UserPosts;
        private CS_Post PostService { get; set; }

        private HubConnection roothubConnection;
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                PostService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);

                roothubConnection = new HubConnectionBuilder()
            .WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub")
            .Build();

                roothubConnection.On("ReceiveMessage", () =>
                {
                    CallLoadData();
                    StateHasChanged();
                });

                await roothubConnection.StartAsync();
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
            if (!string.IsNullOrEmpty(CommunityId) && UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
            {
                UserPosts = await PostService.GetCommunityPostsFromServer(CommunityId);
                await TotalPosts.InvokeAsync(UserPosts.Count);
            }
            else
            {
                UserPosts = await PostService.GetNetworkPostsFromServer(UXUtility.LoggedInUserRole);
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected void CallReactionListModel(string PostId)
        {
            PostModel = UserPosts.Find(p => p.Id == PostId);
            OpenReactionList.ShowModal();
        }

        public void Dispose()
        {
            _ = roothubConnection.DisposeAsync();
        }
    }
}
