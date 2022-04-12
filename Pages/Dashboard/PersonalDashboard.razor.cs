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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class PersonalDashboard : BaseComponent
    {
        private OneUX_SweetAlertBox ConfirmToDoDone { get; set; }
        private User UserModel = new User();
        private Profile ProfileModel = new Profile();
        private List<UserFullInfo> ProfileVisitors { get; set; } = new List<UserFullInfo>();
        private List<string> Last7Days { get; set; }
        private List<int> ChartData { get; set; }
        private List<string> VisitorsId { get; set; } = new List<string>();

        private CS_Profile ProfileService { get; set; }
        private CS_User UserService { get; set; }
        //private UserActivity_ClientService ActivityService { get; set; }

        private UserActivity UserActivities { get; set; } = new UserActivity();
        private List<User> UserConnections { get; set; } = new List<User>();
        private List<ConnectionLocation> ConnectionLocations { get; set; } = new List<ConnectionLocation>();
        private ConnectionLocation CLocation = new ConnectionLocation();

        private List<ConnectionRequest> ConnectionRequests = new List<ConnectionRequest>();
        private CS_ConnectionRequest Request_ClientService { get; set; }
        private int SentConnectionRequests { get; set; }
        private int ReceivedConnectionRequests { get; set; }

        private List<Event> UserEvents { get; set; } = new List<Event>();
        private List<Event> AppliedEvents { get; set; } = new List<Event>();
        private CS_Event EventService { get; set; }
        private List<Registration> EventRegistrations { get; set; } = new List<Registration>();
        private List<string> RegistrationPeriod { get; set; } = new List<string>();
        private List<int> RegistrationData { get; set; } = new List<int>();
        protected string SelectedEventId { get; set; }


        private List<MyTask> ToDoTasks { get; set; } = new List<MyTask>();
        private MyTask TaskModel = new MyTask();
        private CS_MyTask Task_ClientService { get; set; }
        private List<string> DefalutStatus = new List<string>() { "To do", "Progress", "Done" };
        private string PriorityCode { get; set; } = Priority.High.ToString();
        public enum Priority { High = 1, Medium = 2, Low = 3 }
        public string DisappearWithin { get; set; }

        //private Dictionary<KeyValuePair<string, double>, KeyValuePair<string,string>> MapCorordinates { get; set; }
        IJSObjectReference _module;
        IJSObjectReference module;
        private HubConnection CallbackhubConnection;
        protected enum LoadingContainerState { Loading, Loaded, Error }
        protected LoadingContainerState State;
        protected LoadingContainerState ActivityState;
        protected LoadingContainerState TodoState;
        protected LoadingContainerState ConnState;
        protected LoadingContainerState EventState;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                string LoggedInUserId = UXUtility.LoggedInUser.Id;
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                Request_ClientService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                //ActivityService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                Task_ClientService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);

                CallbackhubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.SystemEndpoint.Host + "/callback").Build();
                CallbackhubConnection.On("RerenderData", () =>
                {
                    CallLoadMyTaskToDos();
                    StateHasChanged();
                });

                await LoadUserConnections(LoggedInUserId);
                await LoadRecentActivity(LoggedInUserId);
                await LoadMyTaskToDos(LoggedInUserId);
                await LoadUserEvents(LoggedInUserId);

                await CallbackhubConnection.StartAsync();
                if (CallbackhubConnection.State == HubConnectionState.Connected)
                    Console.WriteLine("callback connection started");
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        public bool IsConnected => CallbackhubConnection.State == HubConnectionState.Connected;
        Task LoadDataCallback() => CallbackhubConnection.SendAsync("DataCallback");

        private void CallLoadMyTaskToDos()
        {
            Task.Run(async () =>
            {
                ToDoTasks = await Task_ClientService.GetAssignedTaskForTodoAsync(UXUtility.LoggedInUser.Id);
                StateHasChanged();
            });
        }

        protected async override Task OnParametersSetAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                string LoggedInUserId = UXUtility.LoggedInUser.Id;
                UserModel = await UserService.GetUserInfoById(LoggedInUserId);
                ProfileModel = await ProfileService.GetProfileByUserId(LoggedInUserId);

                Last7Days = new List<string>();
                ChartData = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    string Date = DateTime.UtcNow.ToLocalTime().AddDays(-i).ToString("dd MMM");
                    Last7Days.Add(Date);

                    if (ProfileModel.ProfileViews.ContainsKey(DateTime.UtcNow.ToLocalTime().AddDays(-i).ToString("dd MMM yy")))
                    {
                        var Data = ProfileModel.ProfileViews.FirstOrDefault(x => x.Key == DateTime.UtcNow.ToLocalTime().AddDays(-i).ToString("dd MMM yy")).Value;
                        if (Data.Count == 0)
                        {
                            ChartData.Add(0);
                        }
                        if (Data.Count > 0)
                        {
                            ChartData.Add(Data.Count);
                            if (Data.Count > 1)
                            {
                                VisitorsId.AddRange(Data);
                            }
                            else
                            {
                                VisitorsId.Add(Data[0]);
                            }
                        }
                    }
                    else
                    {
                        ChartData.Add(0);
                    }
                }
                Last7Days.Reverse();
                ChartData.Reverse();
                Console.WriteLine(VisitorsId.Count);
                VisitorsId = VisitorsId.Distinct().ToList();
                if (VisitorsId.Count != 0)
                {
                    ProfileVisitors = await UserService.GetUserAndProfileForListOfUserIds(VisitorsId);
                }
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadUserConnections(string loggedInUserId)
        {
            try
            {
                ConnState = LoadingContainerState.Loading;
                UserConnections = await UserService.GetUserConnectionsListAsync(loggedInUserId);
                foreach (var connections in UserConnections)
                {
                    CLocation = new ConnectionLocation();
                    CLocation.name = connections.Name;
                    //CLocation..Latitude = connections.Address.Lattitude;
                    //CLocation.coordinate.Longitude = connections.Address.Longitude;
                    if (connections.Address.Lattitude != 0 && connections.Address.Longitude != 0)
                    {
                        ConnectionLocations.Add(CLocation);
                    }
                }
                Console.WriteLine(ConnectionLocations.Count);

                ConnectionRequests = await Request_ClientService.GetAllConnectionRequestsForUser(loggedInUserId);
                if (ConnectionRequests.Count != 0)
                {
                    SentConnectionRequests = ConnectionRequests.FindAll(x => x.FromUserId == UXUtility.LoggedInUser.Id && x.Status == ConnectionStatus.Created).Count;
                    ReceivedConnectionRequests = ConnectionRequests.FindAll(x => x.ToUserId == UXUtility.LoggedInUser.Id && x.Status == ConnectionStatus.Created).Count;
                }
                ConnState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                ConnState = LoadingContainerState.Error;
            }
        }

        protected async Task LoadRecentActivity(string loggedInUserId)
        {
            try
            {
                ActivityState = LoadingContainerState.Loading;
                //UserActivities = await ActivityService.GetUserRecentActivityById(loggedInUserId);
                ActivityState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                ActivityState = LoadingContainerState.Error;
            }
        }

        protected async Task LoadMyTaskToDos(string loggedInUserId)
        {
            try
            {
                TodoState = LoadingContainerState.Loading;
                ToDoTasks = await Task_ClientService.GetAssignedTaskForTodoAsync(loggedInUserId);
                TodoState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                TodoState = LoadingContainerState.Error;
            }
        }

        private async void DiscardToDo()
        {
            TaskModel.Clear();
            await _module.InvokeAsync<object>("AccordianCollapse", "collapseTodoForm");
        }

        private async Task SubmitPersonalToDoAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TaskModel.TaskTitle))
                {
                    //ToastService.ShowWarning("Please Fill To Do Title", "Required");
                    return;
                }
                TaskModel.Core.CreatorId = UXUtility.LoggedInUser.Id;
                TaskModel.Name = UXUtility.LoggedInUser.Name;
                TaskModel.TaskAssignUserId = UXUtility.LoggedInUser.Id;
                TaskModel.TaskAssignUserName = UXUtility.LoggedInUser.Name;
                TaskModel.Status = DefalutStatus[0];
                TaskModel.TaskPriority = PriorityCode;
                TaskModel.TaskCompleteOn = await JSRuntime.InvokeAsync<DateTime>("DatePickerValue", "taskCompleteOnDate");
                if (string.IsNullOrEmpty(TaskModel.Core.Id))
                {
                    HttpResponseMessage response = await Task_ClientService.PostAsync(TaskModel);
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await LoadDataCallback();
                        await _module.InvokeAsync<object>("AccordianCollapse", "collapseTodoForm");
                        //ToastService.ShowSuccess("Personal To Do has been created");
                        TaskModel.Clear();
                    }
                    else
                    {
                        SLog.Write("To Do Creation Failed!");
                        //ToastService.ShowError(response.ReasonPhrase, "Error");
                    }
                }
                else
                {
                    HttpResponseMessage response = await Task_ClientService.PutAsync(TaskModel);
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await LoadDataCallback();
                        await _module.InvokeAsync<object>("AccordianCollapse", "collapseTodoForm");
                        //ToastService.ShowSuccess("To Do has been updated!");
                        TaskModel.Clear();
                    }
                    else
                    {
                        SLog.Write("To Do Updation Failed!");
                        //ToastService.ShowError(response.ReasonPhrase, "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task EditToDoAsync(string todoId)
        {
            if (!string.IsNullOrEmpty(todoId))
            {
                TaskModel = ToDoTasks.Find(td => td.Core.Id == todoId);
                await _module.InvokeAsync<object>("ShowCollapse", "collapseTodoForm");
            }
        }

        private async Task UpdateToDoStatusOnCheckChanged(string TodoId)
        {
            try
            {
                HttpResponseMessage response;
                TaskModel = ToDoTasks.Where(task => task.Core.Id == TodoId).FirstOrDefault();
                //TaskModel = ToDoTasks.SingleOrDefault(task => task.Core.Id == TodoId && task.Status == DefalutStatus[0] || task.Status == DefalutStatus[2]);
                if (TaskModel.Status == DefalutStatus[0])
                {
                    TaskModel.TaskDoneAt = DateTime.UtcNow;
                    TaskModel.Status = DefalutStatus[2];
                    response = await Task_ClientService.PutAsync(TaskModel);
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await LoadDataCallback();
                        //ToastService.ShowSuccess("To do has been complete.", "Done");
                    }
                    else
                    {
                        SLog.Write("To Do Can't Update As Done.");
                        //ToastService.ShowError(response.ReasonPhrase, "Error");
                    }
                }
                else if (TaskModel.Status == DefalutStatus[2])
                {
                    TaskModel.Status = DefalutStatus[0];
                    response = await Task_ClientService.PutAsync(TaskModel);
                    if (response.IsSuccessStatusCode)
                    {
                        if (IsConnected) await LoadDataCallback();
                        //ToastService.ShowSuccess("To Do status reverse from Done to To do");
                    }
                    else
                    {
                        SLog.Write("Not Able To Reverse Status Done to To do!");
                        //ToastService.ShowError(response.ReasonPhrase, "Error");
                    }
                }
                else
                {
                    //ToastService.ShowError("Something Went Wrong!", "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }


        protected async void DeleteToDoAsync(string todoId)
        {
            if (!string.IsNullOrEmpty(todoId))
            {
                string TodoName = ToDoTasks.Find(td => td.Core.Id == todoId).Name;
                try
                {
                    HttpResponseMessage message = await Task_ClientService.DeleteAsync(todoId);
                    if (message.IsSuccessStatusCode)
                    {
                        //ToastService.ShowSuccess("To Do Has Been Deleted Successfully!");
                        if (IsConnected) await LoadDataCallback();
                    }
                }
                catch (Exception ex)
                {
                    //ToastService.ShowError(ex.Message, "Exception");
                }
            }
        }

        protected async Task LoadUserEvents(string loggedInUserId)
        {
            try
            {
                UserEvents AllEvents = new UserEvents();
                EventState = LoadingContainerState.Loading;
                AllEvents = await EventService.GetCreatedAndAppliedEventsForUser(loggedInUserId);
                UserEvents = AllEvents.MyEvents.FindAll(data => data.Core.CreatorId == loggedInUserId).ToList();
                EventRegistrations = AllEvents.RegisteredUsers;
                foreach (var eventinfo in AllEvents.AppliedEventIds)
                {
                    Event appliedEvent = AllEvents.MyEvents.Find(data => data.Core.Id == eventinfo);
                    AppliedEvents.Add(appliedEvent);
                }
                if (UserEvents != null && UserEvents.Count != 0)
                {
                    DisplayEventsChartAsync(UserEvents[0].Core.Id);
                }
                EventState = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                EventState = LoadingContainerState.Error;
            }
        }

        protected void DisplayEventsChartAsync(string eventId)
        {
            if (UserEvents != null && UserEvents.Count != 0)
            {
                RegistrationPeriod.Clear();
                RegistrationData.Clear();
                Event SelectedEvent = UserEvents.Find(e => e.Core.Id == eventId);

                DateTime startDate = SelectedEvent.Deadlines.RegistrationStart.ToLocalTime();
                DateTime endDate = SelectedEvent.Deadlines.RegistrationEnd.ToLocalTime();

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    RegistrationPeriod.Add(date.ToString("dd MMM"));
                    if (EventRegistrations != null && EventRegistrations.Count != 0)
                    {
                        RegistrationData.Add(EventRegistrations.FindAll(x => x.EventsId == eventId && x.ApplyDate.ToShortDateString() == date.ToShortDateString()).Count);
                    }
                }
            }
        }

        async Task EventDataChangeAsync(ChangeEventArgs e)
        {
            try
            {
                DisplayEventsChartAsync(e.Value.ToString());
                IJSObjectReference module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/linechart.js");
                await module.InvokeVoidAsync("LineCharts", "registrationChart", RegistrationPeriod, RegistrationData, "", "300", "ReRenderChart");
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    IJSObjectReference module2 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "https://s1.startuped.net/incubation_cloud/assets/plugins/custom/leaflet/leaflet.bundle.js");
                    IJSObjectReference _bundle = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "https://s1.startuped.net/incubation_cloud/assets/plugins/custom/draggable/draggable.bundle.js");
                    _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/dashboard/dragablewidget.js");
                    await _module.InvokeVoidAsync("DragableWidget");
                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                    await module.InvokeVoidAsync("BootstrapSelectpicker");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
    }
}
