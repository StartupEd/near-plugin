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
using StartupEd.Engine.MarketNetwork;
using StartupEd.UX.One.Services;

namespace StartupEd.UX.Incubation
{
    public partial class UserSentConnectionRequests :  BaseComponent
    {
        private string SearchString { get; set; }
        private User MyDetails { get; set; } = new User();
        private int MutualConnections { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private HubConnection hubConnection;

        // **For- Sent Connection Requests **
        public List<User> SentRequestUser { get; set; }
        public List<Profile> SentRequestProfile { get; set; }
        private List<string> MutualIds { get; set; } = new List<string>();
        private List<User> MyConnections { get; set; }

        private CS_User UserClientService { get; set; }
        private CS_Profile ProfileClientService { get; set; }

        private CS_ConnectionRequest RequestService { get; set; }
        private ConnectionRequest RequestModel = new ConnectionRequest();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string LoggedinUserId = UXUtility.LoggedInUser.Id;
                State = LoadingContainerState.Loading;
                UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ProfileClientService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);

                MyDetails = await UserClientService.GetUserById(LoggedinUserId); // If Cache Service will Work properly, can Remove this Rest Call
                MyConnections = await UserClientService.GetUserConnectionsListAsync(LoggedinUserId);
                SentRequestUser = await UserClientService.GetSentConnectionRequestsFromServer(LoggedinUserId);
                SentRequestProfile = await ProfileClientService.GetProfileForSentConnectionRequest(LoggedinUserId);
                State = LoadingContainerState.Loaded;
                StateHasChanged();

                hubConnection = new HubConnectionBuilder()
                .WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();

                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
               // ToastService.ShowError("Not Able To Find Connections!", "Connection Exception!");
            }
        }

        protected async void CancelSentRequestAsync(string toUserId)
        {
            try
            {
                RequestService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                string loggedinUserId = UXUtility.LoggedInUser.Id;
                RequestModel.FromUserId = loggedinUserId;
                RequestModel.ToUserId = toUserId;
                var result = await RequestService.CancelConnectionRequestAsync(RequestModel);
                if (result == true)
                {
                    await CancelRequestCallBack(true);
                    //ToastService.ShowInfo("Connection Request Cancelled..!");
                }
                else
                {
                    throw new Exception("Exception: Unble To Respond!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                //ToastService.ShowError(e.Message);
            }

        }

        protected async Task CancelRequestCallBack(bool value)
        {
            if (value)
            {
                SentRequestUser = await UserClientService.GetSentConnectionRequestsFromServer(UXUtility.LoggedInUser.Id);
                SentRequestProfile = await ProfileClientService.GetProfileForSentConnectionRequest(UXUtility.LoggedInUser.Id);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
        }

        protected bool IsViewSentRequestTabVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.Persona.ToLower().StartsWith(SearchString) || Connection.Persona.ToUpper().StartsWith(SearchString))
                return true;

            if (Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString))
                return true;

            return false;
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
       { "Sent Requests","/network/user/sent-requests" }
    };
    }
}
