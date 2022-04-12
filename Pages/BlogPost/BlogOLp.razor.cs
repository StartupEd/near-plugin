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
    public partial class BlogOLp : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string BlogName { get; set; }
        public Blog Model = new Blog();
        public CS_Blog ClientService { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        private BlogReactionListModal OpenReactionList { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public bool ButtonClick = false;
        private bool IsShow { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                BlogName = BlogName.Replace(" ", "-");
                await SyncLoggedInUserStatus();
                ClientService = new CS_Blog(ClientExchange,EngineServices.MarketNetwork);
               // Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                await LoadData();
                var UserId = UXUtility.LoggedInUser.Id;
                try
                {
                    var Date = DateTime.UtcNow.ToLocalTime().ToString("dd MMM yy");
                    if (!Model.BlogViewCounter.ContainsKey(Date))
                    {
                        List<string> UserIds = new List<string>();
                        UserIds.Add(UserId);
                        Model.BlogViewCounter.Add(Date, UserIds);
                    }
                    else
                    {
                        if (!Model.BlogViewCounter[Date].Contains(UserId))
                        {
                            Model.BlogViewCounter[Date].Add(UserId);
                        }
                        else
                        {
                            return;
                        }
                    }
                    HttpResponseMessage response = await ClientService.PutAsync(Model);

                    if (response.IsSuccessStatusCode)
                    {

                        SLog.Write("Success");
                    }
                    else
                    {
                        SLog.Write("Blog View Error");
                    }
                }
                catch (Exception ex)
                {
                    SLog.Write(ex.Message);
                }
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }

        public async Task LoadData()
        {
            if (Id != null)
            {
                Model = await ClientService.GetBlogById(Id);
            }
            StateHasChanged();
        }

        protected void ShowSweetAlert()
        {
            ConfirmSweetAlert.ShowSweetAlert();
        }

        private async Task Bookmark()
        {
            // Bookmark feature 
            //bool result = false;
            //BookmarkedObject<Blog> obj = new BookmarkedObject<Blog>();
            //obj = obj.BookMarking(UXUtility.LoggedInUser.Id, Model.Id);
            //Model.BookmarkedObjectUserIds.Add(obj);
            //result = await ClientService.BookmarkBlogById(Model, UXUtility.LoggedInUser.Id);
            //if (result == true)
            //{
            //    await BlogsRecentActivity("You have bookmarked a Blog " + Model.Name);
            //    ToastService.ShowInfo("Blog Pinned");
            //    await LoadData();
                //await PostCallBack.InvokeAsync();
            //}
            //else
            //{
            //    ToastService.ShowError("Blog Not Pinned");
            //}
        }

        private async Task Unbookmark()
        {
            bool result = false;

            result = await ClientService.UnBookmarkBlogById(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                ToastService.ShowInfo("Blog UnPinned");
                await LoadData();
                //await PostCallBack.InvokeAsync();
            }
            else
            {
                ToastService.ShowError("Blog Not UnPinned");
            }
        }

        public async void DeleteBlog()
        {
            try
            {
                HttpResponseMessage message = await ClientService.DeleteAsync(Model.Id);
                if (message.IsSuccessStatusCode)
                {
                    await BlogsRecentActivity("You have deleted Blog " + Model.Name);

                    navigationManager.NavigateTo("/blog");
                    ToastService.ShowSuccess("Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message, "Exception");
            }
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
                    HttpResponseMessage responseMessage = await ClientService.PutAsync(Model);
                    //await UpdateBlogPosts.InvokeAsync();

                    await BlogsRecentActivity("You have recently liked a Blog " + Model.Name);
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
                    HttpResponseMessage responseMessage = await ClientService.PutAsync(Model);
                    //if (await ClientService.RemoveReactions(Model)
                    //{
                    //    if (IsConnected) await SendMessage();
                    //}
                    //await UpdateBlogPosts.InvokeAsync();
                }
            }

        }

        protected void CallReactionListModal()
        {
            //OpenReactionList.ShowModal();
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
                if (await ClientService.AddComment(Model))
                {
                    await BlogsRecentActivity("You have recently commented on Blog " + Model.Name);
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
                await LoadData();
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
                    bool result = await ClientService.UpdateComment(Model, Comment.Id, Comment.Value);
                    if (result)
                    {
                        await LoadData();
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
                    bool result = await ClientService.DeleteComment(Model, Comment.Id);
                    if (result)
                    {
                        await LoadData();
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

        protected async Task BlogsRecentActivity(string description)
        {
            //UserActivity userActivity = new UserActivity();
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(new Model_Activity { FeatureId = Model.Id, Feature = Model.Name, SubModuleId = "", Description = description });
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
