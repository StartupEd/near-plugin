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
using Microsoft.AspNetCore.Components.Web.Virtualization;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class RecommendedBoard : BaseComponent
    {
        private string EventStatus { get; set; }
        private string EventDaysLeft { get; set; }
        public List<Model_Event> EventList { get; set; } = new List<Model_Event>();
        [Parameter] public List<Model_Community> GroupList { get; set; }
        public CS_Event ModelService { get; set; }
        private CS_Community GroupService { get; set; }

        protected async ValueTask<ItemsProviderResult<Model_Community>> LoadGroupsData(ItemsProviderRequest request)
        {
            var results = GroupList;
            return new ItemsProviderResult<Model_Community>(results.Skip(request.StartIndex).Take(request.Count), results.Count());
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                //EventList = await ModelService.GetEventsForFeed(UXUtility.LoggedInUser.Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        private async Task JoinPublicGroup(string groupId)
        {
            try
            {
                GroupService = new CS_Community(ClientExchange, EngineServices.MarketNetwork);
                Community GroupInfo = new Community();
                GroupInfo.Id = groupId;
                HttpResponseMessage response = await GroupService.PutAsync(GroupInfo, "JoinGroup");
                if (response.IsSuccessStatusCode)
                {
                    navigationManager.NavigateTo("community/detail/" + groupId);
                    SLog.Write("User has joined group.." + groupId);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
