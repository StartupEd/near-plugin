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

namespace StartupEd.UX.Incubation.SuperAdmin.Components.Feeds
{
    public partial class PostCard : BaseComponent
    {
        [Parameter] public Post Model { get; set; }
        [Parameter] public EventCallback PostCallBack { get; set; }
        [Parameter] public EventCallback<string> ShowReactionModel { get; set; }

        private OneUX_SweetAlertBox SweetAlertConfirmation { get; set; }
        private CS_Post ClientService { get; set; }


        private bool IsCommentsShow { get; set; } = false;

        protected void ShowSweetAlertModal()
        {
            SweetAlertConfirmation.ShowSweetAlert();
        }

        protected void CallReactionListModel()
        {
            ShowReactionModel.InvokeAsync(Model.Id);
        }

        protected void ShowComments()
        {
            if (!IsCommentsShow)
            {
                IsCommentsShow = true;
            }
            else
            {
                IsCommentsShow = false;
            }
        }

        private async Task UpdatePostAsync(Post Model)
        {
            try
            {
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                bool result = false;
                result = await ClientService.EditPost(Model);
                if (result == true)
                {
                    await PostCallBack.InvokeAsync();
                    ToastService.ShowInfo("Post Has Been Updated..!");
                }
                else
                {
                    throw new Exception("Exception: Post Not Deleted..");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        private async Task DeletePostAsync(bool IsDelete)
        {
            try
            {
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                if (IsDelete)
                {
                    bool result = false;
                    result = await ClientService.RemovePost(Model.Id);
                    if (result == true)
                    {
                        await PostCallBack.InvokeAsync();
                        ToastService.ShowInfo("Post has been deleted!");
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
    }
}
