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
using Newtonsoft.Json;
using StartupEd.UX.One.Services;

namespace StartupEd.UX.Incubation.Pages.Chatting
{
    public partial class ReceivedMessageCard : BaseComponent
    {
        [Parameter] public string AvatarUrl { get; set; }
        [Parameter] public DateTime MessagedAt { get; set; }
        [Parameter] public string MessageValue { get; set; }
        [Parameter] public string SenderName { get; set; }
    }
}
