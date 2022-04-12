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
using StartupEd.UX.Incubation.Pages.CRMFiles.CRMKanbanBoard;

namespace StartupEd.UX.Incubation.Pages.CRMFiles
{
    public partial class CRMKanbanItemList : BaseComponent
    {
        [CascadingParameter] CRMKanbanContainer Container { get; set; }
        [Parameter] public EventCallback<string> OnLogUpdate { get; set; }
        [Parameter] public Board BoardObject { get; set; }
        string dropclass = "";

        protected override void OnParametersSet()
        {
            //Model.Clear();
            //Model.AddRange(Container.TaskList.Where(x => x.Status == TaskStatus));
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
            //await Container.UpdateTask(TaskStatus); Kuldeep
        }
        private void HandleDragLeave()
        {
            dropclass = "";

        }
    }
}
