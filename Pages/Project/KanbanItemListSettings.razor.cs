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
using StartupEd.Engine.MarketNetwork;


namespace StartupEd.UX.Incubation
{
    public partial class KanbanItemListSettings : BaseComponent
    {
        [CascadingParameter] KanbanContainerSettings Container { get; set; }
        [Parameter] public string TaskStatus { get; set; }
        [Parameter] public Project Model { get; set; }
        [Parameter] public CS_Project ClientServices { get; set; }
        [Parameter] public EventCallback<string> OnStatusUpdated { get; set; }
        int i = 0;
        string dropclass = "";
        string arg { get; set; }

        protected override void OnParametersSet()
        {
            // Model.Clear();
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
            await Container.UpdateTask(TaskStatus);
        }

        private void HandleDragLeave()
        {
            dropclass = "";

        }

        public async Task DeleteColumn()
        {
            if (Model.BoardStatus.Count > 1)
            {
                Model.BoardStatus.Remove(TaskStatus);
                await ClientServices.PutAsync(Model);
                await OnStatusUpdated.InvokeAsync(arg);
            }
            else
            {
                //ToastService.ShowWarning("Can't Delete Column atleast one Column is required");
            }
        }
    }
}
