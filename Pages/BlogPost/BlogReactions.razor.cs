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

namespace StartupEd.UX.Incubation.Pages.BlogPost
{
    public partial class BlogReactions : BaseComponent
    {
        [Parameter]
        public string ImgLike { get; set; }
        [Parameter]
        public string ImgLove { get; set; }
        [Parameter]
        public string ImgDollar { get; set; }
        [Parameter]
        public string ImgRocket { get; set; }
        [Parameter]
        public string ImgInsite { get; set; }
        [Parameter]
        public string ImgStartupEd { get; set; }
        [Parameter]
        public Model_Blog Model { get; set; }
        [Parameter]
        public EventCallback OnReactionCountClick { get; set; }
        [Parameter]
        public EventCallback<string> OnReactionClick { get; set; }


        protected async Task OnReactionChanged(string ReactionType)
        {
            if (UXUtility.LoggedInUser.Id != null)
            {
                await OnReactionClick.InvokeAsync(ReactionType);
            }
            else
            {
                Navigation.NavigateTo($"login?returnUrl={Navigation.Uri}");
            }
        }
        protected async Task OnReactionCount()
        {
            await OnReactionCountClick.InvokeAsync();
        }
    }
}
