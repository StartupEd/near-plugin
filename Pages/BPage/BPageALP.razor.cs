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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BPage
{
    public partial class BPageALP : BaseComponent
    {
        public List<BusinessPage> BusinessPages { get; set; }
        public CS_BusinessPage ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        public string ProgramView { get; set; } = "Flix";
        private string SearchString { get; set; }
        private string PageDeleteId { get; set; }

        public void ToggleView(string view)
        {
            ProgramView = view;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await SyncLoggedInUserStatus();
            State = LoadingContainerState.Loading;
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_BusinessPage(ClientExchange,EngineServices.MarketNetwork);
                await LoadData();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadData()
        {
            BusinessPages = await ClientService.GetAllBusinessPagesFromServer();
            BusinessPages = BusinessPages.OrderByDescending(x => x.Core.CreatedAt).ToList();
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected bool IsVisible(BusinessPage Page)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (Page.Name.ToUpper().StartsWith(SearchString) || Page.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected void ShowSweetAlert(string Id)
        {
            if (Id != null)
            {
                PageDeleteId = Id;
            }
            ConfirmSweetAlert.ShowSweetAlert();
        }

        public async void DeletePage()
        {
            try
            {
                BusinessPages = await ClientService.DeleteObject(BusinessPages, PageDeleteId);
                string PageName = BusinessPages.Find(page => page.Id == PageDeleteId).Name;
                await BusinessPageRecentActivity(PageDeleteId, PageName, "You have recently deleted Business Page" + PageName);
                if (BusinessPages != null)
                {
                    StateHasChanged();
                    ToastService.ShowSuccess("Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message, "Exception");
            }
        }

        protected async Task BusinessPageRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(new Model_Activity { FeatureId = Id, Feature = Title, Description = description });
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
    {
        { "Busniess Pages","/businesspage" }
    };
    }
}
