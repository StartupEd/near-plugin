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

namespace StartupEd.UX.Incubation.Pages.Settings
{
    public partial class Personal : BaseComponent
    {
        private User Model = new User();
        private CS_User ClientService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        [Parameter] public string UserId { get; set; }
        private bool IsShowEmailBox { get; set; } = false;
        private string UpdatedEmail { get; set; }
        private string PreviousEmail { get; set; }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetUserById(UserId);
                PreviousEmail = Model.Email;
            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }
        }

        private async void SubmitInfoAsync()
        {
            try
            {
                if (!UXUtility.IsNumberOrDigit(Model.ContactNo.Mobile))
                {
                    ToastService.ShowWarning("Please enter valid phone number.", "Alphabet Not Allowed");
                    return;
                }

                Model.Name = Model.FirstName + " " + Model.LastName;
                HttpResponseMessage response = await ClientService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    //ClientService.UpdateUserCache(Model);
                    //LoginService.UpdateLocalStorageForName(Model);

                    //UserActivity userActivity = new UserActivity();
                    //Activity Module = new Activity();
                    //Module.CreateActivity("Personal Details", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Name, "Personal details has been updated by you.");
                    //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    //userActivity.Activities.Add(Module);
                    //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);

                    ToastService.ShowSuccess("Success");
                    navigationManager.NavigateTo($"/settings/profile/{UserId}");
                }
                else
                {
                    ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
        private void ShowEmailBox()
        {
            IsShowEmailBox = true;
        }
        private void CloseEmailBox()
        {
            IsShowEmailBox = false;
        }
        private async void UpdateEmail()
        {
            try
            {
                if (string.IsNullOrEmpty(UpdatedEmail))
                {
                    ToastService.ShowWarning("E-Mail address can not be empty", "Please Try Again :)");
                    return;
                }
                if (!UXUtility.IsEmailValid(UpdatedEmail))
                {
                    ToastService.ShowWarning("This is not a valid email address", "Please Try Again :)");
                    return;
                }
                Model.Email = UpdatedEmail;
                //bool IsUpdated = await ClientService.PutAsync(Model,"");
                HttpResponseMessage response = await ClientService.PutAsync(Model, "UpdateUserName");
                if (response.IsSuccessStatusCode)
                {
                    IsShowEmailBox = false;
                    PreviousEmail = Model.Email;
                    ClientService.UpdateUserCache(Model);
                    await loginService.Logout();
                    ToastService.ShowSuccess("We have sent you an email for email verification", "Verify email :)");
                    navigationManager.NavigateTo("/email-verification");
                }
                else
                {
                    Model.Email = PreviousEmail;
                    UpdatedEmail = "";
                    SLog.Write(await response.Content.ReadAsStringAsync());
                    ToastService.ShowError(await response.Content.ReadAsStringAsync(), "E-Mail updation failed :)");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }

        }
        public string InputElementStyle { get; set; } = "form-control-solid form-control-lg";
        public string LabelStyle { get; set; } = "col-xl-3 col-lg-3 col-form-label";
        public string ElementWrapperStyle { get; set; } = "col-lg-9 col-xl-6";
        public string FormElementStyle { get; set; } = "row";
    }
}
