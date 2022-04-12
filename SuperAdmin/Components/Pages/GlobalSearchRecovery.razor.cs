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
    public partial class GlobalSearchRecovery : BaseComponent
    {
        private List<Model_Event> EventInfos { get; set; } = new List<Model_Event>();
        private List<User> UserInfos { get; set; } = new List<User>();
        private List<Community> GroupInfos { get; set; } = new List<Community>();
        private List<IncubationProgram> IncubationProgramInfos { get; set; } = new List<IncubationProgram>();
        private List<BusinessPage> BusinessPageInfos { get; set; } = new List<BusinessPage>();


        private CS_Event EventsService { get; set; }
        private CS_User UserService { get; set; }
        public CS_Community GroupService { get; set; }
        public CS_IncubationProgram IncubationProgramService { get; set; }
        public CS_BusinessPage BusinessPageService { get; set; }

        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;

        protected override async Task OnInitializedAsync()
        {
            EventsService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
            UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
            IncubationProgramService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
            BusinessPageService = new CS_BusinessPage(ClientExchange, EngineServices.MarketNetwork);

        }

        protected async void RecoverEventSearchIndex()
        {
            try
            {
                EventInfos = await EventsService.EventSearchIndexRecoveryFromServer(UXUtility.LoggedInUserRole);
                //ToastService.ShowSuccess("Event Search Indexes Has Been Recovered!");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async void RecoverUserSearchIndex()
        {
            try
            {
                UserInfos = await UserService.UserSearchIndexRecoveryFromServer(UXUtility.LoggedInUserRole);
                //ToastService.ShowSuccess("User Search Indexes Has Been Recovered!");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
        protected async void RecoverGroupSearchIndex()
        {
            try
            {
                IncubationProgramInfos = await IncubationProgramService.IncubationProgramSearchIndexRecoveryFromServer(UXUtility.LoggedInUserRole);
                //ToastService.ShowSuccess("Incubation Program Search Indexes Has Been Recovered!");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
        protected async void RecoverIncubationProgramSearchIndex()
        {
            try
            {
                GroupInfos = await GroupService.GroupSearchIndexRecoveryFromServer(UXUtility.LoggedInUserRole);
                //ToastService.ShowSuccess("Group Search Indexes Has Been Recovered!");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
        protected async void RecoverBusinessPageSearchIndex()
        {
            try
            {
                BusinessPageInfos = await BusinessPageService.BusinessPageSearchIndexRecoveryFromServer(UXUtility.LoggedInUserRole);
                //ToastService.ShowSuccess("Business Page Search Indexes Has Been Recovered!");
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }
    }
}
