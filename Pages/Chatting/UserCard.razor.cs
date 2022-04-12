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
    public partial class UserCard : BaseComponent
    {
        [Parameter] public User User { get; set; }
        [Parameter] public Profile Profile { get; set; }
        [Parameter] public EventCallback ConnectionClicked { get; set; }

        private ChatThread ThreadModel = new ChatThread();
        private CS_ChatThread ClientService { get; set; }

        private async Task SubmitChatThreadModel()
        {
            ClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
            try
            {
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(User.Id);
                ThreadModel.ParticipantUserIds = ParticipantIds;

                HttpResponseMessage response = await ClientService.PostAsync(ThreadModel, "CreateThread");
                if (response.IsSuccessStatusCode)
                {
                    await ConnectionClicked.InvokeAsync(true);
                    navigationManager.NavigateTo($"inbox/message/{User.Id}");
                    SLog.Write("Participants Chat Thread Created Successfully..!!");
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
