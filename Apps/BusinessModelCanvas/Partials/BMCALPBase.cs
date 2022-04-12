using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupEd.UX.Incubation
{
    public partial class BMCALPBase : BaseComponent 
    {
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        public OneUX_SweetAlertBox ShowSweetAlertBox { get; set; }
        public CS_BusinessModel BMCService { get; set; }
       // private UserActivity_ClientService Activity_ClientService { get; set; }
        public List<BusinessModel> BMC { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        public string ProgramView { get; set; }
        public string SearchString { get; set; }
        public string DeleteBMCId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);   Review For Dependancy Injection
                BMCService = new CS_BusinessModel(ClientExchange,EngineServices.MarketNetwork);
                await LoadBMCs();
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                State = LoadingContainerState.Error;
            }
            await base.OnInitializedAsync();
        }

        protected async Task LoadBMCs()
        {
            try
            {
                if (UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
                {
                    BMC = await BMCService.GetNetworkBusinessModelsFromServer(UXUtility.LoggedInUserRole);
                    BMC = BMC.OrderByDescending(bmc => bmc.Core.CreatedAt.ToString()).ToList();
                }
                else
                {
                    BMC = await BMCService.GetBusinessModelByUserId(UXUtility.LoggedInUser.Id);
                    BMC = BMC.OrderByDescending(bmc => bmc.Core.CreatedAt.ToString()).ToList();

                }
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                throw;
            }
        }

        //Review For Dependancy Injection or global
        //Start
        public void ToggleView(string view)
        {
            ProgramView = view;
            StateHasChanged();
        }
        //End

        protected void EditBusinessModelAsync(string BmcId)
        {
            if (!string.IsNullOrEmpty(BmcId))
            {
                string BmcName = BMC.Find(app => app.Id == BmcId).Name;
                _NavigationManager.NavigateTo($"businessmodelcanvas/{BmcId}/{BmcName}");
            }
        }

        protected void CallDeleteBMCAlert(string Id)
        {
            if (Id != null)
            {
                DeleteBMCId = Id;
            }
            ShowSweetAlertBox.ShowSweetAlert();
        }

        protected async void DeleteBMCAsync(bool Value)
        {
            if (Value)
            {
                try
                {
                    if (UXUtility.LoggedInUserRole == ApiRoles.SuperAdmin.ToString())
                    {
                        BMC = await BMCService.DeleteObject(BMC, DeleteBMCId);
                        //ToastService.ShowSuccess("BMC Deleted Successfuly");
                    }
                    else 
                    {
                        string bmcTitle = BMC.Find(r => r.Id == DeleteBMCId).Name;
                        BMC = await BMCService.DeleteObject(BMC, DeleteBMCId);
                    }
                }
                catch (Exception ex)
                {
                    SLog.Write(ex.Message);
                    //ToastService.ShowError(ex.Message, "Deletion Failed");
                }
            }
        }

        protected bool IsVisible(BusinessModel BMC)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (BMC.Name.ToUpper().StartsWith(SearchString) || BMC.Name.ToLower().StartsWith(SearchString))
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
        { "Business Model Canvas","/businessmodelcanvas" }
          };
    }
}
