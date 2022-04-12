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
using Newtonsoft.Json;
using StartupEd.UX.One.Services;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.Chatting
{
    public partial class InboxLandingPage : BaseComponent
    {
        private string SearchString { get; set; }
        [Parameter] public string ConnectionId { get; set; }
        public string ConnectionName { get; set; }
        public string ConnectionEmail { get; set; }
        private string UserImageUrl { get; set; }
        private string ConnectionImageUrl { get; set; }

        public List<UserFullInfo> ConnectionsFullInfo { get; set; }
        public List<User> UserChatRequest = new List<User>();
        private List<Profile> ProfileChatRequest = new List<Profile>();
        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }

        private CS_ChatThread ChatClientService { get; set; }
        private ChatThread PreviousChats = new ChatThread();
        private ChatThread Model = new ChatThread();
        private HubConnection hubConnection;

        //protected async override void OnInitialized()
        //{
        //    IJSObjectReference _appModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/chatscroll.js");
        //    await _appModule.InvokeVoidAsync("InboxAutoScrolling");
        //}

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                ConnectionsFullInfo = new List<UserFullInfo>();
                UserService = new CS_User(ClientExchange,EngineServices.MarketNetwork);
                ProfileService = new CS_Profile(ClientExchange,EngineServices.MarketNetwork);
                ChatClientService = new CS_ChatThread(ClientExchange,EngineServices.MarketNetwork);

                //Getting Connections of User..
                ConnectionsFullInfo = await UserService.GetConnectionsListFromServer(UXUtility.LoggedInUser.Id);
                StateHasChanged();

                //Getting User And Profile for LoggedinUser has message request....
                UserChatRequest = await UserService.GetNetworkUserForMessageRequest(UXUtility.LoggedInUser.Id);
                ProfileChatRequest = await ProfileService.GetProfileForMessageRequest(UXUtility.LoggedInUser.Id);

                hubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/chathub").Build();

                hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    CallLoadChatHistory();
                    StateHasChanged();
                });
                await hubConnection.StartAsync();

                if (ConnectionId == null)
                {
                    ConnectionId = ConnectionsFullInfo[0].UserModel.Id;
                }
                if (!string.IsNullOrEmpty(ConnectionId))
                {
                    OnConnectionChanged(ConnectionId);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ToastService.ShowError(ex.Message, "Error!");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    IJSObjectReference _appModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/chatscroll.js");
                    await _appModule.InvokeVoidAsync("InboxAutoScrolling");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CallLoadChatHistory()
        {
            Task.Run(async () =>
            {
                await LoadChatHistory(ConnectionId);
            });
        }

        private void OnConnectionChanged(string connectionId)
        {
            ConnectionId = connectionId;
            UserFullInfo UserConnection = ConnectionsFullInfo.Find(uc => uc.UserModel.Core.Id == connectionId);
            ConnectionName = UserConnection.UserModel.Name;
            ConnectionEmail = UserConnection.UserModel.Email;
            // Getting Profile Picture Of Connected user..
            UserFullInfo ConnectionProfile = ConnectionsFullInfo.Find(pr => pr.ProfileModel.Core.CreatorId == connectionId);
            ConnectionImageUrl = ConnectionProfile.ProfileModel.Core.ImageUrl;

            Task.Run(async () =>
            {
                await LoadChatHistory(connectionId);
            });
        }
        protected async Task LoadChatHistory(string ConnectionId)
        {
            try
            {
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                Model.ParticipantUserIds = ParticipantIds;
                PreviousChats = await ChatClientService.GetConversationHistoryAsync(Model);
                StateHasChanged();
                Console.WriteLine("Record Of Previous Chat : " + PreviousChats.Messages.Count);
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        //Chat Thread Load On Accept Message Request
        protected void OnClickUserChatRequest(string UserId)
        {
            try
            {
                User user = UserChatRequest.Find(uc => uc.Core.Id == UserId);
                ConnectionName = user.Name;

                Model_Profile Profile = ProfileChatRequest.Find(pr => pr.Core.CreatorId == UserId);
                ConnectionImageUrl = Profile.Core.ImageUrl;

                Task.Run(async () =>
                {
                    await LoadChatHistory(UserId);
                });
                //navigationManager.NavigateTo($"inbox/message/{UserId}");
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        //Search connection filter
        protected bool IsVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString))
                return true;

            else if (Connection.Persona.ToUpper().StartsWith(SearchString) || Connection.Persona.ToLower().StartsWith(SearchString))
                return true;

            return false;
        }
    }
}
