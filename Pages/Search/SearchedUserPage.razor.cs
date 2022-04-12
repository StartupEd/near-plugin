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
    public partial class SearchedUserPage : BaseComponent
    {
        [Parameter] public string Query { get; set; }
        [Parameter] public int Page { get; set; }
        //public List<Hits2<SearchClass>> SearchDataUser { get; set; }
        public List<UserFullInfo> AllUserInfo { get; set; }
        public CS_User ClientService { get; set; }
        public int NextPage;
        public int PreviousPage;
        public int UserCount;
        int totalPages;
        int remainder;



        protected async override Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                AllUserInfo = new List<UserFullInfo>();
                //SearchDataUser = await ClientService.SearchUser(Query);
                //UserCount = SearchDataUser.Count();
                totalPages = UserCount / 8;
                remainder = UserCount % 8;
                if (remainder != 0)
                {
                    totalPages = totalPages + 1;
                }

            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }

        }

        protected async override Task OnParametersSetAsync()
        {
            await LoadData();
            SetupPages();
        }

        public void SetupPages()
        {
            if (Page == 1)
            {
                PreviousPage = 1;
            }
            else
            {
                PreviousPage = Page - 1;
            }

            if (Page == totalPages)
            {
                NextPage = Page;
            }
            else
            {
                NextPage = Page + 1;
            }
        }

        protected async Task LoadData()
        {
            int count = 8;
            int StartedIndex = 8 * (Page - 1);
            //if ((count * Page) > SearchDataUser.Count)
            //{
            //    count = SearchDataUser.Count() - StartedIndex;
            //}
            //if (SearchDataUser != null)
            //{

            //    try
            //    {
            //        AllUserInfo = await ClientService.GetSearchUserInfo(SearchDataUser.ToList().GetRange(StartedIndex, count));
            //    }
            //    catch (Exception ex)
            //    {
            //        SLog.Write(ex.Message);
            //    }

            //}
            StateHasChanged();
        }
    }
}
