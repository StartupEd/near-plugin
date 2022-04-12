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
    public partial class SearchResults : BaseComponent
    {
        [Parameter] public string Query { get; set; }
        public CS_User ClientService { get; set; }
        public CS_Event EventsClientService { get; set; }
        public CS_BusinessPage BusinessPageClientService { get; set; }
        public CS_Community GroupClientService { get; set; }
        public CS_IncubationProgram IncubationProgramClientService { get; set; }
        public List<ResultDoc<SearchUnit>> SearchData { get; set; }
        public List<ResultDoc<SearchUnit>> SearchDataUser { get; set; }
        public List<ResultDoc<SearchUnit>> SearchDataEvents { get; set; }
        public List<ResultDoc<SearchUnit>> SearchDataBusinessPage { get; set; }
        public List<ResultDoc<SearchUnit>> SearchDataGroup { get; set; }
        public List<ResultDoc<SearchUnit>> SearchDataIncubationProgram { get; set; }
        private HubConnection hubConnection;
        public List<UserFullInfo> AllUserInfo { get; set; }
        public List<Event> AllEvents { get; set; }
        public List<BusinessPage> AllBusinessPages { get; set; }
        public List<IncubationProgram> AllIncubationPrograms { get; set; }
        public List<Community> AllGroups { get; set; }
        public int UserCount = 0;
        public int EventCount = 0;
        public int BusinessPageCount = 0;
        public int GroupsCount = 0;
        public int IncubationProgramCount = 0;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                EventsClientService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                BusinessPageClientService = new CS_BusinessPage(ClientExchange, EngineServices.MarketNetwork);
                GroupClientService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                IncubationProgramClientService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                AllUserInfo = new List<UserFullInfo>();
                AllEvents = new List<Event>();
                AllBusinessPages = new List<BusinessPage>();
                AllGroups = new List<Community>();
                AllIncubationPrograms = new List<IncubationProgram>();
                hubConnection = new HubConnectionBuilder()
           .WithUrl(EngineServices.MarketNetwork.EndPoint + "/searchhub")
           .Build();

                hubConnection.On("Searching", () =>
                {
                    CallLoadData();
                });
                await hubConnection.StartAsync();
                await LoadData();
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }

        }

        private void CallLoadData()
        {
            Task.Run(async () =>
            {
                await LoadData();
            });
        }

        protected async Task LoadData()
        {
            SearchData = await ClientService.GlobalSearch(Query);
            if (SearchData != null)
            {
                SearchDataUser = SearchData.Where(sd => sd._index.Contains("user")).ToList();
                SearchDataEvents = SearchData.Where(sd => sd._index.Contains("events")).ToList();
                SearchDataBusinessPage = SearchData.Where(sd => sd._index.Contains("businesspage")).ToList();
                SearchDataGroup = SearchData.Where(sd => sd._index.Contains("groups")).ToList();
                SearchDataIncubationProgram = SearchData.Where(sd => sd._index.Contains("incubationprogram")).ToList();
                AllUserInfo = await ClientService.GetSearchUserInfo(SearchDataUser);
                AllEvents = await EventsClientService.GetSearchEventsInfo(SearchDataEvents);
                AllBusinessPages = await BusinessPageClientService.GetSearchBusinessPageInfo(SearchDataBusinessPage);
                AllGroups = await GroupClientService.GetSearchGroupInfo(SearchDataGroup);
                AllIncubationPrograms = await IncubationProgramClientService.GetSearchIncubationProgramInfo(SearchDataIncubationProgram);

            }
            StateHasChanged();
        }
    }
}
