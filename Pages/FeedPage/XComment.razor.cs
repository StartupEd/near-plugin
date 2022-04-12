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
    public partial class XComment : BaseComponent
    {
        [Parameter] public Comment CommentModel { get; set; }
        [Parameter] public EventCallback<Comment> OnCommentChange { get; set; }
        [Parameter] public EventCallback<Comment> OnDeleteComment { get; set; }

        public CS_User UserClientService { get; set; }
        public CachedUser cachedUser { get; set; }
        public User user { get; set; }

        private CS_Post ClientService { get; set; }

        private bool IsCommentEdit { get; set; } = false;
        private string CommentValue { get; set; }

        protected void EditPostComment()
        {
            IsCommentEdit = true;
            CommentValue = CommentModel.Value;
        }
        protected void CancelEditing()
        {
            IsCommentEdit = false;
            CommentValue = string.Empty;
        }
        private async Task UpdateCommentAsync()
        {
            try
            {
                Comment MyComment = new Comment();
                MyComment = CommentModel;
                MyComment.UpdatedAt = DateTime.UtcNow;
                MyComment.Value = CommentValue;
                await OnCommentChange.InvokeAsync(MyComment);
                CancelEditing();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        private async Task DeletePostComment()
        {
            await OnDeleteComment.InvokeAsync(CommentModel);
        }


        //private async Task myKeyPress(EditContext editContext, KeyboardEventArgs key)
        //{
        //    if (key.CtrlKey && key.Code == @"Enter")
        //    {
        //        if (editContext.Validate())
        //        {

        //        }
        //    }
        //}
        protected override async Task OnInitializedAsync()
        {

            cachedUser = new CachedUser();
            UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            cachedUser = await UserClientService.CacheUser(CommentModel.AuthorId);

        }
    }
}
