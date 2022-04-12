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
    public partial class ChatBox : BaseComponent
    {
        protected AlertModal DeleteConfirmation;
        private string MessageValue { get; set; }
        [Parameter] public string ConnectionId { get; set; }
        [Parameter] public string ConnectionName { get; set; }
        [Parameter] public string ConnectionEmail { get; set; }
        [Parameter] public string UserAvatarUrl { get; set; }
        [Parameter] public string ConnectionAvatarUrl { get; set; }
        [Parameter] public ChatThread PreviousChats { get; set; }
        private CS_User UserService { get; set; }
        public CS_ChatThread ChatClientService { get; set; }
        private List<ChatMessage> ChatMessages = new List<ChatMessage>();
        private ChatThread Model = new ChatThread();
        public static List<ResultDoc<SearchUnit>> SearchResults { get; set; }
        public static CS_User UserService1 { get; set; }

        private HubConnection ChathubConnection;

        IJSObjectReference module = null;
        protected override async void OnInitialized()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/chat.js");
            await module.InvokeVoidAsync("inboxElement");

        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ChatClientService = new CS_ChatThread(ClientExchange,EngineServices.MarketNetwork);
                UserService1 = new CS_User(ClientExchange,EngineServices.MarketNetwork);
                ChathubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/chathub").Build();
                await ChathubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        [JSInvokable]
        public static async Task<List<ResultDoc<SearchUnit>>> ReturnUsers(string QueryString)
        {
            SearchResults = await UserService1.SearchUser(QueryString);

            return SearchResults;
        }


        Task SendChatMessage() => ChathubConnection.SendAsync("SendMessage", UXUtility.LoggedInUser.Id, ConnectionId);

        public bool IsChatConnected => ChathubConnection.State == HubConnectionState.Connected;

        private async Task SubmitChatMessage()
        {
            try
            {
                if (PreviousChats.Messages.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine("First Message Checking : Sending Email To Connection");
                    await SendFirstChatEmailNotification();
                }

                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(ConnectionId);
                Model.ParticipantUserIds = ParticipantIds;
                Model.Messages.Clear();     //Clearing Previous Messages from ChatThread

                ChatMessage message = new ChatMessage();
                message.AuthorId = UXUtility.LoggedInUser.Id;
                MessageValue = await JSRuntime.InvokeAsync<string>("getPostBoxValue", "msgTextarea");
                if (MessageValue != "<br>")
                {
                    message.Value = MessageValue;
                    message.SentAt = DateTime.UtcNow;
                    Model.Messages.Add(message);
                    HttpResponseMessage response = await ChatClientService.PutAsync(Model, "ChatMessage");
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsChatConnected) await SendChatMessage();
                        SLog.Write("Message Sending Success..!!");
                        MessageValue = string.Empty;
                    }
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

        /// <summary>
        /// Use method :CallDeleteModalAsync() & DeleteEntireConversation() to delete Message Thread...
        /// </summary>
        ///
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
                        SLog.Write("Chat Thread Deleted Successfully..!!");
                        navigationManager.NavigateTo($"inbox/message");
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

        protected async void SubmitBlockUserOnNetwork()
        {
            try
            {
                UserService = new CS_User(ClientExchange,EngineServices.MarketNetwork);
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


        public void Dispose()
        {
            _ = ChathubConnection.DisposeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {

                    IJSObjectReference module1 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/composer.js");
                    await module1.InvokeVoidAsync("composerJS", "msgTextarea", "#msgTextareaToolbar");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
    }
}
