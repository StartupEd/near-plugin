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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class CRMKanabanBoardSeeting : BaseComponent
    {
        [CascadingParameter] CRMKanbanContainer Container { get; set; }
        [Parameter] public EventCallback UpdateData { get; set; }
        [Parameter] public Board BoardObject { get; set; }
        [Parameter] public CRM Model { get; set; }
        [Parameter] public CS_CRM _ClientService { get; set; }
        string dropclass = "";
        public bool Edit = false;

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

        public async Task DeleteBoard()
        {
            Model.Boards.Remove(BoardObject);
            HttpResponseMessage response = await _ClientService.PutAsync(Model);
            UpdateData.InvokeAsync();
        }
        public void EditBoradName()
        {
            if (Edit)
            {
                Edit = false;
            }
            else
            {
                Edit = true;
            }
        }

        public async Task UpdateModel()
        {
            try
            {
                Model.Boards.Where(b => b.Id == BoardObject.Id).Select(c => { c.Name = BoardObject.Name; return c; });
                HttpResponseMessage response = await _ClientService.PutAsync(Model);
                EditBoradName();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }
    }
}
