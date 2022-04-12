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

namespace StartupEd.UX.Incubation.SuperAdmin.Components.Users
{
    public partial class UserAccountCard : BaseComponent
    {
        [Parameter] public Model_Profile ProfileModel { get; set; }
        [Parameter] public User UserModel { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public EventCallback<string> OnDeactivateClick { get; set; }

        protected async Task DeactivateUserAccountAsync(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                await OnDeactivateClick.InvokeAsync(UserId);
            }
        }

        protected void EditUserProfile()
        {
            navigationManager.NavigateTo($"/root/user-profile/{UserModel.Core.Id}");
        }
    }
}
