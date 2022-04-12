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

namespace StartupEd.UX.Incubation
{
    public partial class QuestionCardListEdit : BaseComponent
    {
        [Parameter]
        public List<Question> QuestionList { get; set; }
        public List<string> QuestionsType = new List<string>() { "Short answer", "Multiple choice" };
        public void DeleteQuestion(Question data)
        {
            QuestionList.Remove(data);
            StateHasChanged();
        }
        public void EditQuestion(Question data)
        {
            StateHasChanged();
        }
        void AddMcOptions(string QuestionId)
        {
            for (int i = 0; i < QuestionList.Count; i++)
            {
                if (QuestionList[i].Id == QuestionId)
                {
                    Random rd = new Random();
                    var RdNo = rd.Next(0, 99);
                    QuestionList[i].MultipleChoiceOption.Add(RdNo.ToString(), "Option");
                }
            }
            StateHasChanged();
        }
        void RemoveOption(string key, string QuestionId)
        {
            for (int i = 0; i < QuestionList.Count; i++)
            {
                if (QuestionList[i].Id == QuestionId)
                {
                    QuestionList[i].MultipleChoiceOption.Remove(key);
                }
            }
            StateHasChanged();
        }
        void InputChangeAsync(string value, string key, string QuestionId)
        {
            for (int i = 0; i < QuestionList.Count; i++)
            {
                if (QuestionList[i].Id == QuestionId)
                {
                    QuestionList[i].MultipleChoiceOption[key] = value;
                }
            }
        }
    }
}
