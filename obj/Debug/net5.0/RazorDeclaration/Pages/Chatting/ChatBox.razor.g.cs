// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Chatting
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 13 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Engine.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.Lib.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Shared.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Grouping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Connections;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.BPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Others;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.InProgress;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Invite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.FeedPage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Chatting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Sidebar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Milestone;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.Pages.ProfileTemplates;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Maps;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Post;

#line default
#line hidden
#nullable disable
#nullable restore
#line 59 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Card;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Flix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.Grid;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Views.List;

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Composer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Reactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.Modal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Shared.PdfViewer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Aside;

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Header;

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.One.Components.Layout.Header.TopBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 78 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Feeds;

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
using StartupEd.UX.Incubation.SuperAdmin.Components.Cards;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Chatting/_Imports.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Chatting/_Imports.razor"
using Microsoft.AspNetCore.WebUtilities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Chatting/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Inbox;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(InboxLayout))]
    public partial class ChatBox : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 87 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Chatting/ChatBox.razor"
      
    protected AlertModal DeleteConfirmation;
    private string MessageValue { get; set; }
    [Parameter] public string ConnectionId { get; set; }
    [Parameter] public string ConnectionName { get; set; }
    [Parameter] public string ConnectionEmail { get; set; }
    [Parameter] public string UserAvatarUrl { get; set; }
    [Parameter] public string ConnectionAvatarUrl { get; set; }
    [Parameter] public ChatThread PreviousChats { get; set; }
    private User_ClientService UserService { get; set; }
    public ChatThread_ClientService ChatClientService { get; set; }
    private List<ChatMessage> ChatMessages = new List<ChatMessage>();
    private ChatThread Model = new ChatThread();
    public static List<Hits2<SearchClass>> SearchResults { get; set; }
    public static User_ClientService UserService1 { get; set; }

    private HubConnection ChathubConnection;

    IJSObjectReference module = null;
    protected override async void OnInitialized()
    {
        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/chat.js");
        await module.InvokeVoidAsync("inboxElement");

    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ChatClientService = new ChatThread_ClientService(EngineServices.MarketNetwork);
            UserService1 = new User_ClientService(EngineServices.MarketNetwork);
            ChathubConnection = new HubConnectionBuilder().WithUrl(EngineServices.MarketNetwork.EndPoint + "/chathub").Build();
            await ChathubConnection.StartAsync();
        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
        }
    }


    [JSInvokable]
    public static async Task<List<Hits2<SearchClass>>> ReturnUsers(string QueryString)
    {
        SearchResults = await UserService1.SearchUser(QueryString);

        return SearchResults;
    }


    Task SendChatMessage() => ChathubConnection.SendAsync("SendMessage", UXUtility.LoggedInUser.Id, ConnectionId);

    public bool IsChatConnected => ChathubConnection.State == HubConnectionState.Connected;

    private async Task SubmitChatMessage()
    {
        try
        {
            if (PreviousChats.Messages.ElementAtOrDefault(0) == null)
            {
                Console.WriteLine("First Message Checking : Sending Email To Connection");
                await SendFirstChatEmailNotification();
            }

            List<string> ParticipantIds = new List<string>();
            ParticipantIds.Add(UXUtility.LoggedInUser.Id);
            ParticipantIds.Add(ConnectionId);
            Model.ParticipantUserIds = ParticipantIds;
            Model.Messages.Clear();     //Clearing Previous Messages from ChatThread

            ChatMessage message = new ChatMessage();
            message.AuthorId = UXUtility.LoggedInUser.Id;
            MessageValue = await JSRuntime.InvokeAsync<string>("getPostBoxValue", "msgTextarea");
            if (MessageValue != "<br>")
            {
                message.Value = MessageValue;
                message.SentAt = DateTime.UtcNow;
                Model.Messages.Add(message);
                HttpResponseMessage response = await ChatClientService.PutAsync(Model, "ChatMessage");
                if (response.IsSuccessStatusCode)
                {
                    if (IsChatConnected) await SendChatMessage();
                    SLog.Write("Message Sending Success..!!");
                    MessageValue = string.Empty;
                }
            }

        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
            ToastService.ShowError("Message Sending Failed!", "Error");
        }
    }



    protected async Task SendFirstChatEmailNotification()
    {
        try
        {
            Model.EmailModel.SenderName = UXUtility.LoggedInUser.Name;
            Model.EmailModel.ReceiverId = ConnectionId;
            Model.EmailModel.ReceiverName = ConnectionName;
            Model.EmailModel.ReceiverEmail = ConnectionEmail;
            Model.EmailModel.Message = MessageValue;
            bool result = await ChatClientService.SendEmailNotificationForFirstMessage(Model);
            if (result)
            {
                SLog.Write("First Message Email Notification Send Successful!");
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
        }
    }

    /// <summary>
    /// Use method :CallDeleteModalAsync() & DeleteEntireConversation() to delete Message Thread...
    /// </summary>
    ///
    protected void CallDeleteModalAsync()
    {
        DeleteConfirmation.ShowModal();
    }

    protected async Task DeleteEntireConversation(bool isConfirmed)
    {
        try
        {
            List<string> ParticipantIds = new List<string>();
            ParticipantIds.Add(UXUtility.LoggedInUser.Id);
            ParticipantIds.Add(ConnectionId);
            Model.ParticipantUserIds = ParticipantIds;
            if (isConfirmed)
            {
                HttpResponseMessage response = await ChatClientService.PutAsync(Model, "ChatDelete");
                if (response.IsSuccessStatusCode)
                {
                    SLog.Write("Chat Thread Deleted Successfully..!!");
                    navigationManager.NavigateTo($"inbox/message");
                }
                else
                {
                    throw new Exception("Critical: Unable to delete conversation..!!");
                }
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
        }
    }

    protected async void SubmitBlockUserOnNetwork()
    {
        try
        {
            UserService = new User_ClientService(EngineServices.MarketNetwork);
            User User = new User();
            User.Id = UXUtility.LoggedInUser.Id;
            User.Core.Id = UXUtility.LoggedInUser.Id;
            User.BlockedIds.Add(ConnectionId);

            HttpResponseMessage response = await UserService.PutAsync(User, "Block");
            if (response.IsSuccessStatusCode)
            {
                User.BlockedIds.Clear();
                ToastService.ShowSuccess("User Blocked On Network!");
            }

        }
        catch (Exception e)
        {
            SLog.Write(e.Message);
        }
    }


    public void Dispose()
    {
        _ = ChathubConnection.DisposeAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {

                IJSObjectReference module1 = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/composer.js");
                await module1.InvokeVoidAsync("composerJS", "msgTextarea", "#msgTextareaToolbar");
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex);
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
