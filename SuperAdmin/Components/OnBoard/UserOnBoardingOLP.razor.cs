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

namespace StartupEd.UX.Incubation.SuperAdmin.Components.OnBoard
{
    public partial class UserOnBoardingOLP : BaseComponent
    {
         public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        private List<Authentication> UserAccounts = new List<Authentication>();
        private CS_Authentication ClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                ClientService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
                UserAccounts = await ClientService.GetCreatedAccountforOnBoard(UXUtility.LoggedInUserRole);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }

        }

        protected void OnCreateUserAccounts(bool value)
        {
            if (value)
            {
                navigationManager.NavigateTo("/root/user-onboard/create");
            }
        }
    }
}
