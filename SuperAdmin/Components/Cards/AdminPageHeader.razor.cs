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

namespace StartupEd.UX.Incubation.SuperAdmin.Components.Cards
{
    public partial class AdminPageHeader : BaseComponent
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string MutedText { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

    }
}
