using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One;

namespace StartupEd.UX.Incubation.Pages.CRMFiles.CRMKanbanBoard
{
    public partial class CRMKanbanContainer : BaseComponent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
