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
using Newtonsoft.Json;
using StartupEd.UX.One.Shared_Classes;
using System.Net.Http;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BlogPost
{
    public partial class BlogCard : BaseComponent
    {
        [Parameter]
        public Blog Model { get; set; }
        [Parameter]
        public EventCallback<string> GetId { get; set; }
        public Profile Profile = new Profile();
        public CS_Profile Sevice { get; set; }
        public CS_Blog ModelService { get; set; }
        [Parameter] public EventCallback<string> ShowReactionModalCallBack { get; set; }
        [Parameter] public EventCallback UpdateBlogPosts { get; set; }
        [Parameter] public EventCallback<Model_Blog> AddBookmark { get; set; }
        [Parameter] public EventCallback<Model_Blog> RemoveBookmark { get; set; }
        public bool ButtonClick = false;
        string Post { get; set; }
        private bool IsShow { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_Blog(ClientExchange, EngineServices.MarketNetwork);
                Sevice = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                Profile = await Sevice.GetProfileByUserId(Model.Core.CreatorId);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                ToastService.ShowError(ex.Message, "Exception");
            }
        }

        public void HandleClick(string blogName, string blogId)
        {
            navigationManager.NavigateTo($"blog/{blogId}/{blogName}");
        }

        protected void CallShowSweetAlertDeleteBlog(string Id)
        {
            GetId.InvokeAsync(Id);
        }

        protected async Task LikeMe(string Reaction)
        {
            if (Reaction != "")
            {
                if (Model.Reactions != null && Model.Reactions.Any(item => item.AuthorId == UXUtility.LoggedInUser.Id))
                {
                    ButtonClick = true;
                }
                if (!ButtonClick)
                {
                    Reaction r1 = new Reaction();
                    if (Reaction == "Love")
                    {
                        r1.Value = ReactionType.Love;
                    }
                    if (Reaction == "Insight")
                    {
                        r1.Value = ReactionType.Insight;
                    }
                    if (Reaction == "Dollar")
                    {
                        r1.Value = ReactionType.Dollar;
                    }
                    if (Reaction == "Rocket")
                    {
                        r1.Value = ReactionType.Rocket;
                    }
                    if (Reaction == "StartupEd")
                    {
                        r1.Value = ReactionType.StartupEd;
                    }
                    r1.AuthorId = UXUtility.LoggedInUser.Id;
                    r1.ReactedAt = Model.Core.CreatedAt.Date;
                    r1.IsLiked = true;
                    r1.AuthorName = UXUtility.LoggedInUser.Name;
                    Model.Reactions.Add(r1);
                    //if (await ModelService.AddReactions(Model))
                    //{
                    //    //if (IsConnected) await SendMessage();
                    //    if (UXUtility.LoggedInUser.Id != Model.Core.CreatorId)
                    //    {
                    //        NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //        NotificationModel.ReceiverId = Model.AuthorId;
                    //        NotificationModel.SentAt = DateTime.UtcNow;
                    //        NotificationModel.NotificationType = "Reaction";
                    //        NotificationModel.Message = "Your Post is Liked By " + UXUtility.LoggedInUser.Name;
                    //        NotificationModel.Url = "post/" + Model.Id;
                    //        await NotificationClientService.PostAsync(NotificationModel);
                    //        if (IsNotConnected) await SendNotification();
                    //    }

                    //}
                    HttpResponseMessage responseMessage = await ModelService.PutAsync(Model);
                    await UpdateBlogPosts.InvokeAsync();
                }
                else
                {
                    var rct = Model.Reactions.Find(x => x.AuthorId == UXUtility.LoggedInUser.Id);
                    Model.Reactions.Remove(rct);
                    if (rct.IsLiked && (rct.Value.ToString() == Reaction))
                        rct.IsLiked = false;
                    else
                        rct.IsLiked = true;
                    if (Reaction == "Love")
                    {
                        rct.Value = ReactionType.Love;
                    }
                    if (Reaction == "Insight")
                    {
                        rct.Value = ReactionType.Insight;
                    }
                    if (Reaction == "Dollar")
                    {
                        rct.Value = ReactionType.Dollar;
                    }
                    if (Reaction == "Rocket")
                    {
                        rct.Value = ReactionType.Rocket;
                    }
                    if (Reaction == "StartupEd")
                    {
                        rct.Value = ReactionType.StartupEd;
                    }

                    Model.Reactions.Add(rct);
                    HttpResponseMessage responseMessage = await ModelService.PutAsync(Model);
                    //if (await ClientService.RemoveReactions(Model)
                    //{
                    //    if (IsConnected) await SendMessage();
                    //}
                    await UpdateBlogPosts.InvokeAsync();
                }
            }

        }

        protected void CallReactionListModal()
        {
            ShowReactionModalCallBack.InvokeAsync(Model.Id);
        }

        private async Task BookmarkBlog()
        {
            await AddBookmark.InvokeAsync(Model);
        }

        private async Task UnbookmarkBlog()
        {
            await RemoveBookmark.InvokeAsync(Model);
        }

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
                c.AuthorName = UXUtility.LoggedInUser.Name;
                Model.Comments.Add(c);
                if (await ModelService.AddComment(Model))
                {
                    //if (IsConnected) await SendMessage();
                    //if (UXUtility.LoggedInUser.Id != Model.Core.CreatorId)
                    //{
                    //    NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                    //    NotificationModel.ReceiverId = Model.AuthorId;
                    //    NotificationModel.SentAt = DateTime.UtcNow;
                    //    NotificationModel.NotificationType = "Comments";
                    //    NotificationModel.Message = UXUtility.LoggedInUser.Name + " Commented on Your Post";
                    //    NotificationModel.Url = "post/" + Model.Id;
                    //    await NotificationClientService.PostAsync(NotificationModel);
                    //    if (IsNotConnected) await SendNotification();
                    //}

                }
                await UpdateBlogPosts.InvokeAsync();
            }
            else
            {
                ToastService.ShowInfo("Comments can not be blank");
            }
        }

        protected async Task CommentUpdateAsync(Comment Comment)
        {
            try
            {
                if (Comment != null)
                {
                    ModelService = new CS_Blog(ClientExchange,EngineServices.MarketNetwork);
                    bool result = await ModelService.UpdateComment(Model, Comment.Id, Comment.Value);
                    if (result)
                    {
                        await UpdateBlogPosts.InvokeAsync();
                        ToastService.ShowSuccess("Comment Updated Successfully");
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

        protected async Task CommentDeleteAsync(Comment Comment)
        {
            try
            {
                if (Comment != null)
                {
                    //Comment.IsDeleted = true;
                    ModelService = new CS_Blog(ClientExchange, EngineServices.MarketNetwork);
                    bool result = await ModelService.DeleteComment(Model, Comment.Id);
                    if (result)
                    {
                        await UpdateBlogPosts.InvokeAsync();
                        ToastService.ShowSuccess("Comment Deleted Successfully");
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
    }
}
