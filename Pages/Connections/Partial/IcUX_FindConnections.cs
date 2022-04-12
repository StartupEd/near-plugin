using Microsoft.AspNetCore.SignalR.Client;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class IcUX_FindConnections : BaseComponent
    {       
        public UserFullInfo UserProfileInfo { get; set; }
        protected static List<UserFullInfo> PotentialConnections { get; set; } = new List<UserFullInfo>();
        protected static CS_User UserClientService { get; set; }

        protected ConnectionRequest RequestModel { get; set; } = new ConnectionRequest();
        protected CS_ConnectionRequest ClientService { get; set; }
        protected CS_ChatThread ChatClientService { get; set; }

        protected List<User> MyConnections { get; set; } = new List<User>();
        protected User MyDetails { get; set; } = new User();

        protected User UserDetail { get; set; } = new User();

        protected List<string> MutualIds { get; set; } = new List<string>();
        protected int MutualConnections { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                string LoggedinUserId = UXUtility.LoggedInUser.Id;                
                UserProfileInfo = new UserFullInfo();
                
                UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                ChatClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //notificationNFCHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();
                //await notificationNFCHubConnection.StartAsync();

                MyConnections = await UserClientService.GetUserConnectionsListAsync(LoggedinUserId);
                MyDetails = await UserClientService.GetUserById(LoggedinUserId);

                FindState = LoadingContainerState.Loading;
                PotentialConnections = await UserClientService.GetPotentialConnectionsOnNetwork(LoggedinUserId);
                FindState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                FindState = LoadingContainerState.Error;
                //ToastService.ShowError("Not Able To Find Connections!", "Connection Exception!");
            }
        }

        #region--Find New Connections        
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState FindState;
        private HubConnection notificationNFCHubConnection;

        protected string MessageValue { get; set; }
        protected string SearchString { get; set; }
        protected bool IsNoteAlertPopUp { get; set; } = false;
        private string ReceiverUserId { get; set; }

        protected async Task CallPotentialConnectionData(bool Value)
        {
            if (Value)
            {
                FindState = LoadingContainerState.Loading;
                PotentialConnections = await UserClientService.GetPotentialConnectionsOnNetwork(UXUtility.LoggedInUser.Id);
                FindState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
        }


        Task SendNFCNotification() => notificationNFCHubConnection.SendAsync("SendNotification", ReceiverUserId, "You have new connection request.");
        public bool IsNFCConnected => notificationNFCHubConnection.State == HubConnectionState.Connected;

        protected async Task SubmitRequestModel(User Model)
        {
            try
            {
                if (Model != null)
                {
                    ClosePersonalizeNoteModal(false);
                    ConnectionRequest EmailModel = new ConnectionRequest();
                    EmailModel.RequestEmailModel.FromName = UXUtility.LoggedInUser.Name;
                    EmailModel.RequestEmailModel.ToName = Model.Name;
                    EmailModel.RequestEmailModel.ToEmail = Model.Email;
                    EmailModel.RequestEmailModel.Note = MessageValue;

                    string loggedinUserId = UXUtility.LoggedInUser.Id;
                    RequestModel.FromUserId = loggedinUserId;
                    RequestModel.ToUserId = Model.Id;
                    if (!string.IsNullOrEmpty(MessageValue))
                    {
                        RequestModel.HasMessageRequest = true;
                    }
                    HttpResponseMessage response = await ClientService.PostAsync(RequestModel, "CreateRequest");
                    if (response.IsSuccessStatusCode)
                    {
                        await CallPotentialConnectionData(true);
                        if (!string.IsNullOrEmpty(MessageValue))
                        {
                            await SendMessageWithConnectionRequest(Model.Id);
                            MessageValue = "";
                        }
                        bool result = await ClientService.ConnectionRequestEmailNotification(EmailModel);

                        //Notification NotificationModel = new Notification();
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = Model.Id;
                        //ReceiverUserId = Model.Id;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Connections";
                        //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has sent you a connection request";
                        //NotificationModel.Url = "network/user/pending-requests";
                        //await NotificationClientService.PostAsync(NotificationModel);
                        //if (IsNFCConnected) await SendNFCNotification();
                        //ToastService.ShowSuccess("Connection Request Sent..!", "Successful");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowError(ex.Message, "Connection Request Failed");
            }
        }

        protected async Task SendMessageWithConnectionRequest(string ConnectionId)
        {
            try
            {
                ChatThread ThreadModel = new ChatThread();
                HttpResponseMessage response;
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                ThreadModel.ParticipantUserIds = ParticipantIds;
                if (!string.IsNullOrEmpty(MessageValue))
                {
                    ThreadModel.Messages.Add(new ChatMessage { AuthorId = UXUtility.LoggedInUser.Id, Value = MessageValue, SentAt = DateTime.UtcNow });
                }
                response = await ChatClientService.PostAsync(ThreadModel, "CreateThread");
                if (response.IsSuccessStatusCode)
                {
                    SLog.Write("Personalize Note Sending Success..!!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected void SendConnectionRequestAsync(User Model)
        {
            IsNoteAlertPopUp = true;
            UserDetail = Model;
        }
        protected void ClosePersonalizeNoteModal(bool value)
        {
            if (value == false)
            {
                IsNoteAlertPopUp = false;
            }
        }

        protected bool IsFindConnectionTabVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.Persona.ToLower().StartsWith(SearchString) || Connection.Persona.ToUpper().StartsWith(SearchString))
                return true;

            if (Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString))
                return true;

            return false;
        }

        public Dictionary<string, string> FindConnRoutes = new Dictionary<string, string>(){
             { "Find Connection","/network/connections" }
        };

        public void DisposeNotification()
        {
            _ = notificationNFCHubConnection.DisposeAsync();
        }
        #endregion

        #region--Pending Incoming Connection Requests

        #endregion

        #region--Sent Connection Requests

        #endregion

        #region--View Sent Connection Requests

        #endregion


    }
}
