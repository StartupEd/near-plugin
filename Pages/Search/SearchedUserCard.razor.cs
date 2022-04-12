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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class SearchedUserCard : BaseComponent
    {
        [Parameter] public UserFullInfo Data { get; set; }
        private string ConnectButton { get; set; } = "Connect";
        private PersonalizeNoteModal ConfirmConnectionRequest { get; set; }
        string MessageValue { get; set; }
        private ConnectionRequest ConnectionModel = new ConnectionRequest();
        private CS_ConnectionRequest ConnectionService { get; set; }

        private ChatThread ThreadModel = new ChatThread();
        private CS_ChatThread ChatClientService { get; set; }



        protected async override Task OnInitializedAsync()
        {
            ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
        }
        protected async void SendConnectionRequest(bool IsValue)
        {
            try
            {
                ConnectionRequest EmailModel = new ConnectionRequest();

                ConnectionModel.FromUserId = UXUtility.LoggedInUser.Id;
                ConnectionModel.ToUserId = Data.UserModel.Core.Id;

                EmailModel.RequestEmailModel.FromName = UXUtility.LoggedInUser.Name;
                EmailModel.RequestEmailModel.ToName = Data.UserModel.Name;
                EmailModel.RequestEmailModel.ToEmail = Data.UserModel.Email;
                if (!string.IsNullOrEmpty(MessageValue))
                {
                    ConnectionModel.HasMessageRequest = true;
                }
                if (IsValue)
                {
                    HttpResponseMessage response = await ConnectionService.PostAsync(ConnectionModel, "CreateRequest");
                    if (response.IsSuccessStatusCode)
                    {
                        ConnectButton = "Cancel Request";
                        StateHasChanged();
                        if (!string.IsNullOrEmpty(MessageValue))
                        {
                            await SendMessageWithConnectionRequest();
                            MessageValue = "";
                        }
                        //ToastService.ShowSuccess("Connection Request Sent..!", "Successful");

                        // bool result = await ConnectionService.ConnectionRequestEmailNotification(EmailModel);
                        // if (IsConnected) await SendConnectNotification();
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Connection Request Failed");
            }
        }
        protected void CallConnectionAlertModal()
        {
            ConfirmConnectionRequest.ShowModal();
        }

        protected void GoToInboxPage()
        {
            //navigationManager.NavigateTo($"/inbox/message/{Data.UserModel.Core.Id}");
        }

        protected async void CancelSentRequestAsync()
        {
            ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
            ConnectionModel.FromUserId = UXUtility.LoggedInUser.Id;
            ConnectionModel.ToUserId = Data.UserModel.Core.Id;
            var result = await ConnectionService.CancelConnectionRequestAsync(ConnectionModel);
            if (result == true)
            {
                //ToastService.ShowInfo("Connection Request Canceled..!", "Canceled");
                ConnectButton = "Connect";
                StateHasChanged();


            }
            else
            {
                //ToastService.ShowError("Unble To Cancel Request!", "Error");
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
                ParticipantIds.Add(Data.UserModel.Core.Id);
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

    }
}
