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
    public partial class BlogReactionListModel : BaseComponent
    {
        private string ProfileImage = "https://s1.startuped.net/incubation_cloud/assets/media/users/default.jpg";
        protected bool IsNoteAlertPopUp { get; set; }
        [Parameter] public Model_Blog Model { get; set; }
        [Parameter] public List<CachedUser> CachedUsers { get; set; }
        [Parameter] public List<CachedUser> Reactions { get; set; }
        public CS_User User_clientService { get; set; }

        public void ShowModal()
        {
            IsNoteAlertPopUp = true;
            StateHasChanged();
            //CallPostData(PostId);
        }
        protected override async Task OnInitializedAsync()
        {
            User_clientService = new CS_User(ClientExchange,EngineServices.MarketNetwork);
        }

        protected async Task OnCloseClick(bool value)
        {
            IsNoteAlertPopUp = false;
        }
    }
}
