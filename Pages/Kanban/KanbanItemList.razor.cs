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

namespace StartupEd.UX.Incubation
{
    public partial class KanbanItemList : BaseComponent
    {
        [CascadingParameter] KanbanContainer Container { get; set; }
        [Parameter] public string TaskStatus { get; set; }
        [Parameter] public EventCallback<string> OnTaskUpdate { get; set; }
        [Parameter] public List<User> TeamMembers { get; set; }
        [Parameter] public List<Model_Profile> Profiles { get; set; }

        int i = 0;

        List<Model_MyTask> Model = new List<Model_MyTask>();
        string dropclass = "";

        protected override void OnParametersSet()
        {
            Model.Clear();
            Model.AddRange(Container.TaskList.Where(x => x.Status == TaskStatus));
        }

        private void HandleDragEnter()
        {
            //if (TaskStatus == Container.Payload.Status) return;

            //if (AllowedStatuses != null && !AllowedStatuses.Contains(Container.Payload.Status))
            //{
            //    dropclass = "no-drop";
            //}
            //else
            //{
            //    dropclass = "can-drop";
            //}
            dropclass = "can-drop";
        }

        private async void HandleDrop()
        {
            dropclass = "";
            // if (AllowedStatuses != null && !AllowedStatuses.Contains(Container.Payload.Status)) return;
            await Container.UpdateTask(TaskStatus);
        }

        private void HandleDragLeave()
        {
            dropclass = "";

        }
    }
}
