using Microsoft.JSInterop;
using StartupEd.UX.One;
using System.Threading.Tasks;
namespace StartupEd.UX.Incubation.Pages.InProgress
{
    public partial class OneUX_FileUploader : BaseComponent
    {

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("FileUploader");
        }
    }
}
