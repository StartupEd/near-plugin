using Microsoft.AspNetCore.Components;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.UX.One.Components.Layout.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StartupEd.Lib.Engine;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;

namespace StartupEd.UX.Incubation.Pages.EmailTemplates
{
    public partial class RoundTimeline : BaseComponent
    {
        [Parameter]
        public List<Round> RoundList { get; set; }
        [Parameter]
        public string DisplayTimeLine { get; set; } = "d-none";
        [Parameter]
        public EventCallback<Round> ReturnRound { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (RoundList != null && RoundList.Count > 0)
            {
                DisplayTimeLine = "d-block";
            }
        }

        public void DeleteRound(Round data)
        {
            RoundList.Remove(data);
            if (RoundList.Count == 0)
            {
                DisplayTimeLine = "d-none";
                StateHasChanged();
            }

        }

        public async void EditRound(Round data)
        {
            await ReturnRound.InvokeAsync(data);
        }
    }
}
