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
    public partial class KanbanItem : BaseComponent
    {
        [CascadingParameter] KanbanContainer Container { get; set; }
        [Parameter] public MyTask Model { get; set; }
        [Parameter] public int Value { get; set; }
        [Parameter] public List<User> TeamMembers { get; set; }
        [Parameter] public List<Model_Profile> Profiles { get; set; }
        [Parameter] public EventCallback<string> OnTaskUpdate { get; set; }
        public CS_MyTask ClientService { get; set; }
        IJSObjectReference module = null;
        public TaskViewModal Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_MyTask(ClientExchange, EngineServices.MarketNetwork);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        private void HandleDragStart(MyTask selectTask)
        {
            Container.Payload = selectTask;

        }
    }
}
