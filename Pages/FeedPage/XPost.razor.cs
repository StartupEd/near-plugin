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
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class XPost : BaseComponent
    {
        [Parameter] public string UserId { get; set; }
        [Parameter] public Post Model { get; set; }
        [Parameter] public EventCallback<string> ShowReactionModelCallBack { get; set; }
        [Parameter] public EventCallback CommentsCallBack { get; set; }
        [Parameter] public EventCallback PostCallBack { get; set; }
        [Parameter] public CachedUser Cacheduser { get; set; }


        private MyConnectionListModel OpenConnectionList { get; set; }

        private OneUX_SweetAlertBox SweetAlertConfirmation { get; set; }

        private CS_Post ClientService { get; set; }
        //private Notification_ClientService NotificationClientService { get; set; }
        //public Notification NotificationModel = new Notification();
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        private CS_User CS_User { get; set; }

        public bool ButtonClick = false;
        private bool IsShow { get; set; } = false;

        private HubConnection hubConnection;
        private HubConnection notificationPostHubConnection;


        protected void ShowSweetAlertModal()
        {
            SweetAlertConfirmation.ShowSweetAlert();
        }

        private async void SubmitComment(string Value)
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                SLog.Write("Comment = " + Value + " Sumbitted");
                Model.Comments.Clear();
                Comment c = new Comment();
                c.AuthorId = UXUtility.LoggedInUser.Id;
                c.Value = Value;
                c.CommentedAt = DateTime.UtcNow;
                Model.Comments.Add(c);
                if (await ClientService.AddComment(Model, Model.Id))
                {
                    if (IsConnected) await SendMessage();
                    if (UXUtility.LoggedInUser.Id != Model.Core.CreatorId)
                    {
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = Model.Core.CreatorId;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Comments";
                        //NotificationModel.Message = UXUtility.LoggedInUser.Name + " Commented on Your Post";
                        //NotificationModel.Url = "post/" + Model.Id;
                        //await NotificationClientService.PostAsync(NotificationModel);
                        //if (IsNotConnected) await SendNotification();
                    }

                    if (string.IsNullOrEmpty(Model.GroupId) && Model.IsCommunityPost == false)
                    {
                        string description = "";
                        if (Model.Core.CreatorId == UXUtility.LoggedInUser.Id)
                        {
                            description = "Commented On Own Post: " + Value;
                        }
                        else
                        {
                            description = "Commented On " + Model.Name + "'s Post: " + Value;
                        }
                        await PostUserActivity(description);
                    }
                    else
                    {
                        await CommunityPostUserActivity(Model.GroupId, "You have Commented In Community Post.");
                    }

                }
                await CommentsCallBack.InvokeAsync();
            }
            else
            {
                //ToastService.ShowInfo("Comment doesn't allow blank or whitespace");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
            ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
            CS_User = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
            //cachedUser = new CachedUser();
            //cachedUser = await CS_User.CacheUser(Model.Core.CreatorId);

            int count = Model.Reactions.Count;
            SLog.Write("Post Loaded - " + count);
            hubConnection = new HubConnectionBuilder()
                .WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub")
                .Build();
            notificationPostHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
            hubConnection.On("ReceiveMessage", () =>
            {
                PostCallBack.InvokeAsync();
                StateHasChanged();
            });

            await hubConnection.StartAsync();
            await notificationPostHubConnection.StartAsync();

        }
        protected override async Task OnParametersSetAsync()
        {



        }
        Task SendMessage() => hubConnection.SendAsync("SendMessage");

        public bool IsConnected =>
        hubConnection.State == HubConnectionState.Connected;
        public bool IsNotConnected => notificationPostHubConnection.State == HubConnectionState.Connected;
        Task SendNotification() => notificationPostHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Post Notification");

        private async Task EditMe(Post Model)
        {
            bool result = false;
            result = await ClientService.EditPost(Model);
            if (result == true)
            {
                //ToastService.ShowInfo("Post Updated");
                await PostCallBack.InvokeAsync();
            }
            else
            {
                //ToastService.ShowError("Post Not Updated");
            }
        }

        private async Task PinMe()
        {
            StateHasChanged();
            bool result = false;
            result = await ClientService.PiningPost(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                if (string.IsNullOrEmpty(Model.GroupId) && Model.IsCommunityPost == false)
                {
                    string description = "";
                    if (Model.Core.CreatorId == UXUtility.LoggedInUser.Id)
                    {
                        description = "You have pinned a post. ";
                    }
                    else
                    {
                        description = "You have pinned " + Model.Name + "'s Post.";
                    }
                    await PostUserActivity(description);
                }
                else
                {
                    await CommunityPostUserActivity(Model.GroupId, "You have pinned a post in Community.");
                }

                if (IsConnected) await SendMessage();
                //ToastService.ShowInfo("Post Pinned");
                await PostCallBack.InvokeAsync();
            }
            else
            {
                //ToastService.ShowError("Post Not Pinned");
            }
        }

        private async Task DeleteMe(bool IsDelete)
        {
            try
            {
                if (IsDelete)
                {
                    bool result = false;
                    result = await ClientService.RemovePost(Model.Id);
                    if (result == true)
                    {
                        if (IsConnected) await SendMessage();
                        await PostCallBack.InvokeAsync();
                        //ToastService.ShowInfo("Post has been deleted!");
                    }
                    else
                    {
                        throw new Exception("Exception: Post Not Deleted..");
                    }
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task LikeMe(ReactionType reactionType)
        {
            if (reactionType != null)
            {
                if (Model.Reactions != null && Model.Reactions.Any(item => item.AuthorId == UXUtility.LoggedInUser.Id))
                {
                    ButtonClick = true;
                }
                if (!ButtonClick)
                {
                    //Reaction<Post> r1 = new Reaction<Post>();
                    //r1 = r1.Create(reactionType, UXUtility.LoggedInUser.Id);
                    //Model.Reactions.Add(r1);
                    if (await ClientService.AddReactions(Model, UXUtility.LoggedInUser.Id))
                    {
                        if (IsConnected) await SendMessage();
                        if (UXUtility.LoggedInUser.Id != Model.Core.CreatorId)
                        {
                            //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                            //NotificationModel.ReceiverId = Model.Core.CreatorId;
                            //NotificationModel.SentAt = DateTime.UtcNow;
                            //NotificationModel.NotificationType = "Reaction";
                            //NotificationModel.Message = "Your Post is Liked By " + UXUtility.LoggedInUser.Name;
                            //NotificationModel.Url = "post/" + Model.Id;
                            //await NotificationClientService.PostAsync(NotificationModel);
                            if (IsNotConnected) await SendNotification();
                        }
                        if (string.IsNullOrEmpty(Model.GroupId) && Model.IsCommunityPost == false)
                        {
                            string description = "";
                            if (Model.Core.CreatorId == UXUtility.LoggedInUser.Id)
                            {
                                description = "You have liked your own post on Feed";
                            }
                            else
                            {
                                description = Model.Name + "'s Post has been liked by you.";
                            }
                            await PostUserActivity(description);
                        }
                        else
                        {
                            await CommunityPostUserActivity(Model.GroupId, "You have liked a post in Community.");
                        }
                    }
                    await PostCallBack.InvokeAsync();
                }
                else
                {
                    //one Copy Cut
                    var rct = Model.Reactions.Find(x => x.AuthorId == UXUtility.LoggedInUser.Id);

                    if (rct.IsLiked && (rct.Value == reactionType))
                    {
                        //rct = rct.Remove(rct);
                        //Model.Reactions.Where(r => r.Id == rct.Id).Select(r => { r = rct; return r; }).ToList();
                    }
                    else
                    {
                        //rct = rct.Update(rct, reactionType);
                        //Model.Reactions.Where(r => r.Id == rct.Id).Select(r => { r = rct; return r; }).ToList();
                    }
                    if (await ClientService.RemoveReactions(Model, UXUtility.LoggedInUser.Id))
                    {
                        if (IsConnected) await SendMessage();
                    }
                    await PostCallBack.InvokeAsync();
                }
            }
            else
            {
                SLog.Write("Reaction is Empty!!!");
            }
        }


        protected async Task PostCommentUpdateAsync(Comment Comment)
        {
            try
            {
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                if (Comment != null)
                {
                    Post PostModel = new Post();
                    PostModel.Id = Model.Id;
                    PostModel.Core.Id = Model.Core.Id;
                    PostModel.Comments.Add(Comment);
                    HttpResponseMessage response = await ClientService.PutAsync(PostModel, "UpdateComment");
                    if (response.IsSuccessStatusCode)
                    {
                        await PostCallBack.InvokeAsync();
                        //ToastService.ShowSuccess("Comment Updated Successfully");
                    }
                    else
                    {
                        throw new Exception("Exception: Not able to update comment.");
                    }
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task PostCommentDeleteAsync(Comment Comment)
        {
            try
            {
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                if (Comment != null)
                {
                    Post PostModel = new Post();
                    PostModel.Id = Model.Id;
                    PostModel.Core.Id = Model.Core.Id;
                    PostModel.Comments.Add(Comment);
                    HttpResponseMessage response = await ClientService.PutAsync(PostModel, "DeleteComment");
                    if (response.IsSuccessStatusCode)
                    {
                        await PostCallBack.InvokeAsync();
                        //ToastService.ShowSuccess("Comment Deleted Successfully");
                    }
                    else
                    {
                        throw new Exception("Exception: Not able to Delete comment.");
                    }
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        private void ShowComments()
        {
            if (!IsShow)
            {
                IsShow = true;
            }
            else
            {
                IsShow = false;
            }
        }


        private async Task SetPrivacy(string Value)
        {
            if (Value == "OnlyMe")
            {
                Model.IsVisibleMe = true;
                Model.IsPublic = false;
                Model.IsMyConnection = false;
                Model.IsMySpecificConnection = false;
                Model.SpecificConnectionIds = null;
            }
            if (Value == "MyConnections")
            {
                Model.IsVisibleMe = false;
                Model.IsPublic = false;
                Model.IsMyConnection = true;
                Model.IsMySpecificConnection = false;
                Model.SpecificConnectionIds = null;
            }
            if (Value == "Public")
            {
                Model.IsVisibleMe = false;
                Model.IsPublic = true;
                Model.IsMyConnection = false;
                Model.IsMySpecificConnection = false;
                Model.SpecificConnectionIds = null;
            }
            await ClientService.SetPrivacy(Model);
            await PostCallBack.InvokeAsync();
        }

        private async Task UnPinnedMe()
        {
            bool result = false;
            result = await ClientService.UnPiningPost(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                if (IsConnected) await SendMessage();
                //ToastService.ShowInfo("Post UnPinned");
                await PostCallBack.InvokeAsync();
            }
            else
            {
                //ToastService.ShowError("Post Not UnPinned");
            }
        }

        protected void CallConnectionListtModal()
        {
            OpenConnectionList.ShowModal();
        }

        protected async Task SetSpecificConnRights()
        {
            await PostCallBack.InvokeAsync();
        }

        protected void CallReactionListModel()
        {
            ShowReactionModelCallBack.InvokeAsync(Model.Id);
        }

        protected async Task PostUserActivity(string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Feed", Model.Id, "Feed Post", description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }

        protected async Task CommunityPostUserActivity(string communityId, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Community", communityId, "Community Post", description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
        //public async Task LoadCacheForUser()
        //{
        //    foreach (var Item in Model.Comments)
        //    {
        //        if (!cachedUsers.Any(cu => cu.UserId == Item.AuthorId))
        //        {
        //            CachedUser cachedUser = await CS_User.CacheUser(Item.AuthorId);
        //            cachedUsers.Add(cachedUser);
        //        }
        //    }
        //    State = LoadingContainerState.Loaded;
        //    StateHasChanged();
        //}
    }
}
