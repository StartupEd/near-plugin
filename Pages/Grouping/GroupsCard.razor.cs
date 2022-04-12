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
    public partial class GroupsCard
    {
        public NavigationManager navigationManager;
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        //private FileUploader_ClientService<Model_Profile> UploaderService { get; set; }
        [Parameter] public Model_Community GroupDetail { get; set; } = new Model_Community();
        [Parameter] public EventCallback<string> OnButtonClick { get; set; }
        private CS_Community GroupService { get; set; }
        private string deleteGroupId { get; set; }


        public async void Imagebase64(string src)
        {
            //GroupDetail.Core.ImageUrl = await UploaderService.UploadFile(src,GetType());
            //StateHasChanged();
        }

        private void EditGroupInfo()
        {
            navigationManager.NavigateTo("community/" + GroupDetail.Id + "/" + GroupDetail.Name + "/edit");
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
                    //GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                    HttpResponseMessage message = await GroupService.DeleteAsync(deleteGroupId);
                    if (message.IsSuccessStatusCode)
                    {
                        await OnButtonClick.InvokeAsync(deleteGroupId);
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

        private async Task JoinCommunityAsync(string groupId)
        {
            try
            {
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                if (!string.IsNullOrEmpty(groupId))
                {
                    Community GroupInfo = new Community();
                    GroupInfo.Id = groupId;
                    HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "JoinGroup");
                    if (response.IsSuccessStatusCode)
                    {
                        await OnButtonClick.InvokeAsync(groupId);
                        SLog.Write("User has joined group.." + groupId);
                    }
                }
                else
                {
                    throw new Exception("Exception: User Couldn't Joined Community!");
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
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                if (!string.IsNullOrEmpty(groupId))
                {
                    Community GroupInfo = new Community();
                    GroupInfo.Id = groupId;
                    HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "LeaveGroup");
                    if (response.IsSuccessStatusCode)
                    {
                        await OnButtonClick.InvokeAsync(groupId);
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
    }
}
