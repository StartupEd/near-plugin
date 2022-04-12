using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupEd.UX.Incubation.Shared.Extensions;
using System.Net.Http;
using System.Net.Http.Json;
using StartupEd.Lib.Core;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.Authentication
{
    public class SocialAuthenticationBase: BaseComponent
    {
        [Inject]
        IConfiguration Configuration { get; set; }
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        [Inject]
        private ILoginService loginService { get; set; }
        [Parameter]
        public string SocialType { get; set; }

        protected async override Task OnInitializedAsync()
        {
            string endPoint = string.Format("{0}/authentication/GetAccessToken?sn={1}&code={2}",
                EngineServices.MarketNetwork.SystemEndpoint.Host, SocialType, _NavigationManager.QueryString("code"));
            HttpClient request = new HttpClient();
            var authResult = await request.GetFromJsonAsync<Engine.MarketNetwork.Authentication>(endPoint);
            SLog.Write(authResult.Token);
            await loginService.Login(authResult);
            await SyncLoggedInUserStatus();
            await CheckLogin(UXUtility.LoggedInUser.Email);

            //if (logincheck == "true")
            //{
            // navigationManager.NavigateTo($"/settings/personal/{UXUtility.LoggedInUser.Id}");
            _NavigationManager.NavigateTo("/");
            //}
        }

        public async Task<string> CheckLogin(string Id)
        {
            //Cache_ClientService cacheService = new Cache_ClientService(UXUtility.Engines.MarketNetwork);
            //var logincheck = await cacheService.SelfGetValue(Id);
            //if (logincheck == "true")
            //{
            //    cacheService.SelfSetValue(Id, "false");
            //}

            //SLog.Write("Cache Updated");
            //return logincheck;
            return "false";
        }
    }
}
