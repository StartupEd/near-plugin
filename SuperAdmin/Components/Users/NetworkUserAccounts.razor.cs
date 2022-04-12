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

namespace StartupEd.UX.Incubation
{
    public partial class NetworkUserAccounts : BaseComponent
    {
        private string SearchString { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        private CS_User UserService { get; set; }
        private List<UserFullInfo> UserAccounts { get; set; } = new List<UserFullInfo>();

        private CS_ChatThread ClientService { get; set; }
        private CS_Profile ProfileService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);

                UserAccounts = await UserService.GetNetworkUserFromServer(UXUtility.LoggedInUserRole);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
                SLog.Write("User Account List " + UserAccounts.Count);
            }
            catch (Exception e)
            {
                State = LoadingContainerState.Error;
                SLog.Write(e.Message);
            }

        }

        protected async Task DeactivateUserAccount(string UserId)
        {
            try
            {
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                var result = await ProfileService.DeacticateUserProfileAsync(UserId);
                if (result == true)
                {
                    //ToastService.ShowInfo("Account Has Been Deactivated!");
                    await OnUserAccountCallBack();
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

        protected async Task ActivateUserAccount(string UserId)
        {
            try
            {
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                var result = await ProfileService.ActicateUserProfileAsync(UserId);
                if (result == true)
                {
                    ToastService.ShowInfo("Account Has Been Activated!");
                    await OnUserAccountCallBack();
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

        protected async Task SubmitChatThreadModel(string Value)
        {
            ClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
            try
            {
                Model_ChatThread ThreadModel = new Model_ChatThread();
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(Value);
                ThreadModel.ParticipantUserIds = ParticipantIds;

                //HttpResponseMessage response = await ClientService.PostAsync(ThreadModel, "CreateThread");
                //if (response.IsSuccessStatusCode)
                //{
                //    //await OnMessageClick.InvokeAsync(Value);
                //    navigationManager.NavigateTo($"inbox/message/{Value}");
                //    SLog.Write("Participants Chat Thread Created Successfully..!!");
                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async Task OnUserAccountCallBack()
        {
            UserAccounts = await UserService.GetNetworkUserFromServer(UXUtility.LoggedInUserRole);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected bool SearchOnTextChange(User Details)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Details.Persona.ToLower().StartsWith(SearchString) || Details.Persona.ToUpper().StartsWith(SearchString))
                return true;

            if (Details.FirstName.ToLower().StartsWith(SearchString) || Details.FirstName.ToUpper().StartsWith(SearchString) || Details.LastName.ToLower().StartsWith(SearchString) || Details.LastName.ToUpper().StartsWith(SearchString))
                return true;

            return false;
        }

    }
}
