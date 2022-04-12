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

namespace StartupEd.UX.Incubation.Pages.Connections
{
    public partial class PersonalizeNoteModal : BaseComponent
    {
        protected bool IsNoteAlertPopUp { get; set; }

        [Parameter] public string UserName { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public EventCallback<bool> ButtonClickCallBack { get; set; }

        protected async Task OnButtonConfirmClick(bool value)
        {
            IsNoteAlertPopUp = false;
            await ButtonClickCallBack.InvokeAsync(value);
        }

        public void ShowModal()
        {
            IsNoteAlertPopUp = true;
            StateHasChanged();
        }
    }
}
