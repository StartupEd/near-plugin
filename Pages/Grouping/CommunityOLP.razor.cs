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
using Microsoft.AspNetCore.SignalR.Client;


namespace StartupEd.UX.Incubation
{
    public partial class CommunityOLP : BaseComponent
    {
        public NavigationManager navigationManager;
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        [Parameter] public string GroupId { get; set; }
        [Parameter] public string GroupName { get; set; }
        private bool IsVirtualize { get; set; } = false;
        private int PostCount { get; set; } = 0;
        private string deleteGroupId { get; set; }
        private Community GroupDetail { get; set; } = new Community();
        private CS_Community GroupService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        private List<UserFullInfo> CommunityAllMembers { get; set; } = new List<UserFullInfo>();
        private CS_User UserService { get; set; }

        private Post GroupPost { get; set; } = new Post();
        private CS_Post PostService { get; set; }
        private List<User> UserConnections { get; set; } = new List<User>();
        //private Notification_ClientService NotificationClientService { get; set; }

        private HubConnection communityPosthubConnection;
        private HubConnection notificationcommunityPosthubConnection;

        protected enum LoadingContainerState { Loading, Loaded, Error }
        protected LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                PostService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);

                communityPosthubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub").Build();
                notificationcommunityPosthubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await notificationcommunityPosthubConnection.StartAsync();
                await communityPosthubConnection.StartAsync();

                GroupDetail = await GroupService.GetGroupsByIdAsync(GroupId);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
                if (GroupDetail.MemberIds != null)
                {
                    CommunityAllMembers = await UserService.GetUserAndProfileForListOfUserIds(GroupDetail.MemberIds);
                }
            }
            catch (NullReferenceException ex)
            {
                SLog.Write(ex.Message);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadGroupDetailsAsync(string groupId)
        {
            GroupDetail = await GroupService.GetGroupsByIdAsync(groupId);
            if (GroupDetail.MemberIds != null)
            {
                CommunityAllMembers = await UserService.GetUserAndProfileForListOfUserIds(GroupDetail.MemberIds);
            }
            IsVirtualize = true;
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected void TotalPostCounts(int value)
        {
            if (value >= 0)
            {
                PostCount = value;
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

        protected async Task DeleteCommunity(bool value)
        {
            try
            {
                if (value)
                {
                    GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                    HttpResponseMessage message = await GroupService.DeleteAsync(deleteGroupId);
                    if (message.IsSuccessStatusCode)
                    {
                        SLog.Write("Communty has been deleted!");
                        //ToastService.ShowInfo("Community Deleted Successfuly");
                        navigationManager.NavigateTo("/community");
                    }
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Deletion Failed");
            }
        }

        private async Task JoinPublicGroup(string groupId)
        {
            try
            {
                Community GroupInfo = new Community();
                GroupInfo.Id = groupId;
                HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "JoinGroup");
                if (response.IsSuccessStatusCode)
                {
                    await LoadGroupDetailsAsync(groupId);
                    //ToastService.ShowSuccess("Group Joined Successfully!");
                    SLog.Write("User has joined group.." + groupId);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
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
                        await LoadGroupDetailsAsync(groupId);
                        SLog.Write("User has leaved group.." + groupId);
                        //ToastService.ShowInfo("You have left community!");
                    }
                }
                else
                {
                    throw new Exception("Exception: Not Able To Leave Community!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        private async Task RemoveMemberFromGroup(string memberId)
        {
            try
            {
                Community GroupInfo = new Community();
                GroupInfo.Id = GroupId;
                GroupInfo.Core.Id = GroupId;
                GroupInfo.MemberIds.Add(memberId);
                HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "RemoveMember");
                if (response.IsSuccessStatusCode)
                {
                    await LoadGroupDetailsAsync(GroupId);
                    //ToastService.ShowInfo("Member Removed from Group..!");
                    SLog.Write("Member has removed from group.." + memberId);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async ValueTask<ItemsProviderResult<UserFullInfo>> LoadGroupsData(ItemsProviderRequest request)
        {
            return new ItemsProviderResult<UserFullInfo>(CommunityAllMembers.Skip(request.StartIndex).Take(request.Count), CommunityAllMembers.Count());
        }

        private void EditGroupInfo()
        {
            navigationManager.NavigateTo("community/" + GroupId + "/" + GroupName + "/edit");
        }


        private async Task SubmitGroupPost()
        {
            try
            {
                GroupPost.AuthorId = UXUtility.LoggedInUser.Id;
                GroupPost.Name = UXUtility.LoggedInUser.Name;
                GroupPost.GroupId = GroupId;
                GroupPost.IsCommunityPost = true;
                GroupPost.Summary = await JSRuntime.InvokeAsync<string>("getPostBoxValue", "ideaComposer_editor");
                if (GroupPost.Summary != "<p><br></p>")
                {
                    HttpResponseMessage response = await PostService.PostAsync(GroupPost);
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await SendMessage();
                        //ToastService.ShowSuccess("Post has been submitted.");

                        foreach (var MemberInfo in CommunityAllMembers)
                        {
                            if (MemberInfo.UserModel.Id != UXUtility.LoggedInUser.Id)
                            {
                                //Notification NotificationModel = new Notification();
                                //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                                //NotificationModel.ReceiverId = MemberInfo.UserModel.Id;
                                //NotificationModel.SentAt = DateTime.UtcNow;
                                //NotificationModel.NotificationType = "community";
                                //NotificationModel.Message = UXUtility.LoggedInUser.Name + " has posted on Community " + GroupDetail.Name;
                                //NotificationModel.Url = "community/detail/" + GroupId;
                                //await NotificationClientService.PostAsync(NotificationModel);
                                if (IsNotiConnected) await SendFeedNotification();
                            }
                        }

                        UserActivity userActivity = new UserActivity();
                        userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                        //userActivity.Activities.Add(new Activity{ FeatureId = GroupId, Feature = GroupDetail.Name, Description = "You have posted in community " + GroupDetail.Name });
                        //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                    }
                    else
                    {
                        SLog.Write("Posting Failed");
                        //ToastService.ShowError(response.ReasonPhrase, "Post Failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowError(ex.Message, "Post Failed Exception");
            }
        }

        public bool IsConnected => communityPosthubConnection.State == HubConnectionState.Connected;
        Task SendMessage() => communityPosthubConnection.SendAsync("SendMessage");

        public bool IsNotiConnected => notificationcommunityPosthubConnection.State == HubConnectionState.Connected;
        Task SendFeedNotification() => notificationcommunityPosthubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Feed");

        public void Dispose()
        {
            _ = communityPosthubConnection.DisposeAsync();
        }
        public void DisposeNotification()
        {
            _ = notificationcommunityPosthubConnection.DisposeAsync();
        }
    }
}
