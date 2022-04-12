using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public  partial class GroupsGridCard : BaseComponent
    {
        [Parameter]
        public Model_Community GroupInfo { get; set; }
        [Parameter]
        public EventCallback<string> OnLeaveGroup { get; set; }
        [Parameter]
        public EventCallback<string> OnDeleteGroup { get; set; }
        private CS_Community GroupService { get; set; }

        protected async Task LeaveGroupAsync(string groupId)
        {
            await OnLeaveGroup.InvokeAsync(groupId);
        }

        protected async Task DeleteCommunity(string groupId)
        {
            await OnDeleteGroup.InvokeAsync(groupId);
        }
    }
}
