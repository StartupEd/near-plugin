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
namespace StartupEd.UX.Incubation.Pages.Sidebar
{
    public partial class NetworkUpdate : BaseComponent
    {
        public Dictionary<string, object> tagAttributes { get; set; } =
new Dictionary<string, object>()
{
        { "class", "form-control kt_selectpicker" }
   };
    }
}
