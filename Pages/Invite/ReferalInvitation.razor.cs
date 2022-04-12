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

namespace StartupEd.UX.Incubation.Pages.Invite
{
    public partial class ReferalInvitation : BaseComponent
    {
        private string EmailAddresses { get; set; }
        private string ReferalLink { get; set; }
        private UserReferalInvites UserReferalInvites = new UserReferalInvites();
        private CS_Invitation InvitationService { get; set; }
        private CS_User UserService { get; set; }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private bool IsInviting { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                InvitationService = new CS_Invitation(ClientExchange, EngineServices.MarketNetwork);
                UserReferalInvites = await InvitationService.GetUserReferalInvitationsAsync(UXUtility.LoggedInUser.Id);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (UserReferalInvites.UserModel != null)
            {
                if (string.IsNullOrEmpty(UserReferalInvites.UserModel.ReferalLink))
                {
                    ReferalLink = await UserService.UpdateInvitationReferalLinkAsync(UserReferalInvites.UserModel);
                    UserReferalInvites.UserModel.ReferalLink = ReferalLink;
                    //StateHasChanged();
                    Console.WriteLine(UserReferalInvites.UserModel.ReferalLink);
                }
            }
        }

        protected async void LoadInvitationData()
        {
            UserReferalInvites = await InvitationService.GetUserReferalInvitationsAsync(UXUtility.LoggedInUser.Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        private async void NetworkInviteAsync()
        {
            try
            {
                IsInviting = true;
                List<string> emails = UXUtility.ExtractStringsFromTagify(EmailAddresses);
                if (emails == null)
                {
                    ToastService.ShowError("Please enter valid Email Addresses.");
                    return;
                }
                List<Invitation> Invitations = new List<Invitation>();
                foreach (string email in emails)
                {
                    if (UXUtility.IsEmailValid(email) == false)
                    {
                        ToastService.ShowError("Please enter valid Email Addresses.");
                        return;
                    }
                    else
                    {
                        Invitations.Add(new Invitation(UXUtility.LoggedInUser.Id, email));
                    }
                }

                HttpResponseMessage response = await InvitationService.BulkPostAsync(Invitations);
                if (response.IsSuccessStatusCode)
                {
                    LoadInvitationData();
                    ToastService.ShowSuccess("Your friends have received the invitations. Thanks!", "Invitation Sent");
                    SLog.Write("Invitation Sent..!");
                    await JSRuntime.InvokeVoidAsync("RemoveTagifyValue");
                    IsInviting = false;
                }
                else
                {
                    SLog.Write(await response.Content.ReadAsStringAsync());
                    ToastService.ShowError("Invitation Failed: You Can't Invite Yourself!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                IsInviting = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    IJSObjectReference tagmodule = null;
                    tagmodule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/directInvite.js");
                    await tagmodule.InvokeVoidAsync("inviteTagify", "referalInviteTagging");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>(){
       { "Invite a Friend","/invite" }
    };

    }
}
