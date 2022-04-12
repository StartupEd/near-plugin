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
using Microsoft.AspNetCore.Components.Web;
namespace StartupEd.UX.Incubation
{
    public partial class StageForm : BaseComponent
    {
        [Parameter]
        public Stage StageData { get; set; }
        [Parameter]
        public string Style { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback1 { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback2 { get; set; }
        //public int i = 0;
        //public List<string> ListofBusinessAppValue = new List<string>() { "BusinessModel", "BusinessPlan", "Other" };
        //public List<string> ListofBusinessApp = new List<string>() { "Business Model Canvas", "Business Plan" };

    }
}
