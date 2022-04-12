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
    public partial class EditUserOnBoarding : BaseComponent
    {
        [Parameter] public string userAccountId { get; set; }
        public CS_FileUploader UploaderService { get; set; }

        private AccountFullInfo AccountInfo = new AccountFullInfo();
        private CS_Authentication ClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ClientService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
                UploaderService = new CS_FileUploader(ClientExchange, EngineServices.MarketNetwork);
                if (userAccountId != null)
                {
                    AccountInfo = await ClientService.GetAccountFullInfoforOnBoard(userAccountId);
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async Task UpdateUserOnBoardAccount()
        {
            try
            {
                if (string.IsNullOrEmpty(AccountInfo.AuthModel.FirstName) || string.IsNullOrEmpty(AccountInfo.AuthModel.LastName))
                {
                    //ToastService.ShowWarning("First Name Or Last Name Can't Be Empty!!");
                    return;
                }
                AccountInfo.ProfileModel.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "aboutOnboardUser");
                string response = await ClientService.UpdateUserOnboardAccountAsync(AccountInfo);
                if (!string.IsNullOrEmpty(response))
                {
                    SLog.Write("User Account Updated Successfully..!!");
                    //ToastService.ShowSuccess("User Account Has Been Updated..!!");
                    //navigationManager.NavigateTo("/root/user-onboard");
                }
                else
                {
                    //ToastService.ShowError("Account Not Updated..!!", "Error");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                //ToastService.ShowError("Exception: Something Went Wrong..!!", "Error");
            }
        }

        public List<string> Persona = new List<string>()
    { "Student Entrepreneur",
        "Founder",
        "Investor",
        "Mentor",
        "Intrapreneur",
        "Startup",
        "Incubator",
    };

        public async void Imagebase64(string src)
        {
            AccountInfo.ProfileModel.Core.ImageUrl = await UploaderService.UploadFile(src, GetType());
            StateHasChanged();
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "User Onboard","/root/user-onboard" }
    };
    }
}
