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
    public partial class DirectInviteModal : BaseComponent
    {
        private string EmailAddresses { get; set; }
        IJSObjectReference module1 = null;
        IJSObjectReference module2 = null;

        private async void NetworkInviteAsync()
        {
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

            //EngineServices = MyServiceProvider.GetService<EngineServices>();
            CS_Invitation service = new CS_Invitation(ClientExchange, EngineServices.MarketNetwork);

            HttpResponseMessage response = await service.BulkPostAsync(Invitations);
            if (response.IsSuccessStatusCode)
            {
                ToastService.ShowSuccess("Your friends have received the invitations. Thanks!", "Invitation Sent");
                SLog.Write("Invitation Sent..!");
                await module2.InvokeAsync<object>("HideModal", "inviteModal");
                await JSRuntime.InvokeVoidAsync("RemoveTagifyValue");
            }
            else
            {
                SLog.Write(await response.Content.ReadAsStringAsync());
                ToastService.ShowError("Invitation Failed: You Can't Invite Yourself!");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    module1 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/directInvite.js");
                    await module1.InvokeVoidAsync("inviteTagify", "inviteTagging");
                    module2 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        //protected override async void OnInitialized()
        //{
        //    module1 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/directInvite.js");
        //    await module1.InvokeVoidAsync("inviteTagify", "inviteTagging");
        //    module2 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
        //}
    }
}
