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

namespace StartupEd.UX.Incubation.SuperAdmin
{
    public partial class CreateAsideMenu : BaseComponent
    {
        private AsideMenu AsideMenu = new AsideMenu();
        private CS_AsideMenu MenuService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                MenuService = new CS_AsideMenu(ClientExchange, EngineServices.MarketNetwork);

            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }

        private async Task SubmitAsideMenuAsync()
        {
            try
            {
                MenuService = new CS_AsideMenu(ClientExchange, EngineServices.MarketNetwork);
                HttpResponseMessage response = await MenuService.PostAsync(AsideMenu, "CreateMenu");
                if (response.IsSuccessStatusCode)
                {
                    SLog.Write("Aside Menu has been created successfully");
                    ToastService.ShowSuccess("Aside Menu has been Created!!");
                }
                else
                {
                    throw new Exception("Not able to Create Menu!");
                }
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
                ToastService.ShowError(e.Message);
            }
        }
    }
}
