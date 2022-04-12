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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class PaymentSuccess : BaseComponent
    {
        [Parameter] public string SessionId { get; set; }
        [Parameter] public string UserId { get; set; }
        public Authentication AuthModel { get; set; } = new Authentication();
        public CS_Authentication AuthService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            AuthService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
            AuthModel = await AuthService.GetAuthModelByUserId(UserId);
            if (AuthModel.Duration == "Monthly")
            {
                AuthModel.PlanExpirationDate = AuthModel.Core.CreatedAt.AddDays(30);
            }
            else
            {
                AuthModel.PlanExpirationDate = AuthModel.Core.CreatedAt.AddDays(365);
            }

            HttpResponseMessage response = await AuthService.PutAsync(AuthModel);
            if (response.IsSuccessStatusCode)
            {
                //ToastService.ShowSuccess("Your payment was successfully completed...");
            }
            else
            {
                //ToastService.ShowError(await response.Content.ReadAsStringAsync(), "Payment Failed");
            }
        }
    }
}
