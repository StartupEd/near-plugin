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
using Newtonsoft.Json;
using StartupEd.UX.One.Shared_Classes;
using System.Net.Http;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BlogPost
{
    public partial class BlogInsight : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public Model_Blog Model = new Model_Blog();
        public CS_Blog ClientService { get; set; }
        public List<UserFullInfo> BlogVisitors { get; set; } = new List<UserFullInfo>();
        public List<UserFullInfo> BookmarkedByUserIds { get; set; } = new List<UserFullInfo>();
        public CS_User UserService { get; set; }
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        List<string> UserIds { get; set; } = new List<string>();
        public string Date = DateTime.UtcNow.ToLocalTime().ToString("dd MMM yy");

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_Blog(ClientExchange,EngineServices.MarketNetwork);
                UserService = new CS_User(ClientExchange,EngineServices.MarketNetwork);
                await LoadData();
                if (Model.BlogViewCounter.ContainsKey(Date))
                {
                    UserIds = Model.BlogViewCounter[Date];
                }

                BlogVisitors = await UserService.GetUserAndProfileForListOfUserIds(UserIds);
                BookmarkedByUserIds = await UserService.GetUserAndProfileForListOfUserIds(Model.BookmarkedByUserIds);
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }

        public async Task LoadData()
        {
            if (Id != null)
            {
                Model = await ClientService.GetBlogById(Id);
            }
            StateHasChanged();
        }
    }
}
