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

namespace StartupEd.UX.Incubation
{
    public partial class ProfileTemplate : BaseComponent
    {
        [Parameter] public string UserId { get; set; }
        [Parameter] public string ProfileUrl { get; set; }
        private Profile ProfileDetails = new Profile();
        private User UserDetails = new User();
        private List<Community> MyGroups { get; set; }
        private List<Event> AllEvent { get; set; } = new List<Event>();
        private List<IncubationProgram> IPList { get; set; } = new List<IncubationProgram>();
        private List<BusinessPage> Pages { get; set; } = new List<BusinessPage>();
        private List<ConnectionRequest> ConnectionList = new List<ConnectionRequest>();

        private CS_IncubationProgram ProgramService { get; set; }
        private CS_Event EventService { get; set; }
        private CS_Community GroupService { get; set; }
        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }
        private CS_BusinessPage PageService { get; set; }
        private CS_ConnectionRequest ConnectionService { get; set; }
        public DateTime LocalDate = DateTime.UtcNow;
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private string SearchString { get; set; }

        public bool Error { get; set; } = false;

        private string ConnectButton { get; set; } = "Connect";
        public int i = 0, b;
        //private Notification_ClientService NotificationClientService { get; set; }
        private HubConnection notificationProfileViewHubConnection;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                ProgramService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                PageService = new CS_BusinessPage(ClientExchange, EngineServices.MarketNetwork);
                //NotificationClientService = new Notification_ClientService(ClientExchange, EngineServices.MarketNetwork);
                notificationProfileViewHubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/notificationhub").Build();
                await SyncLoggedInUserStatus();
                if (ProfileUrl != null)
                {
                    ProfileDetails = await ProfileService.GetDefaultProfileByProfileUrl(ProfileUrl);
                    if (ProfileDetails != null)
                    {
                        UserId = ProfileDetails.Core.CreatorId;
                    }
                    else
                    {
                        Error = true;
                    }
                }
                await notificationProfileViewHubConnection.StartAsync();
                if (UserId != UXUtility.LoggedInUser.Id)
                {
                    await SendProfileViewNotification();
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public bool IsProViewNotiConnected => notificationProfileViewHubConnection.State == HubConnectionState.Connected;
        Task SendProviewNotification() => notificationProfileViewHubConnection.SendAsync("SendNotification", UserId, "Your profile viewed");

        private async Task SendProfileViewNotification()
        {
            //Notification NotificationModel = new Notification();
            //NotificationModel.SenderId = UXUtility.LoggedInUser.Id;
            //NotificationModel.ReceiverId = UserId;
            //NotificationModel.SentAt = DateTime.UtcNow;
            //NotificationModel.NotificationType = "Profile view";
            //NotificationModel.Message = UXUtility.LoggedInUser.Name + " viewed your profile";
            //NotificationModel.Url = "profile/" + UXUtility.LoggedInUser.Id;
            //await NotificationClientService.PostAsync(NotificationModel);
            //NotificationService notificationService = new NotificationService(ClientExchange, EngineServices.MarketNetwork);
            //await notificationService.SendAsync(NotificationModel);
            if (IsProViewNotiConnected) await SendProviewNotification();
        }

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                var id = UXUtility.LoggedInUser.Id;
                ProfileDetails = await ProfileService.GetProfileByUserId(UserId);
                Pages = await PageService.GetBusinessPageByUserId(UserId);
                UserDetails = await UserService.GetUserById(UserId);
                AllEvent = await EventService.GetAllEventsByUserId(UserId);
                MyGroups = await GroupService.GetGroupForMemberOrCreator(UserId);
                IPList = await ProgramService.GetIncubationProgramByUserId(UserId);
                ConnectionList = await ConnectionService.GetAllConnectionRequestsForUser(id);

                if (ConnectionList != null && ConnectionList.Count != 0 && UserDetails != null)
                {
                    foreach (var Request in ConnectionList)
                    {
                        if (Request.FromUserId == UXUtility.LoggedInUser.Id && Request.ToUserId == UserDetails.Core.Id && Request.Status == ConnectionStatus.Created)
                        {
                            ConnectButton = "Cancel Request";
                        }
                        else if (Request.FromUserId == UserDetails.Core.Id && Request.ToUserId == UXUtility.LoggedInUser.Id && Request.Status == ConnectionStatus.Created)
                        {
                            ConnectButton = "Confirm Request";
                        }
                    }
                }
                State = LoadingContainerState.Loaded;
                StateHasChanged();
                if (id != UserId)
                {
                    try
                    {
                        var Date = DateTime.UtcNow.ToLocalTime().ToString("dd MMM yy");
                        if (!ProfileDetails.ProfileViews.ContainsKey(Date))
                        {
                            List<string> UserIds = new List<string>();
                            UserIds.Add(id);
                            ProfileDetails.ProfileViews.Add(Date, UserIds);
                        }
                        else
                        {
                            if (!ProfileDetails.ProfileViews[Date].Contains(id))
                            {
                                ProfileDetails.ProfileViews[Date].Add(id);
                            }
                            else
                            {
                                return;
                            }
                        }
                        HttpResponseMessage response = await ProfileService.PutAsync(ProfileDetails);

                        if (response.IsSuccessStatusCode)
                        {

                            SLog.Write("Success");
                        }
                        else
                        {
                            //ToastService.ShowError("Updation Failed! and Profile Name is taken");
                        }

                    }
                    catch (Exception ex)
                    {
                        SLog.Write(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task ButtonClickCallBackEvent()
        {
            UserDetails = await UserService.GetUserById(UserId);
            ProfileDetails = await ProfileService.GetProfileByUserId(UserId);
            ConnectionList = await ConnectionService.GetAllConnectionRequestsForUser(UXUtility.LoggedInUser.Id);
            StateHasChanged();
            if (ConnectionList != null && ConnectionList.Count != 0 && UserDetails != null)
            {
                foreach (var Request in ConnectionList)
                {
                    if (Request.FromUserId == UXUtility.LoggedInUser.Id && Request.ToUserId == UserDetails.Core.Id && Request.Status == ConnectionStatus.Created)
                    {
                        ConnectButton = "Cancel Request";
                    }
                    else if (Request.FromUserId == UserDetails.Core.Id && Request.ToUserId == UXUtility.LoggedInUser.Id && Request.Status == ConnectionStatus.Created)
                    {
                        ConnectButton = "Confirm Request";
                    }
                    else
                    {
                        ConnectButton = "Connect";
                    }
                }
            }
        }

        protected async Task CommunityButtonClickCallBack()
        {
            MyGroups = await GroupService.GetGroupForMemberOrCreator(UserId);
            StateHasChanged();
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }


        protected bool IsVisible(BaseModel Model)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Model.Name.ToUpper().StartsWith(SearchString) || Model.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }

        public Dictionary<string, string> ProfileTabs = new Dictionary<string, string>() {
        {"About","fas fa-user"},
        {"Ideas","fas fa-lightbulb"},
        {"Community","fas fa-users"},
        {"Events","fas fa-calendar-alt"},
        {"Incubation Programs","fas fa-user-graduate"},
        {"Business Pages","fab fa-font-awesome-flag"}
    //{"Entrepreneurial Education","fas fa-book"},
};
    }
}
