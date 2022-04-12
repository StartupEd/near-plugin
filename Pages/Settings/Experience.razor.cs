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

namespace StartupEd.UX.Incubation.Pages.Settings
{
    //public partial class Experience : BaseComponent
    //{
    //    private User Model = new User();
    //    private CS_User ClientService { get; set; }



    //    [Parameter]
    //    public string UserId { get; set; }

    //    protected override void OnInitialized()
    //    {
    //        UserId = UserId ?? UXUtility.LoggedInUser.Id;
    //    }

    //    protected async override Task OnInitializedAsync()
    //    {
    //        try
    //        {
    //            ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
    //            Model = await ClientService.GetUserById(UserId);
    //        }
    //        catch (NullReferenceException e)
    //        {
    //            SLog.Write(e.Message);
    //        }

    //    }


    //    public async void Imagebase64(string src)
    //    {
    //        Model.AvatarImageUrl = await UploaderService.UploadFile(src);

    //    }

    //    private async void SubmitInfoAsync()
    //    {
    //        try
    //        {
    //            Model.Name = Model.FirstName + " " + Model.LastName;
    //            Model.DateOfBirth = await JSRuntime.InvokeAsync<DateTime>("DatePickerValue", "DateOfBirth");
    //            HttpResponseMessage response = await ClientService.PutAsync(Model);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                ToastService.ShowSuccess("Personal Information Updated Successfully!", " ");
    //                UXUtility.LoggedInUser.Name = Model.FirstName + " " + Model.LastName;
    //            }
    //            else
    //            {
    //                ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            SLog.Write(ex);
    //        }
    //    }

    //    public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
    //    public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
    //    public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
    //    public string FormElementStyle { get; set; } = "row";

    
}
