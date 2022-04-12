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
    public partial class ActivityCard : BaseComponent
    {
        [Parameter] public string ActivityAt { get; set; }
        [Parameter] public string Description { get; set; }
    }
}
