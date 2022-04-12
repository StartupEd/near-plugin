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
    public partial class Account : BaseComponent
    {
        //    private User Model = new User();
        //    private UserService ModelService { get; set; }
        //    private List<string> TimeZoneList = new List<string>();
        //    private List<string> LanguageList = new List<string>()
        //{
        //        "English (US)","English (UK)","Español - Spanish","Filipino","Hrvatski - Croatian","Italiano - Italian","Magyar - Hungarian","Nederlands - Dutch",
        //        "Norsk - Norwegian","Polski - Polish","Português - Portuguese","Română - Romanian","Slovenčina - Slovak","Suomi - Finnish",
        //        "Svenska - Swedish","Türkçe - Turkish","Ελληνικά - Greek","Български език - Bulgarian","Русский - Russian","Српски - Serbian",
        //        "Українська мова - Ukrainian","עִבְרִית - Hebrew","اردو - Urdu (beta)","العربية - Arabic<","فارسی - Persian",
        //        "मराठी - Marathi","हिन्दी - Hindi","বাংলা - Bangla","ગુજરાતી - Gujarati","தமிழ் - Tamil","ಕನ್ನಡ - Kannada","ภาษาไทย - Thai",
        //        "한국어 - Korean","日本語 - Japanese","简体中文 - Simplified Chinese","繁體中文 - Traditional Chinese"
        //    };

        //    protected async override Task OnInitializedAsync()
        //    {
        //        try
        //        {

        //            var timeZone = TimeZoneInfo.GetSystemTimeZones();
        //            foreach (var zone in timeZone)
        //            {
        //                string TimeZone = string.Format("{0}", zone.DisplayName);
        //                TimeZoneList.Add(TimeZone);
        //            }
        //            TimeZoneList.Add("(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi");

        //            ModelService = new UserService(ClientExchange, EngineServices.MarketNetwork);
        //            Model = await ModelService.GetUserFromServer(UXUtility.LoggedInUser.Id);

        //            Model.Email = UXUtility.LoggedInUser.Email;
        //            if (Model.Settings.Language == null)
        //            {
        //                Model.Settings.Language = "English (UK)";
        //            }
        //            if (Model.Settings.TimeZone == null)
        //            {
        //                Model.Settings.TimeZone = "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi";
        //            }
        //        }
        //        catch (NullReferenceException e)
        //        {
        //            SLog.Write(e.Message);
        //        }

        //    }

        //    private void OnLanguageChange(ChangeEventArgs e)
        //    {
        //        Model.Settings.Language = e.Value.ToString();
        //    }
        //    private void OnTimeZoneChange(ChangeEventArgs e)
        //    {
        //        Model.Settings.TimeZone = e.Value.ToString();
        //    }

        //    private async void SubmitAccountInfo()
        //    {
        //        try
        //        {
        //            HttpResponseMessage response = await ModelService.PutAsync(Model);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                ToastService.ShowSuccess("Update Successful!", "Your Account Settings Updated...");
        //                SLog.Write("Account Settings Updated!");
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

    }
}
