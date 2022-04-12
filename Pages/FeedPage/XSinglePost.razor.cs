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
    public partial class XSinglePost : BaseComponent
    {
        //[Parameter]
        //public Post Model { get; set; }
        public Post Model = new Post();
        private CS_Post ClientService { get; set; }
        [Parameter] public string PostId { get; set; }
        public string arg;
        private bool calledit { get; set; } = false;
        [Parameter] public string PinnedColor { get; set; }
        public bool ButtonClick = false;
        [Parameter] public EventCallback CommentsCallBack { get; set; }
        [Parameter] public string UserId { get; set; }
        private bool IsShow { get; set; } = false;
        private HubConnection hubConnection;
        [Parameter] public EventCallback PostCallBack { get; set; }
        private MyConnectionListModel OpenConnectionList { get; set; }
        public bool IsSpConValue { get; set; } = false;
        public Notification NotificationModel = new Notification();
        //private Notification_ClientService NotificationClientService { get; set; }
        private ReactionListModel OpenReactionList { get; set; }
        private HubConnection notificationHubConnectionOnSinglePost;
        [Parameter] public EventCallback ShowReactionModelCallBack { get; set; }
        private async void SubmitComment(string Value)
        {
            if (!string.IsNullOrEmpty(Value))
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
                    await LoadPost();
                }
                await CommentsCallBack.InvokeAsync();
            }
            else
            {
                //ToastService.ShowInfo("Comments can not be blank");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
            //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
            //Model = await ClientService.GetPostByPostId(PostId);

            hubConnection = new HubConnectionBuilder()
            .WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/feedhub")
            .Build();
            notificationHubConnectionOnSinglePost = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
            hubConnection.On("ReceiveMessage", () =>
            {
                PostCallBack.InvokeAsync();
                CallLoadData();
                StateHasChanged();
            });

            await hubConnection.StartAsync();
            await notificationHubConnectionOnSinglePost.StartAsync();
        }
        protected async override Task OnParametersSetAsync()
        {
            await LoadPost();
        }
        private void CallLoadData()
        {
            Task.Run(async () =>
            {
                await LoadPost();
            });
        }
        protected async Task LoadPost()
        {
            Model = await ClientService.GetPostByPostId(PostId);
            StateHasChanged();
        }
        Task SendMessage() => hubConnection.SendAsync("SendMessage");

        public bool IsConnected =>
        hubConnection.State == HubConnectionState.Connected;
        public bool IsNotConnected => notificationHubConnectionOnSinglePost.State == HubConnectionState.Connected;
        Task SendNotification() => notificationHubConnectionOnSinglePost.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Post Notification");
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
            bool result = false;
            result = await ClientService.PiningPost(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                if (IsConnected) await SendMessage();
                //ToastService.ShowInfo("Post Pinned");
                await PostCallBack.InvokeAsync();
                await LoadPost();
            }
            else
            {
                //ToastService.ShowError("Post Not Pinned");
            }
        }

        private async Task DeleteMe()
        {
            bool result = false;
            result = await ClientService.RemovePost(Model.Id);
            if (result == true)
            {
                if (IsConnected) await SendMessage();
                //ToastService.ShowInfo("Post Deleted");
                await PostCallBack.InvokeAsync();
                await LoadPost();
            }
            else
            {
                //ToastService.ShowError("Post Not Deleted");
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
                            //if (IsNotConnected) await SendNotification();
                        }
                        await LoadPost();
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
                    await LoadPost();
                }
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

        private void ShowEdit()
        {
            if (!calledit)
            {
                calledit = true;
            }
            else
            {
                calledit = false;
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
            await LoadPost();
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
                await LoadPost();
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
            //OpenReactionList.ShowModal(Model.Id);
            //ShowReactionModelCallBack.InvokeAsync();
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
                        await LoadPost();
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
    }
}
