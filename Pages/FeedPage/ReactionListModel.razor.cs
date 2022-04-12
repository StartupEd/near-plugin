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
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;
namespace StartupEd.UX.Incubation
{
    public partial class ReactionListModel : BaseComponent
    {
        private string ProfileImage = "https://s1.startuped.net/incubation_cloud/assets/media/users/default.jpg";
        protected bool IsNoteAlertPopUp { get; set; }
        [Parameter] public Post Model { get; set; }
        [Parameter] public List<CachedUser> CachedUsers { get; set; }
        [Parameter] public List<CachedUser> Reactions { get; set; }

        public CS_User CS_User { get; set; }


        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;


        public async void ShowModal()
        {
            IsNoteAlertPopUp = true;
            StateHasChanged();
            //CallPostData(PostId);
        }
        protected override async Task OnInitializedAsync()
        {
            CS_User = new CS_User(ClientExchange, EngineServices.MarketNetwork);

        }

        protected async Task OnCloseClick(bool value)
        {
            IsNoteAlertPopUp = false;
        }
    }
}
