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
using System.ComponentModel.DataAnnotations;

namespace StartupEd.UX.Incubation.Pages.Dashboard
{
    public partial class DateShortingDropdown : BaseComponent
    {
        private EventCallback<string> OnChange { get; set; }
        private FilterOption filterOption { get; set; } = FilterOption.Today;
        public enum FilterOption
        {
            [Display(Name = "Today")]
            Today = 1,
            [Display(Name = "Yesterday")]
            Yesterday = 2,
            [Display(Name = "Last 7 Days")]
            Last7Days = 3,

            [Display(Name = "Last 30 Days")]
            Last30Days = 4
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IJSObjectReference module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                await module.InvokeVoidAsync("BootstrapSelectpicker");
            }
        }

        protected async Task OnChangeForNetworkusers(ChangeEventArgs e)
        {
            filterOption = (FilterOption)Enum.Parse(typeof(FilterOption), e.Value.ToString());

            if (!string.IsNullOrEmpty(filterOption.ToString()))
            {
                //Networkusers = await UserService.GetNetworkUserForControlPanel(UXUtility.LoggedInUserRole);
                //Networkusers = await UserService.GetNetworkUserForControlPanel(filterOption.ToString());
                Console.WriteLine("Selected Value One: " + filterOption);
            }
            //if (filterOption == FilterOption.Last7Days)
            //{
            //    //Networkusers = await UserService.GetNetworkUserFromServer(UXUtility.LoggedInUserRole);
            //    Console.WriteLine("Selected Value Two: " + filterOption);
            //}
            //if (filterOption == FilterOption.Last30Days)
            //{
            //    //Networkusers = await UserService.GetNetworkUserFromServer(UXUtility.LoggedInUserRole);
            //    Console.WriteLine("Selected Value Three: " + filterOption);
            //}
            //if (filterOption == FilterOption.ThisYear)
            //{
            //    //Networkusers = await UserService.GetNetworkUserFromServer(UXUtility.LoggedInUserRole);
            //    Console.WriteLine("Selected Value Four: " + filterOption);
            //}
        }
    }
}
