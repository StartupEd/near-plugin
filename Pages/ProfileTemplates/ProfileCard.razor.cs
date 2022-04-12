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
using StartupEd.UX.Incubation.Pages.Connections;
using StartupEd.UX.Incubation.Shared.Pages;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class ProfileCard : BaseComponent
    {
        private PersonalizeNoteModal ConfirmConnectionRequest { get; set; }
        private ReportModal ConfirmReportRequest { get; set; }
        [Parameter] public Dictionary<string, string> Tabs { get; set; }

        [Parameter] public Model_Profile ProfileModel { get; set; }

        [Parameter] public User UserModel { get; set; }

        [Parameter] public string ConnectButton { get; set; }

        [Parameter] public EventCallback<string> OnClickCallBack { get; set; }

        private HubConnection notificationHubConnection;

        private string MessageValue { get; set; }
        private string ProfileOfUser { get; set; }
        private string Display { get; set; }
        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }

        private ConnectionRequest ConnectionModel = new ConnectionRequest();
        private CS_ConnectionRequest ConnectionService { get; set; }

        private Model_ChatThread ThreadModel = new Model_ChatThread();
        private CS_ChatThread ChatClientService { get; set; }

        private Report ReportModel = new Report();
        private CS_Report ReportService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
            notificationHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
            await notificationHubConnection.StartAsync();
        }


        private bool IsConnected => notificationHubConnection.State == HubConnectionState.Connected;
        Task SendConnectNotification() => notificationHubConnection.SendAsync("SendNotification", UserModel.Core.Id, "You have new connection request.");

        Task SendAcceptedNotification() => notificationHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name + "  Is Now Your Connection.");


        protected void CallUserReportModal()
        {
            ConfirmReportRequest.ShowReportModal();
        }

        protected void EditUserProfile()
        {
            //navigationManager.NavigateTo($"/root/user-profile/{UserModel.Core.Id}");
        }

        protected void GoToInboxPage()
        {
            //navigationManager.NavigateTo($"/inbox/message/{UserModel.Core.Id}");
        }


        protected void CallConnectionAlertModal()
        {

            if (UXUtility.LoggedInUser.Id != null)
            {
                ConfirmConnectionRequest.ShowModal();
            }
            else
            {
                //Navigation.NavigateTo($"login?returnUrl={Navigation.Uri}");
            }
        }


        #region ..Here Send personalize Message with connection request..!!
        protected async void SendConnectionRequest(bool IsValue)
        {
            try
            {
                ConnectionRequest EmailModel = new ConnectionRequest();

                ConnectionModel.FromUserId = UXUtility.LoggedInUser.Id;
                ConnectionModel.ToUserId = UserModel.Core.Id;

                EmailModel.RequestEmailModel.FromName = UXUtility.LoggedInUser.Name;
                EmailModel.RequestEmailModel.ToName = UserModel.Name;
                EmailModel.RequestEmailModel.ToEmail = UserModel.Email;
                if (!string.IsNullOrEmpty(MessageValue))
                {
                    ConnectionModel.HasMessageRequest = true;
                }
                if (IsValue)
                {
                    HttpResponseMessage response = await ConnectionService.PostAsync(ConnectionModel, "CreateRequest");
                    if (response.IsSuccessStatusCode)
                    {
                        if (!string.IsNullOrEmpty(MessageValue))
                        {
                            await SendMessageWithConnectionRequest();
                            MessageValue = "";
                        }
                        //ToastService.ShowSuccess("Connection Request Sent..!", "Successful");
                        await OnClickCallBack.InvokeAsync(UserModel.Id);
                        bool result = await ConnectionService.ConnectionRequestEmailNotification(EmailModel);
                        // if (IsConnected) await SendConnectNotification();
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Connection Request Failed");
            }
        }
        protected async Task SendMessageWithConnectionRequest()
        {
            ChatClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
            try
            {
                HttpResponseMessage response;
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(UserModel.Core.Id);
                ThreadModel.ParticipantUserIds = ParticipantIds;
                if (!string.IsNullOrEmpty(MessageValue))
                {
                    ThreadModel.Messages.Add(new ChatMessage { AuthorId = UXUtility.LoggedInUser.Id, Value = MessageValue, SentAt = DateTime.UtcNow });
                }
                //response = await ChatClientService.PostAsync(ThreadModel, "CreateThread");
                //if (response.IsSuccessStatusCode)
                //{
                //    SLog.Write("Personalize Note Sending Success..!!");
                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
        #endregion

        protected async void CancelSentRequestAsync()
        {
            ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
            ConnectionModel.FromUserId = UXUtility.LoggedInUser.Id;
            ConnectionModel.ToUserId = UserModel.Core.Id;
            var result = await ConnectionService.CancelConnectionRequestAsync(ConnectionModel);
            if (result == true)
            {
                //ToastService.ShowInfo("Connection Request Canceled..!", "Canceled");
                await OnClickCallBack.InvokeAsync(UserModel.Core.Id);
            }
            else
            {
                //ToastService.ShowError("Unble To Cancel Request!", "Error");
            }
        }

        protected async void AcceptRequestAsync()
        {
            try
            {
                ConnectionModel.FromUserId = ProfileModel.Core.CreatorId;
                ConnectionModel.ToUserId = UXUtility.LoggedInUser.Id;
                ConnectionModel.Status = ConnectionStatus.Accepted;
                var result = await ConnectionService.AcceptOrRejectConnectionRequestAsync(ConnectionModel);
                if (result == true)
                {
                    //ToastService.ShowSuccess("Connection Request Accepted..!", "Accepted");
                    if (IsConnected) await SendAcceptedNotification();
                    await OnClickCallBack.InvokeAsync(UserModel.Core.Id);
                }
                else
                {
                    //ToastService.ShowError("Unble To Respond Request!", "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }

        }

        protected async void RejectConnectionRequest()
        {
            try
            {
                ConnectionModel.FromUserId = ProfileModel.Core.CreatorId;
                ConnectionModel.ToUserId = UXUtility.LoggedInUser.Id;
                ConnectionModel.Status = ConnectionStatus.Rejected;
                var result = await ConnectionService.AcceptOrRejectConnectionRequestAsync(ConnectionModel);
                if (result == true)
                {
                    //ToastService.ShowInfo("Connection Request Rejected..!", "Rejected");
                    await OnClickCallBack.InvokeAsync(UserModel.Core.Id);
                }
                else
                {
                    //ToastService.ShowError("Unble To Respond Request!", "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async void RemoveConnectionAsync()
        {
            try
            {
                ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                ConnectionModel.FromUserId = UXUtility.LoggedInUser.Id;
                ConnectionModel.ToUserId = UserModel.Core.Id;
                ConnectionModel.Status = ConnectionStatus.Removed;
                var result = await ConnectionService.RemoveConnectionFromUserConnections(ConnectionModel);
                if (result == true)
                {
                    //ToastService.ShowSuccess("Connection Has Been Removed..!", "Successful");
                    await OnClickCallBack.InvokeAsync(UserModel.Core.Id);
                }
                else
                {
                    //ToastService.ShowError("Unble To Respond Your Request!", "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async void SubmitBlockUserOnNetwork()
        {
            try
            {
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                User User = new User();
                User.Id = UXUtility.LoggedInUser.Id;
                User.Core.Id = UXUtility.LoggedInUser.Id;
                User.BlockedIds.Add(UserModel.Id);

                HttpResponseMessage response = await UserService.PutAsync(User, "Block");
                if (response.IsSuccessStatusCode)
                {
                    User.BlockedIds.Clear();
                    //ToastService.ShowSuccess("User Blocked!");
                }

            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async void SubmitProfileReport(string Value)
        {
            try
            {
                //ReportService = new Report_ClientService(ClientExchange, EngineServices.MarketNetwork);
                if (!string.IsNullOrEmpty(Value))
                {
                    ReportModel.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    ReportModel.ReportedId = UserModel.Core.Id;
                    ReportModel.Content = Value.TrimEnd(';');
                    HttpResponseMessage response = await ReportService.PostAsync(ReportModel, "CreateReport");
                    if (response.IsSuccessStatusCode)
                    {
                        //ToastService.ShowSuccess("User Report has been submitted..");
                    }
                }
                else
                {
                    //ToastService.ShowWarning("Please Select Option(s) to Report.", "Empty Option!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task DeactivateUserAccount()
        {
            try
            {
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                var result = await ProfileService.DeacticateUserProfileAsync(UserModel.Core.Id);
                if (result == true)
                {
                    //ToastService.ShowInfo("Account Has Been Deactivated!");
                    await OnClickCallBack.InvokeAsync(UserModel.Core.Id);
                }
                else
                {
                    throw new Exception("Exception: Account Deactivation Failed!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
