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
using StartupEd.UX.One.Services;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace StartupEd.UX.Incubation
{
    public partial class EditCommunity : BaseComponent
    {
        public NavigationManager navigationManager;
        [Parameter] public string Id { get; set; }
        [Parameter] public string GroupName { get; set; }
        //public FileUploader_ClientService<Model_Community> UploaderService { get; set; }
        public List<Select> MemberNameList { get; set; } = new List<Select>();
        private List<User> Connections { get; set; } = new List<User>();
        private CS_User UserService { get; set; }
        private Community GroupInfo { get; set; } = new Community();
        private CS_Community GroupService { get; set; }

        private InputValidation Validation = new InputValidation();
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationCommEditHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //UploaderService = new FileUploader_ClientService<Model_Community>(ClientExchange, EngineServices.MarketNetwork);
                //List<User> MyConnections = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                GroupInfo = await GroupService.GetGroupsByIdAsync(Id);
                List<User> MyConnections = await UserService.GetUserConnectionsListAsync(GroupInfo.Core.CreatorId);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationCommEditHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                if (MyConnections != null && MyConnections.Count != 0)
                {
                    foreach (User user in MyConnections)
                    {
                        if (!GroupInfo.MemberIds.Contains(user.Id))
                        {
                            Connections.Add(user);
                        }
                    }
                }
                await notificationCommEditHubConnection.StartAsync();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
        public bool IsCommEditNotiConnected => notificationCommEditHubConnection.State == HubConnectionState.Connected;
        Task SendCommEditNotification() => notificationCommEditHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Community created");
        private async Task UpdateGroupInformation()
        {
            try
            {
                if (string.IsNullOrEmpty(GroupInfo.Name))
                {
                    //ToastService.ShowWarning("Please Fill Community Name.");
                    return;
                }
                GroupInfo.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "communityDescription");
                if (GroupInfo.Core.Description == "<p><br></p>")
                {
                    //ToastService.ShowWarning("Please Fill Description.");
                    return;
                }
                var data = await JSRuntime.InvokeAsync<Object>("getName", "memberName");
                MemberNameList = JsonConvert.DeserializeObject<List<Select>>(data.ToString());
                foreach (var Name in MemberNameList)
                {
                    GroupInfo.MemberIds.Add(Name.Id);
                }

                HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "UpdateGroup");
                if (response.IsSuccessStatusCode)
                {
                    SLog.Write("Group Updated Successfully..!!");
                    //ToastService.ShowSuccess("Group Info Updated Successfully..!!");
                    foreach (var Name in MemberNameList)
                    {
                        //Notification NotificationModel = new Notification();
                        //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                        //NotificationModel.ReceiverId = Name.Id;
                        //NotificationModel.SentAt = DateTime.UtcNow;
                        //NotificationModel.NotificationType = "Community";
                        //NotificationModel.Message = "You are added in community " + GroupInfo.Name;
                        //NotificationModel.Url = "community/" + GroupInfo.Id + "/" + GroupName;
                        //await NotificationClientService.PostAsync(NotificationModel);
                        //if (IsCommEditNotiConnected) await SendCommEditNotification();
                    }
                    navigationManager.NavigateTo($"/community/{ GroupInfo.Id}/{GroupName}");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public async void Imagebase64(string src)
        {
            //GroupInfo.Core.ImageUrl = await UploaderService.UploadFile(src,GetType());
            StateHasChanged();
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var maxAllowedFiles = 1;
            var format = "image/png";
            foreach (var imageFile in e.GetMultipleFiles(maxAllowedFiles))
            {
                string Ext = imageFile.Name.Substring(imageFile.Name.IndexOf(".") + 1);
                var resizedImageFile = await imageFile.RequestImageFileAsync(format, 500, 500);
                var buffer = new byte[resizedImageFile.Size];
                await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                var data = Ext + "." + Convert.ToBase64String(buffer);
                //GroupInfo.Core.ImageUrl = await UploaderService.UploadFile(data, GetType());
                StateHasChanged();
            }
        }
    }
}
