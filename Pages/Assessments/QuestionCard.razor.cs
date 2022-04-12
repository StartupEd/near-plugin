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
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

namespace StartupEd.UX.Incubation
{
    public partial class QuestionCard : BaseComponent
    {
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
    }
}
