using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace StartupEd.UX.Incubation.Shared.Service
{
    public class CallBackService :ComponentBase
    {
        protected bool ShowComponent { get; set; }

        [Parameter]
        public EventCallback<string> ConfirmationChanged { get; set; }

        [Parameter] 
        public EventCallback<bool> ConfirmationClicked { get; set; }

        protected async Task OnConnectionClickedChange(bool Value)
        {
            //await SubmitChatThreadModel(Value);
            await ConfirmationClicked.InvokeAsync(Value);
        }

        protected async Task OnConnectionClickedChange(string userId)
        {
            await ConfirmationChanged.InvokeAsync(userId);
        }


    }
}
