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

namespace StartupEd.UX.Incubation.Pages.Dashboard
{
    public partial class WidgetObject : BaseComponent
    {
        [Parameter] public string PrimaryText { get; set; }
        [Parameter] public string MutedText { get; set; }
        [Parameter] public string ImageUrl { get; set; }
        [Parameter] public string Route { get; set; } = "";
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
