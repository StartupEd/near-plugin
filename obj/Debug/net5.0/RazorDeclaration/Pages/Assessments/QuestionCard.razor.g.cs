// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace StartupEd.UX.Incubation.Pages.Assessments
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
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Assessments/_Imports.razor"
using StartupEd.UX.One.Components.Custom.BrowserFlix;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Assessments/_Imports.razor"
using StartupEd.UX.One.Shared_Classes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Assessments/_Imports.razor"
using StartupEd.Engine.MarketNetwork.Assessment;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Assessments/_Imports.razor"
[AllowAnonymous]

#line default
#line hidden
#nullable disable
    public partial class QuestionCard : BaseComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 94 "/Users/ankitbuti/workspace/bigapple_corev3/UXs/Incubation/Pages/Assessments/QuestionCard.razor"
       
    [Parameter]
    public Question QuestionData { get; set; }
    public QuestionType Type { get; set; } = QuestionType.ShortAnswer;
    [Parameter]
    public EventCallback<MouseEventArgs> OnClickCallback1 { get; set; }
    [Parameter]
    public EventCallback<MouseEventArgs> OnClickCallback2 { get; set; }
    [Parameter]
    public EventCallback<int> RemoveQuestionClick { get; set; }
    public List<string> QuestionsType = new List<string>() { "Short Answer", "Multiple Choice" };
    public int optiValue { get; set; } = 1;
    public bool Validation = true;
    
    protected async Task RemoveQuestion(int value)
    {
        await RemoveQuestionClick.InvokeAsync(value);
    }

    void QuestionTypeChange(ChangeEventArgs e)
    {
        try
        {
            Type = (QuestionType)Enum.Parse(typeof(QuestionType), e.Value.ToString());
            QuestionData.value = Type;
            Random rd = new Random();
            var RdNo = rd.Next(0, 99);
            if (QuestionData.value == QuestionType.MultipleChoice)
            {
                QuestionData.MultipleChoiceOption.Add(RdNo.ToString(), "Option");
            }
            else
            {
                QuestionData.MultipleChoiceOption.Clear();
            }
        }
        catch (Exception ex)
        {
            SLog.Write(ex.Message);
            Type = QuestionType.ShortAnswer;
        }
    }

    void AddMcOptions()
    {
        Random rd = new Random();
        var RdNo = rd.Next(0, 99);
        QuestionData.MultipleChoiceOption.Add(RdNo.ToString(), "Option");
    }

    void InputChangeAsync(string value, string key)
    {
        QuestionData.MultipleChoiceOption[key] = value;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IJSObjectReference module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
            await module.InvokeVoidAsync("AssessmentBootstrapSelect");
        }
    }
    void RemoveOption(string key)
    {
        QuestionData.MultipleChoiceOption.Remove(key);
        StateHasChanged();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager Navigation { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVersionInfoService VersionService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILoginService loginService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IServiceProvider MyServiceProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
