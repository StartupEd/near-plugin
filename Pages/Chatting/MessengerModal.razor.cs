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
    public partial class MessengerModal : BaseComponent
    {
        protected AlertModal DeleteConfirmation;
        protected bool IsVisibleModal { get; set; }
        private string MessageValue { get; set; }
        [Parameter] public string ConnectionId { get; set; }
        [Parameter] public string ConnectionName { get; set; }
        [Parameter] public string ConnectionEmail { get; set; }
        [Parameter] public string UserAvatarUrl { get; set; }
        [Parameter] public string ConnectionAvatarUrl { get; set; }
        [Parameter] public Model_ChatThread PreviousChats { get; set; }

        private CS_ChatThread ChatClientService { get; set; }
        private ChatThread Model = new ChatThread();

        private CS_User UserService { get; set; }
        private HubConnection hubConnection;

        public void ShowMessenger()
        {
            IsVisibleModal = true;
            StateHasChanged();
        }

        protected void OnCloseButtonClick(bool value)
        {
            if (value == false)
                IsVisibleModal = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ChatClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
                hubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/chathub").Build();
                hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    CallLoadChatHistory();
                    StateHasChanged();
                });
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        Task SendMessage() => hubConnection.SendAsync("SendMessage", UXUtility.LoggedInUser.Id, MessageValue);

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        private void CallLoadChatHistory()
        {
            Task.Run(async () =>
            {
                await LoadChatHistory(ConnectionId);
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

        private async Task SubmitChatMessage()
        {
            try
            {
                if (PreviousChats != null && PreviousChats.Messages.ElementAtOrDefault(0) == null)
                {
                    SLog.Write("First Message Checking : Sending Email To Connection");
                    await SendFirstChatEmailNotification();
                }

                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                Model.ParticipantUserIds = ParticipantIds;
                Model.Messages.Clear();     //Clearing Previous Messages from ChatThread

                ChatMessage message = new ChatMessage();
                message.AuthorId = UXUtility.LoggedInUser.Id;
                message.Value = MessageValue;
                message.SentAt = DateTime.UtcNow;
                Model.Messages.Add(message);

                if (!string.IsNullOrWhiteSpace(MessageValue) || !string.IsNullOrEmpty(MessageValue))
                {
                    HttpResponseMessage response = await ChatClientService.PutAsync(Model, "ChatMessage");
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await SendMessage();
                        SLog.Write("Message Sending Success..!!");
                        MessageValue = string.Empty;
                    }
                }
                else
                {
                    SLog.Write("Empty Message Value Can't Submit...!!");
                    ToastService.ShowWarning("Empty Message Value Can't Send!", "Sending Failed");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                ToastService.ShowError("Message Sending Failed!", "Error");
            }
        }

        protected async Task SendFirstChatEmailNotification()
        {
            try
            {
                Model.EmailModel.SenderName = UXUtility.LoggedInUser.Name;
                Model.EmailModel.ReceiverId = ConnectionId;
                Model.EmailModel.ReceiverName = ConnectionName;
                Model.EmailModel.ReceiverEmail = ConnectionEmail;
                Model.EmailModel.Message = MessageValue;
                bool result = await ChatClientService.SendEmailNotificationForFirstMessage(Model);
                if (result)
                {
                    SLog.Write("First Message Email Notification Send Successful!");
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
                User.BlockedIds.Add(ConnectionId);

                HttpResponseMessage response = await UserService.PutAsync(User, "Block");
                if (response.IsSuccessStatusCode)
                {
                    User.BlockedIds.Clear();
                    ToastService.ShowSuccess("User Blocked On Network!");
                }

            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected void BlockUserMessage()
        {

        }

        protected void CallDeleteModalAsync()
        {
            DeleteConfirmation.ShowModal();
        }

        protected async Task DeleteEntireConversation(bool isConfirmed)
        {
            try
            {
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                Model.ParticipantUserIds = ParticipantIds;
                if (isConfirmed)
                {
                    HttpResponseMessage response = await ChatClientService.PutAsync(Model, "ChatDelete");
                    if (response.IsSuccessStatusCode)
                    {
                        PreviousChats.Messages.Clear();
                        OnCloseButtonClick(false);
                        SLog.Write("Chat Thread Deleted Successfully..!!");
                        //navigationManager.NavigateTo($"inbox/message");
                    }
                    else
                    {
                        throw new Exception("Critical: Unable to delete conversation..!!");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
    }
}
