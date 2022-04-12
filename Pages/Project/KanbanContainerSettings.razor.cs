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
    public partial class KanbanContainerSettings : BaseComponent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<string> OnStatusUpdated { get; set; }
        [Parameter] public CS_MyTask ModelService { get; set; }
        [Parameter] public Project PModel { get; set; }

        public Model_MyTask Payload { get; set; }

        public async Task UpdateTask(string newStatus)
        {

        }
    }
}
