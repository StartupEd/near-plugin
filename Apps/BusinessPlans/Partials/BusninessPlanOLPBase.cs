using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.Lib.Engine;
using StartupEd.Lib.Core;
using System.Net.Http;
using StartupEd.UX.One.Services;
using Microsoft.JSInterop;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public class BusninessPlanOLPBase : BaseComponent
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        private OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        [Parameter] public string BplanId { get; set; }
        [Parameter] public string BplanName { get; set; }
        public BusinessPlan Model = new BusinessPlan();
        public CS_BusinessPlan BPlanService;
        //private UserActivity_ClientService Activity_ClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                BPlanService = new CS_BusinessPlan(ClientExchange, EngineServices.MarketNetwork);
                if (BplanId != null)
                {
                    Model = await BPlanService.GetBusinessPlanById(BplanId);
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
               // ToastService.ShowError(ex.Message, "BusinessPlan Exception");
            }
        }

        protected void CallDeleteBPlanAlert()
        {
            ShowSweetAlertBox.ShowSweetAlert();
        }
        protected void DeleteBPlanAsync(bool Value)
        {
            if (Value)
            {
                DeleteBplan(Value);
            }
        }

        public async void DeleteBplan(bool Value)
        {
            try
            {
                if (!string.IsNullOrEmpty(BplanId))
                {
                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("Business Plan", BplanId, Model.VentureName, "You have deleted Business Plan " + Model.VentureName);
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                }
                List<BusinessPlan> BP = new List<BusinessPlan>();
                BP = await BPlanService.DeleteObject(BP, BplanId);
                if (BP != null)
                {
                    //ToastService.ShowSuccess("Bplan Deleted Successfuly");
                    _NavigationManager.NavigateTo("businessplan");
                }
            }
            catch (Exception ex)
            {
                //ToastService.ShowError(ex.Message, "Deletion Failed");
            }
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Business Plan","/businessplan" }
    };
    }
}
