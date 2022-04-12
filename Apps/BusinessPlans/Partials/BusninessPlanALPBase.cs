using Microsoft.AspNetCore.Components;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class BusninessPlanALPBase : BaseComponent 
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        protected OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        protected string SearchString { get; set; }
        public List<BusinessPlan> BusinessPlans_Data { get; set; }

        public CS_BusinessPlan ClientService;
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        public string ProgramView { get; set; }
        protected string DeleteBPlanId { get; set; }

        public void ToggleView(string view)
        {
            ProgramView = view;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                ClientService = new CS_BusinessPlan(ClientExchange, EngineServices.MarketNetwork);
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
            if (UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
            {
                BusinessPlans_Data = await ClientService.GetNetworkBusinessPlansFromServer(UXUtility.LoggedInUserRole);
                BusinessPlans_Data = BusinessPlans_Data.OrderByDescending(bp => bp.Core.CreatedAt.ToString()).ToList();
            }
            else
            {
                BusinessPlans_Data = await ClientService.GetBusinessPlanByUserId(UXUtility.LoggedInUser.Id);
                BusinessPlans_Data = BusinessPlans_Data.OrderByDescending(bp => bp.Core.CreatedAt.ToString()).ToList();
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }

        protected void EditBusinessPlanAsync(string BPlanId)
        {
            if (!string.IsNullOrEmpty(BPlanId))
            {
                string BplanName = BusinessPlans_Data.Find(plan => plan.Id == BPlanId).VentureName;
                _NavigationManager.NavigateTo($"businessplan/{BPlanId}/{BplanName}/edit");
            }
        }

        protected void CallDeleteBPlanAlert(string Id)
        {
            if (Id != null)
            {
                DeleteBPlanId = Id;
            }
            ShowSweetAlertBox.ShowSweetAlert();
        }
        protected void DeleteBPlanAsync(bool Value)
        {
            if (Value)
            {
                DeleteBplan(DeleteBPlanId);
            }
        }

        public async void DeleteBplan(string BplanId)
        {
            try
            {
                //if (BusinessPlans != null)
                //{
                //    string ventureName = BusinessPlans.Find(r => r.Id == BplanId).VentureName;
                //    UserActivity userActivity = new UserActivity();
                //    Activity Module = new Activity();
                //    Module.CreateActivity("Business Plan", BplanId, ventureName, "You have deleted Business Plan " + ventureName);
                //    userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //    userActivity.Activities.Add(Module);
                //    await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                //}
                BusinessPlans_Data = await ClientService.DeleteObject(BusinessPlans_Data, BplanId);
                if (BusinessPlans_Data != null)
                {
                    StateHasChanged();
                    //ToastService.ShowSuccess("Bplan Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Deletion Failed");
            }
        }

        protected bool IsVisible(BusinessPlan BP)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (BP.VentureName.ToUpper().StartsWith(SearchString) || BP.VentureName.ToLower().StartsWith(SearchString))
                return true;

            return false;
        }

        public void GetSearchedString(string str)
        {
            SearchString = str;
            StateHasChanged();
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Business Plan","/businessplan" }
    };

    }
}
