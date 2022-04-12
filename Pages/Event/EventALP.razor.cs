using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Shared_Classes;
using System.Collections.Generic;

namespace StartupEd.UX.Incubation
{
    public partial class EventALP : BaseComponent
    {
        public List<Event> AllEvent = new List<Event>();
        public CS_Event ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private string SearchString { get; set; }
        public string ProgramView { get; set; } = "Flix";
        public string EventDeleteId { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }

        public bool Validation = false;
        public string EType { set; get; }

        public void ToggleView(string view)
        {

            ProgramView = view;
            StateHasChanged();
        }

        protected void ShowSweetAlert(string Id)
        {
            if (Id != null)
            {
                EventDeleteId = Id;
            }
            ConfirmSweetAlert.ShowSweetAlert();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
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
            //if (UXUtility.LoggedInUserRole == Roles.Root_SuperAdmin.ToString())
            //{
            //    AllEvent = await ModelService.GetNetworkBusinessModelsFromServer(UXUtility.LoggedInUserRole);
            //    BMC = BMC.OrderByDescending(bmc => bmc.Core.CreatedAt.ToString()).ToList();
            //}
            //else
            //{
            //    BMC = await BMCService.GetBusinessModelByUserId(UXUtility.LoggedInUser.Id);
            //    BMC = BMC.OrderByDescending(bmc => bmc.Core.CreatedAt.ToString()).ToList();

            //}
            //State = LoadingContainerState.Loaded;
            //StateHasChanged();
            AllEvent = await ModelService.GetAllEventsFromServer();
            AllEvent = AllEvent.OrderByDescending(x => x.Deadlines.EventEnd.Date).ToList();
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected bool IsVisible(Event Event)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Event.Name.ToUpper().StartsWith(SearchString) || Event.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }

        public async void DeleteEvent()
        {
            try
            {
                AllEvent = await ModelService.DeleteObject(AllEvent, EventDeleteId);
                if (AllEvent != null)
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

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Events","/events" }
    };
        public List<string> EventType = new List<string>() { "Competition", "Workshop", "Hackthon", "Webinars", "Festivals", "Scholarship", "Quizzes", "Led by Social Issue" };

    }
}
