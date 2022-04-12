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
    public partial class OrderDetails : BaseComponent
    {
        [Parameter] public string UserId { get; set; }
        public Authentication AuthModel { get; set; } = new Authentication();
        public CS_Authentication AuthService { get; set; }
        public TimeSpan DaysLeft { get; set; }


        protected override async Task OnInitializedAsync()
        {
            AuthService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
            AuthModel = await AuthService.GetAuthModelByUserId(UserId);
            DaysLeft = AuthModel.PlanExpirationDate.ToLocalTime().Subtract(DateTime.UtcNow.ToLocalTime());  // What About Plan Expiry ?
        }
    }
}
