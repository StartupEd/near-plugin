// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Connections
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Engine.MarketNetwork;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core.Modules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Chat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Alerts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Pricing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Contact;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Custom.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 68 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Metronics.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Connections/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Inbox;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Connections/_Imports.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    public partial class NewConnectionCard : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple/UXs/Incubation/Pages/Connections/NewConnectionCard.razor"
       
    private PersonalizeNoteModal ConfirmConnectionRequest { get; set; }
    private string MessageValue { get; set; }
    [Parameter] public int MutualConnections { get; set; }
    [Parameter] public User Model { get; set; }
    [Parameter] public Profile Profile { get; set; }
    [Parameter] public EventCallback<string> OnSendRequestCallBack { get; set; }

    private ConnectionRequest RequestModel { get; set; } = new ConnectionRequest();
    private ConnectionRequest_ClientService ClientService { get; set; }

    private ChatThread ThreadModel = new ChatThread();
    private ChatThread_ClientService ChatClientService { get; set; }

    private HubConnection notificationHubConnection;

    protected override async Task OnInitializedAsync()
    {
        notificationHubConnection = new HubConnectionBuilder()
        .WithUrl(EngineServices.MarketNetwork.EndPoint + "/notificationhub").Build();

        await notificationHubConnection.StartAsync();
    }

    Task SendNotification() => notificationHubConnection.SendAsync("SendNotification", Model.Id, "You have new connection request.");

    public bool IsConnected => notificationHubConnection.State == HubConnectionState.Connected;


    protected void CallConnectionAlertModal()
    {
        ConfirmConnectionRequest.ShowModal();
    }

    private async Task SubmitRequestModel(bool IsValue)
    {
        try
        {
            ConnectionRequest EmailModel = new ConnectionRequest();
            EmailModel.RequestEmailModel.FromName = loginService.LoggedInUser.Name;       
            EmailModel.RequestEmailModel.ToName = Model.Name;
            EmailModel.RequestEmailModel.ToEmail = Model.Email;
            EmailModel.RequestEmailModel.Note = MessageValue;

            ClientService = new ConnectionRequest_ClientService(EngineServices.MarketNetwork);
            string loggedinUserId = loginService.LoggedInUser.Id;
            RequestModel.FromUserId = loggedinUserId;
            RequestModel.ToUserId = Profile.Core.CreatorId;
            if (!string.IsNullOrEmpty(MessageValue))
            {
                RequestModel.HasMessageRequest = true;
            }
            if (IsValue)
            {
                HttpResponseMessage response = await ClientService.PostAsync(RequestModel, "CreateRequest");
                if (response.IsSuccessStatusCode)
                {
                    await OnSendRequestCallBack.InvokeAsync(Model.Id);
                    if (!string.IsNullOrEmpty(MessageValue))
                    {
                        await SendMessageWithConnectionRequest();
                        MessageValue = "";
                    }
                    bool result = await ClientService.ConnectionRequestEmailNotification(EmailModel);
                    if (IsConnected) await SendNotification();
                    ToastService.ShowSuccess("Connection Request Sent..!", "Successful");
                }
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message, "Connection Request Failed");
        }

    }

    /// <summary>
    /// Use Method to send personalize Message with connection request..!!
    /// </summary>
    /// <returns></returns>
    protected async Task SendMessageWithConnectionRequest()
    {
        ChatClientService = new ChatThread_ClientService(EngineServices.MarketNetwork);
        try
        {
            HttpResponseMessage response;
            List<string> ParticipantIds = new List<string>();
            ParticipantIds.Add(loginService.LoggedInUser.Id);
            ParticipantIds.Add(Model.Id);
            ThreadModel.ParticipantUserIds = ParticipantIds;
            ThreadModel.IsChatRequest = true;
            if (!string.IsNullOrEmpty(MessageValue))
            {
                ThreadModel.Messages.Add(new ChatMessage { AuthorId = loginService.LoggedInUser.Id, Value = MessageValue, SentAt = DateTime.UtcNow });
            }
            response = await ChatClientService.PostAsync(ThreadModel, "CreateThread");
            if (response.IsSuccessStatusCode)
            {
                SLog.Write("Personalize Note Sending Success..!!");
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
        }
    }


    public void DisposeNotification()
    {
        _ = notificationHubConnection.DisposeAsync();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
