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


namespace StartupEd.UX.Incubation.Pages.Milestone
{
    public partial class ClaimCertificates : BaseComponent
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public string Organisation { get; set; }
        [Parameter] public string CoverImageUrl { get; set; }
        [Parameter] public DateTime StartDate { get; set; }
        [Parameter] public DateTime EndDate { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        //[Parameter] public EventCallback<string> OnClaimClick { get; set; }
        //[Parameter] public string BtnDesabled { get; set; }
    }
}
