using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class CommunityALP : BaseComponent
    {
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        private bool IsVirtualize { get; set; } = false;
        private List<Community> JoinedGroups { get; set; } = new List<Community>();
        private List<Community> RecommendedGroups { get; set; }
        private CS_Community GroupService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        private string deleteGroupId { get; set; }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                JoinedGroups = await GroupService.GetGroupForMemberOrCreator(UXUtility.LoggedInUser.Id);
                if (UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
                {
                    RecommendedGroups = await GroupService.GetNetworkAllGroupsFromServer(UXUtility.LoggedInUserRole);
                }
                else
                {
                    //RecommendedGroups = await GroupService.GetRecommendedGroupsAsync(UXUtility.LoggedInUser.Id);
                }
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (NullReferenceException ex)
            {
                SLog.Write(ex.Message);
                State = LoadingContainerState.Error;
            }
        }

        IJSObjectReference module = null;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/flix.js");
                await module.InvokeVoidAsync("BrowserFlix", "mygroupsList");
            }
        }


        private async Task LeaveGroupAsync(string groupId)
        {
            try
            {
                if (!string.IsNullOrEmpty(groupId))
                {
                    Community GroupInfo = new Community();
                    GroupInfo.Id = groupId;
                    HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "LeaveGroup");
                    if (response.IsSuccessStatusCode)
                    {
                        await CommunityInfoCallBack();
                        SLog.Write("User has leaved group.." + groupId);
                        //ToastService.ShowInfo("You have leaved community!");
                    }
                }
                else
                {
                    throw new Exception("Exception: Not Able TO Leave Community!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected void CallDeleteGroupAlert(string groupId)
        {
            if (groupId != null)
            {
                deleteGroupId = groupId;
                ShowSweetAlertBox.ShowSweetAlert();
            }
        }

        public async void DeleteCommunityAsync(bool value)
        {
            try
            {
                if (value)
                {
                    string groupName = JoinedGroups.Find(g => g.Id == deleteGroupId).Name;
                    JoinedGroups = await GroupService.DeleteObject(JoinedGroups, deleteGroupId);
                    if (JoinedGroups.Count >= 0)
                    {
                        Model_Activity userActivity = new Model_Activity();
                        //Activity Module = new Activity(ClientExchange, EngineServices.MarketNetwork);
                        //Module.CreateActivity("Community", deleteGroupId, groupName, groupName + " Community has been Deleted By You.");
                        //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                        //userActivity.Activities.Add(Module);
                        //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                        StateHasChanged();
                        SLog.Write("Communty has been deleted!");
                        //ToastService.ShowInfo("Community Deleted Successfuly");
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Deletion Failed");
            }
        }

        protected async Task CommunityInfoCallBack()
        {
            JoinedGroups = await GroupService.GetGroupForMemberOrCreator(UXUtility.LoggedInUser.Id);
            RecommendedGroups = await GroupService.GetRecommendedGroupsAsync(UXUtility.LoggedInUser.Id);
            State = LoadingContainerState.Loaded;
            StateHasChanged();
            IsVirtualize = true;
        }

        protected async ValueTask<ItemsProviderResult<Community>> LoadGroupsData(ItemsProviderRequest request)
        {
            return new ItemsProviderResult<Community>(RecommendedGroups.Skip(request.StartIndex).Take(request.Count), RecommendedGroups.Count());
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
    {
       { "Community","/community" }
    };
    }
}
