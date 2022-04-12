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
    public partial class Blocklist : BaseComponent
    {
        [Parameter] public string LoggedinUserId { get; set; }
        private List<User> BlockedUsers = new List<User>();
        private List<Model_Profile> BlockedProfiles = new List<Model_Profile>();

        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
            //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);

            BlockedUsers = await UserService.GetBlockedUserOnNetwork(UXUtility.LoggedInUser.Id);
            //BlockedProfiles = await ProfileService.GetProfileListofUser(BlockedUsers);

        }

        protected async Task UnblockPeopleOnClick(string blockedUserId)
        {
            try
            {
                User User = new User();
                User.Id = UXUtility.LoggedInUser.Id;
                User.Core.Id = UXUtility.LoggedInUser.Id;
                User.BlockedIds.Add(blockedUserId);

                HttpResponseMessage response = await UserService.PutAsync(User, "UnBlockUser");
                if (response.IsSuccessStatusCode)
                {
                    string blockedUserName = BlockedUsers.Find(usr => usr.Core.Id == blockedUserId).Name;
                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("User Unblock", blockedUserId, blockedUserName, blockedUserName + " Unblocked by you.");
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);

                    await CallBackAfterUnblock();
                    User.BlockedIds.Clear();
                    //ToastService.ShowSuccess("User Unblocked!");
                }
                else
                {
                    //ToastService.ShowSuccess("User Unblocking Failed!");
                    SLog.Write("Unable To Unblock People..!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task CallBackAfterUnblock()
        {
            BlockedUsers = await UserService.GetBlockedUserOnNetwork(UXUtility.LoggedInUser.Id);
            //BlockedProfiles = await ProfileService.GetProfileListofUser(BlockedUsers);
            StateHasChanged();
        }
    }
}
