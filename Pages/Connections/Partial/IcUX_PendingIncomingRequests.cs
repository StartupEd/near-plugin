using Microsoft.AspNetCore.SignalR.Client;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class IcUX_PendingIncomingRequests : BaseComponent
    {
        protected bool VirtualizeItem { get; set; } = false;
        private int MutualConnections { get; set; }
        private string SearchString { get; set; }
        private User MyDetails { get; set; } = new User();

        // **For- Received Connection Requests **
        private List<UserFullInfo> PendingRequests { get; set; }
        private CS_User UserClientService { get; set; }
        private List<User> MyConnections { get; set; }
        private List<string> MutualIds { get; set; } = new List<string>();

        private CS_ConnectionRequest RequestService { get; set; }
        private ConnectionRequest RequestModel = new ConnectionRequest();

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private HubConnection notificationPendRHubConnection;
        //private Notification_ClientService NotificationClientService { get; set; }
        private string ReceiverId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                string LoggedinUserId = UXUtility.LoggedInUser.Id;
                MyConnections = new List<User>();
                State = LoadingContainerState.Loading;
                RequestService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);

                MyDetails = await UserClientService.GetUserById(LoggedinUserId); // If Cache Service will Work properly, can Remove this Rest Call
                MyConnections = await UserClientService.GetUserConnectionsListAsync(LoggedinUserId);
                PendingRequests = await UserClientService.GetPendingConnectionRequestsFromServer(LoggedinUserId);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationPendRHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();
                await notificationPendRHubConnection.StartAsync();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
                //ToastService.ShowError("Not Able To Find Connections!", "Connection Exception!");
            }
        }

        //protected async ValueTask<ItemsProviderResult<UserFullInfo>> LoadPendingConnectionData(ItemsProviderRequest request)
        //{
        //    return new ItemsProviderResult<UserFullInfo>(PendingRequests.Skip(request.StartIndex).Take(request.Count), PendingRequests.Count());
        //}

        Task SendAcceptedNotification() => notificationPendRHubConnection.SendAsync("SendNotification", ReceiverId, UXUtility.LoggedInUser.Name + "  Is Now Your Connection.");

        public bool IsPendRConnected => notificationPendRHubConnection.State == HubConnectionState.Connected;

        protected async void AcceptRequestAsync(string fromUserId)
        {
            try
            {
                ReceiverId = fromUserId;
                RequestModel.FromUserId = fromUserId;
                RequestModel.ToUserId = UXUtility.LoggedInUser.Id;
                RequestModel.Status = ConnectionStatus.Accepted;
                var result = await RequestService.AcceptOrRejectConnectionRequestAsync(RequestModel);
                if (result == true)
                {
                    await AcceptRequestCallBack(true);
                    //Notification NotificationModel = new Notification();
                    //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //NotificationModel.ReceiverId = fromUserId;
                    //NotificationModel.SentAt = DateTime.UtcNow;
                    //NotificationModel.NotificationType = "Connections";
                    //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has accepted your connection request";
                    //NotificationModel.Url = "network/user/connections";
                    //await NotificationClientService.PostAsync(NotificationModel);
                    //if (IsPendRConnected) await SendAcceptedNotification();
                    //ToastService.ShowSuccess("Connection Request Accepted..!", "Accepted");
                }
                else
                {
                    throw new Exception("Exception: Unble To Respond Request!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowError(ex.Message);
            }
        }

        protected async void RejectConnectionRequest(string fromUserId)
        {
            try
            {
                RequestModel.FromUserId = fromUserId;
                RequestModel.ToUserId = UXUtility.LoggedInUser.Id;
                RequestModel.Status = ConnectionStatus.Rejected;
                var result = await RequestService.AcceptOrRejectConnectionRequestAsync(RequestModel);
                if (result == true)
                {
                    await AcceptRequestCallBack(true);
                    //ToastService.ShowInfo("Connection Request Rejected..!");
                }
                else
                {
                    throw new Exception("Exception: Unble To Respond Request!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                //ToastService.ShowError(e.Message);
            }
        }

        protected async Task AcceptRequestCallBack(bool Value)
        {
            if (Value)
            {
                PendingRequests = await UserClientService.GetPendingConnectionRequestsFromServer(UXUtility.LoggedInUser.Id);
                VirtualizeItem = true;
                StateHasChanged();
            }
        }

        protected bool IsPendingRequestsTabVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.Persona.ToLower().StartsWith(SearchString) || Connection.Persona.ToUpper().StartsWith(SearchString))
                return true;

            if (Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString))
                return true;

            return false;
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>(){
            { "Pending Requests","/network/user/pending-requests" }
        };

        public void Dispose()
        {
            _ = notificationPendRHubConnection.DisposeAsync();
        }

    }
}
