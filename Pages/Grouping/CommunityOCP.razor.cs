using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class CommunityOCP :BaseComponent
    {
        private int PrivacyCode { get; set; } = 1;
        private string MemberNames { get; set; }
        //public FileUploader_ClientService<Model_Community> UploaderService { get; set; }
        public List<Select> MemberNameList { get; set; } = new List<Select>();
        private List<User> Connections { get; set; } = new List<User>();
        private CS_User UserService { get; set; }

        private Model_Community GroupInfo = new Model_Community();
        private CS_Community GroupService { get; set; }
        private InputValidation Validation = new InputValidation();
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationCommHubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                //UploaderService = new FileUploader_ClientService<Model_Community>(ClientExchange, EngineServices.MarketNetwork);
                List<User> MyConnections = await UserService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationCommHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                if (MyConnections != null && MyConnections.Count != 0)
                {
                    foreach (User user in MyConnections)
                    {
                        if (GroupInfo.MemberIds.Contains(user.Id))
                        {
                            break;
                        }
                        else
                        {
                            Connections.Add(user);
                        }
                    }
                }
                await notificationCommHubConnection.StartAsync();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
        public bool IsCommNotiConnected => notificationCommHubConnection.State == HubConnectionState.Connected;
        Task SendCommNotification() => notificationCommHubConnection.SendAsync("SendNotification", UXUtility.LoggedInUser.Id, "Community created");

        private async Task SubmitGroupInformation()
        {
            try
            {
                if (string.IsNullOrEmpty(GroupInfo.Name))
                {
                    Validation = new InputValidation();
                    Validation.IsEmpty = true;
                    Validation.Message = "Please enter community name.";
                    Validation.InputStyle = "border border-danger";
                    return;
                }
                GroupInfo.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "communityDescription");
                if (GroupInfo.Core.Description == "<p><br></p>" || string.IsNullOrEmpty(GroupInfo.Core.Description))
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
                //GroupInfo.Privacy = (GroupPrivacy)PrivacyCode;
                //string result = await GroupService.PostGroupInfoAsync(GroupInfo);
                //if (!string.IsNullOrEmpty(result))
                //{
                //    SLog.Write("Group Created Successfully..!!");
                //    ToastService.ShowSuccess("Group Created Successfully..!!");

                //    foreach (var Name in MemberNameList)
                //    {
                //        Notification NotificationModel = new Notification();
                //        NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
                //        NotificationModel.ReceiverId = Name.Id;
                //        NotificationModel.SentAt = DateTime.UtcNow;
                //        NotificationModel.NotificationType = "Community";
                //        NotificationModel.Message = "You are addedd in community " + GroupInfo.Name;
                //        NotificationModel.Url = "community/" + result + "/" + GroupInfo.Name;
                //        await NotificationClientService.PostAsync(NotificationModel);
                //        if (IsCommNotiConnected) await SendCommNotification();
                //    }
                //    navigationManager.NavigateTo("/community");
                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public async void Imagebase64(string src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return;
            }
            //GroupInfo.Core.ImageUrl = await UploaderService.UploadFile(src,GetType());
            StateHasChanged();
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
       { "Community","/community" }
    };
    }
}
