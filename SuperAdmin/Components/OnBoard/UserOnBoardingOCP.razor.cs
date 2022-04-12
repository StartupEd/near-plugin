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
using System.Text.RegularExpressions;
using StartupEd.UX.One.Services;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class UserOnBoardingOCP : BaseComponent
    {
        private AccountFullInfo AccountInfo = new AccountFullInfo();
        private CS_Authentication ClientService { get; set; }

        //protected FileUploader_ClientService<Model_Profile> UploaderService { get; set; }
        private InputValidation Validation = new InputValidation();
        private RandomStringGenerator RandomGenerator = new RandomStringGenerator();

        protected override async Task OnInitializedAsync()
        {
            ClientService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
            //UploaderService = new FileUploader_ClientService<Model_Profile>(ClientExchange, EngineServices.MarketNetwork);
        }

        protected void GetEmailAddress(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(AccountInfo.AuthModel.FirstName))
            {
                //ToastService.ShowWarning("First Name can't be empty!");
                return;
            }
            if (!string.IsNullOrEmpty(firstName) && (!string.IsNullOrEmpty(lastName)))
            {
                firstName = Regex.Replace(firstName, @"\s+", "");
                lastName = Regex.Replace(lastName, @"\s+", "");
                AccountInfo.AuthModel.UserName = RandomGenerator.GenerateUserEmail(firstName, lastName);
                AccountInfo.AuthModel.UserName = Regex.Replace(AccountInfo.AuthModel.UserName, @"\s+", "");
                AccountInfo.AuthModel.Password = RandomGenerator.GetPassword();
            }
        }

        protected async Task SubmitUserAccount()
        {
            try
            {
                if (string.IsNullOrEmpty(AccountInfo.AuthModel.FirstName))
                {
                    //ToastService.ShowWarning("First Name can't be empty!");
                    return;
                }
                if (string.IsNullOrEmpty(AccountInfo.AuthModel.LastName))
                {
                    //ToastService.ShowWarning("Last Name can't be empty!");
                    return;
                }
                //if (string.IsNullOrEmpty(AccountInfo.AuthModel.Persona))
                //{
                //    ToastService.ShowWarning("Persona Role can't be empty!");
                //    return;
                //}
                if (!UXUtility.IsEmailValid(AccountInfo.AuthModel.UserName))
                {
                    //ToastService.ShowWarning("Please enter valid email..!");
                    return;
                }
                AccountInfo.ProfileModel.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "aboutUser");
                AccountInfo.AuthModel.IsClaimedAccount = false;
                AccountInfo.AuthModel.OnboardByRole = UXUtility.LoggedInUserRole.ToString();
                AccountInfo.AuthModel.IsValidAccount = false;
                AccountInfo.AuthModel.IsValidateTerms = true;
                string response = await ClientService.CreateUserOnboardAccountAsync(AccountInfo);
                if (!string.IsNullOrEmpty(response))
                {
                    SLog.Write("User Account Created Successfully..!!");
                    //ToastService.ShowSuccess("User Account Has Been Created..!!");
                    //navigationManager.NavigateTo("/root/user-onboard");
                }
                else
                {
                    //ToastService.ShowError("User Account Not Created..!!", "Error");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
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
            Model_Profile profile = new Model_Profile();
            //profile.Core.ImageUrl = await UploaderService.UploadFile(src,GetType());
            AccountInfo.ProfileModel.Core.ImageUrl = profile.Core.ImageUrl;
            StateHasChanged();
        }

        protected void OnviewCreatedUserAccounts(bool value)
        {
            if (value)
            {
                //navigationManager.NavigateTo("/root/user-onboard");
            }
        }
    }
}
