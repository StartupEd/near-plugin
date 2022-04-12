using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using StartupEd.UX.One.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StartupEd.UX.Incubation
{
    public partial class BMCOLPBase : BaseComponent
    {

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        private NavigationManager _NavigationManager { get; set; }

        [Parameter]
        public string BmcId { get; set; }
        [Parameter]
        public string BmcName { get; set; }
        [Parameter]
        public string EventName { get; set; }

        protected BusinessModel Model = new BusinessModel();

        public List<Event> AllEvent = new List<Event>();
        private CS_BusinessModel BMCService { get; set; }
        public CS_Event EventService { get; set; }
        public string Title { get; set; }
        public string EventId { get; set; }

        IJSObjectReference module = null;
        public bool Save { get; set; } = true;
        protected bool IsSpinning { get; set; } = false;

        //public string TeamMembers { get; set; }
        //public CoreProperties CoreProperty { get; set; }
        //DataProperties DataProperty { get; set; }
        // private UserActivity_ClientService Activity_ClientService { get; set; }

       protected void SelectEvent(ChangeEventArgs e)
        {
            EventId = e.Value.ToString();
        }
        protected override async void OnInitialized()
        {
            try
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/bmc.js");
                await module.InvokeAsync<object>("businessModelCanvas", "");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
            await base.OnInitializedAsync();
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                BMCService = new CS_BusinessModel(ClientExchange,EngineServices.MarketNetwork);
                EventService = new CS_Event(ClientExchange,EngineServices.MarketNetwork);
                AllEvent = await EventService.GetEventsForApps("BusinessModel", UXUtility.LoggedInUser.Id);
                if (BmcId != null)
                {
                    Model = await BMCService.GetBusinessModelById(BmcId);
                    Title = Model.Name;
                    StateHasChanged();
                    var Object = await module.InvokeAsync<object>("businessModelCanvas", Model);
                }
                // Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //TeamMembers = Model.TeamMembers;
                //if (Model.DataProperty.Permissions.ContainsKey(UXUtility.LoggedInUser.Id))
                //{
                //    if (Model.DataProperty.Permissions[UXUtility.LoggedInUser.Id].Write == false)
                //    {
                //        Save = false;
                //    }
                //}
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }



        protected async void SubmitModel()
        {
            try
            {
                IsSpinning = true;
                HttpResponseMessage response;
                if (string.IsNullOrEmpty(Title))
                {
                    //ToastService.ShowError("Title can't be empty", "");
                    IsSpinning = false;
                    return;
                }
                if (!string.IsNullOrEmpty(BmcId))
                {
                    //DataProperty = Model.DataProperty;
                    //CoreProperty = Model.Core;
                }
                string CreatorId = Model.Core.CreatorId;
                var Object = await module.InvokeAsync<object>("businessModelCanvas", "");
                Model = Newtonsoft.Json.JsonConvert.DeserializeObject<BusinessModel>(Object.ToString());
                Model.KeyPartners = Model.RemoveTags(Model.KeyPartners);
                Model.KeyActivities = Model.RemoveTags(Model.KeyActivities);
                Model.KeyResources = Model.RemoveTags(Model.KeyResources);
                Model.Channels = Model.RemoveTags(Model.Channels);
                Model.CostStructure = Model.RemoveTags(Model.CostStructure);
                Model.CustomerRelationship = Model.RemoveTags(Model.CustomerRelationship);
                Model.CustomerSegments = Model.RemoveTags(Model.CustomerSegments);
                Model.RevenueStreams = Model.RemoveTags(Model.RevenueStreams);
                Model.ValueProposition = Model.RemoveTags(Model.ValueProposition);
                Model.Name = Title;
                //Model.TeamMembers = TeamMembers;
                if (BmcId != null && BmcId != "new")
                {
                    Model.Id = BmcId;
                    //CoreProperty.Name = Title;
                    //Model.Core = CoreProperty;
                    //Model.DataProperty = DataProperty;
                    response = await BMCService.PutAsync(Model);
                    if (response.IsSuccessStatusCode)
                    {
                       // await PostUserActivity(BmcId, Title, "You have made changes in BMC " + Title);
                       // ToastService.ShowSuccess("BMC Updated Successfully!", "");
                        _NavigationManager.NavigateTo("/businessmodelcanvas");
                    }
                }
                else
                {
                    if (Title != null)
                    {
                        await BMCService.BMCConfirmationEmail(UXUtility.LoggedInUser.Email, UXUtility.LoggedInUser.Name);


                        response = await BMCService.PostAsync(Model);
                        if (response.IsSuccessStatusCode)
                        {
                            //await PostUserActivity(response.Content.ReadAsStringAsync().Result, Title, "You have created new BMC " + Title);
                            //ToastService.ShowSuccess("BMC Created Successfully!", ":)");
                            _NavigationManager.NavigateTo("/businessmodelcanvas");
                        }
                    }
                    else
                    {
                        //ToastService.ShowError("Title can't be empty", "");
                    }
                }
                IsSpinning = false;
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                IsSpinning = false;
            }
        }


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "Business Model Canvas","/businessmodelcanvas" }
    };

        //protected async Task PostUserActivity(string bmcId, string bmcName, string description)
        //{
        //    UserActivity userActivity = new UserActivity();
        //    Activity Module = new Activity();
        //    Module.CreateActivity("Business Model Canvas", bmcId, bmcName, description);
        //    userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
        //    userActivity.Activities.Add(Module);
        //    await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        //}
    }
}
