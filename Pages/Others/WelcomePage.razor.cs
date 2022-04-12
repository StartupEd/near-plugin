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

namespace StartupEd.UX.Incubation.Pages.Others
{
    public partial class WelcomePage : BaseComponent
    {
        private string PersonaName { get; set; }
        private string OtherPersonaName { get; set; }
        private string ProductName { get; set; }
        private string OtherProductName { get; set; }
        private string IcPlan { get; set; }
        private string OtherIcPlan { get; set; }
        private bool IsFirstStep { get; set; } = true;
        private bool IsSecondStep { get; set; } = false;
        private bool IsThirdStep { get; set; } = false;
        private bool IsLastStep { get; set; } = false;
        private string BorderColor = "border border-primary";
        private double Opacity = 0.7;
        private bool IsIconActive_1 { get; set; } = false;
        private bool IsIconActive_2 { get; set; } = false;
        private bool IsIconActive_3 { get; set; } = false;
        private bool IsIconActive_4 { get; set; } = false;
        private bool IsIconActive_5 { get; set; } = false;
        private bool IsIconActive_6 { get; set; } = false;
        private bool IsIconActive_7 { get; set; } = false;
        private bool IsIconActive_8 { get; set; } = false;
        private bool IsIconActive_9 { get; set; } = false;
        private bool IsIconActive_10 { get; set; } = false;
        private bool IsIconActive_11 { get; set; } = false;
        private bool IsIconActive_12 { get; set; } = false;
        private bool IsIconActive_13 { get; set; } = false;
        private bool IsIconActive_14 { get; set; } = false;
        private bool IsIconActive_15 { get; set; } = false;
        private bool IsIconActive_16 { get; set; } = false;
        private bool IsIconActive_17 { get; set; } = false;
        private bool IsIconActive_18 { get; set; } = false;
        private bool IsIconActive_19 { get; set; } = false;
        private bool IsIconActive_20 { get; set; } = false;
        private bool IsIconActive_21 { get; set; } = false;
        private bool IsIconActive_22 { get; set; } = false;
        private User UserModel = new User();
        private CS_User UserClientService { get; set; }
        public async Task HandleClick()
        {
            UserClientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
            UserModel.Persona = PersonaName;
            UserModel.Products = ProductName;
            UserModel.OtherPersona = OtherPersonaName;
            UserModel.OtherProduct = OtherProductName;
            UserModel.IcPlan = IcPlan;
            UserModel.OtherIcPlan = OtherIcPlan;
            UserModel.Id = UXUtility.LoggedInUser.Id;
            await UserClientService.PutAsync(UserModel, "SetPersonaAndProduct");
            navigationManager.NavigateTo("/home");
        }
        private async Task NextStep()
        {
            if (IsFirstStep)
            {
                IsFirstStep = false;
                IsSecondStep = true;
                IsThirdStep = false;
            }
            else if (IsSecondStep)
            {
                if ((!string.IsNullOrEmpty(PersonaName)) || (!string.IsNullOrEmpty(OtherPersonaName)))
                {
                    IsFirstStep = false;
                    IsSecondStep = false;
                    IsLastStep = false;
                    IsThirdStep = true;
                }
                else
                {
                    ToastService.ShowWarning("Select at least one role or type as per your profession", "Role :)");
                    return;
                }

            }
            else if (IsThirdStep)
            {
                IsFirstStep = false;
                IsLastStep = true;
                IsSecondStep = false;
                IsThirdStep = false;
            }
        }
        private async Task PreviousStep()
        {
            if (IsSecondStep)
            {
                IsFirstStep = true;
                IsSecondStep = false;
                IsThirdStep = false;
            }
            else if (IsThirdStep)
            {
                IsThirdStep = false;
                IsSecondStep = true;
                IsFirstStep = false;
            }
        }
        private async Task BorderColorChange(int number)
        {
            if (number == 1)
            {
                IsIconActive_1 = true;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Founder";
                OtherPersonaName = "";
            }
            if (number == 2)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = true;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Investor";
                OtherPersonaName = "";
            }
            if (number == 3)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = true;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Mentor";
                OtherPersonaName = "";
            }
            if (number == 4)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = true;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Intrapreneur";
                OtherPersonaName = "";
            }
            if (number == 5)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = true;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Startup";
                OtherPersonaName = "";
            }
            if (number == 6)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = true;
                IsIconActive_7 = false;
                IsIconActive_8 = false;
                PersonaName = "Incubator";
                OtherPersonaName = "";
            }
            if (number == 7)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = true;
                IsIconActive_8 = false;
                PersonaName = "Student Entrepreneur";
                OtherPersonaName = "";
            }
            if (number == 8)
            {
                IsIconActive_1 = false;
                IsIconActive_2 = false;
                IsIconActive_3 = false;
                IsIconActive_4 = false;
                IsIconActive_5 = false;
                IsIconActive_6 = false;
                IsIconActive_7 = false;
                IsIconActive_8 = true;
                PersonaName = "";
            }
            if (number == 9)
            {
                IsIconActive_9 = true;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Product";
                OtherProductName = "";
            }
            if (number == 10)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = true;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Strategy";
                OtherProductName = "";
            }
            if (number == 11)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = true;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Finance";
                OtherProductName = "";
            }
            if (number == 12)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = true;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Engineering";
                OtherProductName = "";
            }
            if (number == 13)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = true;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Legal";
                OtherProductName = "";
            }
            if (number == 14)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = true;
                IsIconActive_15 = false;
                IsIconActive_16 = false;
                ProductName = "Operations";
                OtherProductName = "";
            }
            if (number == 15)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = true;
                IsIconActive_16 = false;
                ProductName = "Growth";
                OtherProductName = "";
            }
            if (number == 16)
            {
                IsIconActive_9 = false;
                IsIconActive_10 = false;
                IsIconActive_11 = false;
                IsIconActive_12 = false;
                IsIconActive_13 = false;
                IsIconActive_14 = false;
                IsIconActive_15 = false;
                IsIconActive_16 = true;
                ProductName = "";
            }
            if (number == 17)
            {
                IsIconActive_17 = true;
                IsIconActive_18 = false;
                IsIconActive_19 = false;
                IsIconActive_20 = false;
                IsIconActive_21 = false;
                IsIconActive_22 = false;
                IcPlan = "Networking";
                OtherIcPlan = "";
            }
            if (number == 18)
            {
                IsIconActive_17 = false;
                IsIconActive_18 = true;
                IsIconActive_19 = false;
                IsIconActive_20 = false;
                IsIconActive_21 = false;
                IsIconActive_22 = false;
                IcPlan = "Business Management";
                OtherIcPlan = "";
            }
            if (number == 19)
            {
                IsIconActive_17 = false;
                IsIconActive_18 = false;
                IsIconActive_19 = true;
                IsIconActive_20 = false;
                IsIconActive_21 = false;
                IsIconActive_22 = false;
                IcPlan = "Business Development";
                OtherIcPlan = "";
            }
            if (number == 20)
            {
                IsIconActive_17 = false;
                IsIconActive_18 = false;
                IsIconActive_19 = false;
                IsIconActive_20 = true;
                IsIconActive_21 = false;
                IsIconActive_22 = false;
                IcPlan = "Mentorship";
                OtherIcPlan = "";
            }
            if (number == 21)
            {
                IsIconActive_17 = false;
                IsIconActive_18 = false;
                IsIconActive_19 = false;
                IsIconActive_20 = false;
                IsIconActive_21 = true;
                IsIconActive_22 = false;
                IcPlan = "Scouting";
                OtherIcPlan = "";
            }
            if (number == 22)
            {
                IsIconActive_17 = false;
                IsIconActive_18 = false;
                IsIconActive_19 = false;
                IsIconActive_20 = false;
                IsIconActive_21 = false;
                IsIconActive_22 = true;
                IcPlan = "";
            }
        }
    }
}
