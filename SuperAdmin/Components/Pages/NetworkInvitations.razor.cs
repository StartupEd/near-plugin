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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.SuperAdmin.Components.Pages
{
    public partial class NetworkInvitations : BaseComponent
    {
        private string SearchString { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        private CS_Invitation InvitationService { get; set; }
        private List<Invitation> Invitations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Invitations = new List<Invitation>();
                State = LoadingContainerState.Loading;
                InvitationService = new CS_Invitation(ClientExchange, EngineServices.MarketNetwork);

                Invitations = await InvitationService.GetNetworkInvitationsFromServer(UXUtility.LoggedInUserRole);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                State = LoadingContainerState.Error;
                SLog.Write(e.Message);
            }

        }
    }
}
