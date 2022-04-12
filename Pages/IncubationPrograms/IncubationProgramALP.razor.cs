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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class IncubationProgramALP : BaseComponent
    {

        public List<IncubationProgram> Programs { get; set; } = new List<IncubationProgram>();
        public List<IncubationProgram> IPList { get; set; } = new List<IncubationProgram>();
        public CS_IncubationProgram ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public string ProgramView { get; set; } = "Flix";
        private string SearchString { get; set; }
        public string ProgramDeleteId { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        //public DateTime LocalDate = Utility.ConvertUTCDateTimeToLocal(DateTime.UtcNow);

        public void ToggleView(string view)
        {
            ProgramView = view;
            StateHasChanged();
        }

        protected void ShowSweetAlert(string Id)
        {
            if (Id != null)
            {
                ProgramDeleteId = Id;
            }
            ConfirmSweetAlert.ShowSweetAlert();
        }

        protected override async Task OnInitializedAsync()
        {
            State = LoadingContainerState.Loading;
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                await LoadData();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadData()
        {
            IPList = await ClientService.GetAllIncubationProgramFromServer();
            IPList = IPList.OrderByDescending(x => x.Deadlines.ProgramEnd.Date).ToList();
            Programs = IPList;
            State = LoadingContainerState.Loaded;
            StateHasChanged();
            await module.InvokeVoidAsync("Chevrons", "all");
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected bool IsVisible(IncubationProgram Program)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Program.Name.ToUpper().StartsWith(SearchString) || Program.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }
        IJSObjectReference module;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {

                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                    await module.InvokeVoidAsync("Chevrons", "all");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        public async void UpcomingPrograms()
        {
            await module.InvokeVoidAsync("Chevrons", "upcoming");
            //Programs = IPList.FindAll(p => Utility.ConvertUTCDateTimeToLocal(p.Deadlines.RegistrationStart) > LocalDate);
            StateHasChanged();
        }


        public async void ProgramRegistration()
        {
            await module.InvokeVoidAsync("Chevrons", "registration");
            //Programs = IPList.FindAll(p => Utility.ConvertUTCDateTimeToLocal(p.Deadlines.RegistrationStart) == LocalDate);
            StateHasChanged();
        }


        public async void ProgramsStarted()
        {
            await module.InvokeVoidAsync("Chevrons", "started");
            //Programs = IPList.FindAll(p => Utility.ConvertUTCDateTimeToLocal(p.Deadlines.ProgramStart) <= LocalDate && LocalDate <= p.Deadlines.ProgramEnd.Date);
            StateHasChanged();
        }

        public async void ProgramsOver()
        {
            await module.InvokeVoidAsync("Chevrons", "over");
            //Programs = IPList.FindAll(p => LocalDate >= p.Deadlines.ProgramEnd.Date);
            StateHasChanged();
        }

        public async void DeleteProgram()
        {
            try
            {
                if (!string.IsNullOrEmpty(ProgramDeleteId))
                {
                    string programName = IPList.Find(p => p.Id == ProgramDeleteId).Name;
                    await IncubationProgramRecentActivity(ProgramDeleteId, programName, "You have recently created new Incubation Program " + programName);
                }
                IPList = await ClientService.DeleteObject(IPList, ProgramDeleteId);
                if (IPList != null)
                {
                    StateHasChanged();
                    //ToastService.ShowSuccess("Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task IncubationProgramRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Incubation Program", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Incubation Programs","/incubationprogram" }
    };
    }
}
