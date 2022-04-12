using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Shared_Classes;
using System.Collections.Generic;
using StartupEd.Lib.Engine;
using Microsoft.AspNetCore.Components.Web;

namespace StartupEd.UX.Incubation
{
    public partial class RoundForm : BaseComponent
    {
        [Parameter]
        public Round RoundData { get; set; }
        [Parameter]
        public string Style { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback1 { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback2 { get; set; }
        public int i = 0;
        public List<string> ListofBusinessAppValue = new List<string>() { "BusinessModel", "BusinessPlan", "Other" };
        public List<string> ListofBusinessApp = new List<string>() { "Business Model Canvas", "Business Plan" };
        //System.Guid guid1;
        //System.Guid guid2;
        //protected async override Task OnParametersSetAsync()
        //{
        //    guid1 = System.Guid.NewGuid();
        //    guid2 = System.Guid.NewGuid();
        //    guid1.ToString() guid2.ToString()
        //}
    }
}
