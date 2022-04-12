using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.Incubation.Pages.Chatting;
using Microsoft.AspNetCore.SignalR.Client;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class IcUX_MyConnections : BaseComponent
    {
        [Parameter] public string ConnectionsOfUserId { get; set; } // using Freddie to get User Connections
        protected MessengerModal ConfirmShowMessenger { get; set; }
        protected string SearchString { get; set; }
        protected int MutualConnections { get; set; }
        protected User MyDetails { get; set; } = new User();
        protected Model_Profile MyProfile { get; set; } = new Model_Profile();
        protected List<User> MutualIds { get; set; } = new List<User>();

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        // **For- My Connection **
        public List<UserFullInfo> ConnectionsFullInfo { get; set; }
        private CS_User UserClientService { get; set; }
        private CS_Profile ProfileClientService { get; set; }

        //For:: Messenger Popup-->
        protected string connectionId { get; set; }
        protected string connectionName { get; set; }
        protected string connectionEmail { get; set; }
        protected string connectionAvatar { get; set; }
        private Model_ChatThread PreviousChats { get; set; }
        private CS_ChatThread ChatClientService { get; set; }
        IJSObjectReference _appModule = null;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ChatClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
                ProfileClientService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);

                MyDetails = await UserClientService.GetUserById(UXUtility.LoggedInUser.Id); // If Cache Service will Work properly, can Remove this Rest Call
                MyProfile = await ProfileClientService.GetProfileByUserId(UXUtility.LoggedInUser.Id);  // If Cache Service will Work properly, can Remove this Rest Call

                if (!string.IsNullOrEmpty(ConnectionsOfUserId))
                {
                    if (UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
                    {
                        ConnectionsFullInfo = await UserClientService.GetConnectionsListFromServer(ConnectionsOfUserId);
                        State = LoadingContainerState.Loaded;
                        StateHasChanged();
                    }
                }
                else
                {
                    ConnectionsFullInfo = await UserClientService.GetConnectionsListFromServer(UXUtility.LoggedInUser.Id);
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

        protected async Task UserConnectionCallBack()
        {
            ConnectionsFullInfo = await UserClientService.GetConnectionsListFromServer(UXUtility.LoggedInUser.Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }


        //protected async ValueTask<ItemsProviderResult<UserFullInfo>> LoadMyConnectionData(ItemsProviderRequest request)
        //{
        //    return new ItemsProviderResult<UserFullInfo>(ConnectionsFullInfo.Skip(request.StartIndex).Take(request.Count), ConnectionsFullInfo.Count());
        //}

        protected async Task ShowMessengerPopupModal(string connectionid)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionid))
                {
                    await LoadChatHistory(connectionid);
                    ConfirmShowMessenger.ShowMessenger();
                    UserFullInfo profileDetail = ConnectionsFullInfo.Find(pf => pf.ProfileModel.Core.CreatorId == connectionid);
                    UserFullInfo connectionDetail = ConnectionsFullInfo.Find(pf => pf.UserModel.Core.Id == connectionid);
                    connectionId = connectionid;
                    connectionName = connectionDetail.UserModel.Name;
                    connectionEmail = connectionDetail.UserModel.Email;
                    connectionAvatar = profileDetail.ProfileModel.AvatarImageUrl;
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task LoadChatHistory(string ConnectionId)
        {
            try
            {
                Model_ChatThread Model = new Model_ChatThread();
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                Model.ParticipantUserIds = ParticipantIds;
                //PreviousChats = await ChatClientService.GetConversationHistoryAsync(Model);
                StateHasChanged();
                Console.WriteLine("Record Of Previous Chat : " + PreviousChats.Messages.Count);
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        protected bool IsMyConnectionTabVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.Persona.ToLower().StartsWith(SearchString) || Connection.Persona.ToUpper().StartsWith(SearchString))
                return true;

            if (Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString))
                return true;

            return false;
        }

        protected Dictionary<string, string> Routes = new Dictionary<string, string>(){
            { "My Connections","/network/user/connections" }
        };
    }
}
