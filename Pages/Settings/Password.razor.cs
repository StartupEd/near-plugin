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
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;
using StartupEd.UX.One.Services;

namespace StartupEd.UX.Incubation
{
    public partial class Password : BaseComponent
    {
        [Parameter]
        public string UserId { get; set; }

        public Authentication Model = new Authentication();
        private CS_Authentication ClientService { get; set; }
        // public string OldPassword { get; set; }
        public static string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClickCallBack { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        IJSObjectReference module = null;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                string email = UXUtility.LoggedInUser.Email;
                ClientService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                Model = await ClientService.GetAuthenticationToChangePasswordAsync(UXUtility.LoggedInUser.Email);

            }
            catch (NullReferenceException e)
            {
                SLog.Write(e.Message);
            }

        }

        protected async void SubmitChangePassword()
        {
            try
            {
                if (CurrentPassword != Model.Password)
                {
                    //ToastService.ShowError("Current password is Incorrect!", " ");
                    return;
                }
                else
                {
                    if (NewPassword != ConfirmPassword)
                    {
                        //ToastService.ShowError("Password Mismatch!", " ");
                        return;
                    }
                    else
                    {
                        Model.Password = NewPassword;
                        HttpResponseMessage response = await ClientService.PutAsync(Model, "ChangePassword");
                        if (response.IsSuccessStatusCode)
                        {
                            //UserActivity userActivity = new UserActivity();
                            //Activity Module = new Activity();
                            //Module.CreateActivity("Account Password", UXUtility.LoggedInUser.Id, UXUtility.LoggedInUser.Email, "You have recently changed account password.");
                            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
                            //userActivity.Activities.Add(Module);
                            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
                            //CurrentPassword = " ";
                            //NewPassword = " ";
                            //ConfirmPassword = " ";
                            //ToastService.ShowSuccess("Update Successfully!", " ");
                        }
                        else
                        {
                            //ToastService.ShowError("Updation Failed!", await response.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }

        private async void ShowConfirmPassword()
        {
            if (ConfirmPassword != null)
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");
                await module.InvokeVoidAsync("Password", "confirmPassword", "passwordShowIcon");
            }
        }
    }
}
