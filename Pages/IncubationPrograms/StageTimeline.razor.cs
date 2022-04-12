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

namespace StartupEd.UX.Incubation
{
    public partial class StageTimeline : BaseComponent
    {
        [Parameter]
        public List<Stage> Stages { get; set; }
        [Parameter]
        public string DisplayTimeLine { get; set; } = "d-none";
        [Parameter]
        public EventCallback<Stage> ReturnStage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Stages != null && Stages.Count > 0)
            {
                DisplayTimeLine = "d-block";
            }
        }


        public async void EditStage(Stage data)
        {
            await ReturnStage.InvokeAsync(data);
        }

        public void DeleteStage(Stage data)
        {
            Stages.Remove(data);
            if (Stages.Count == 0)
            {
                DisplayTimeLine = "d-none";
                StateHasChanged();
            }
        }
    }
}
