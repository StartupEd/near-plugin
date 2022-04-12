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
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class Pricing : BaseComponent
    {
        [Parameter] public string UserId { get; set; }
        public PricingPlan Model = new PricingPlan();
        public Authentication AuthModel { get; set; } = new Authentication();

        public List<PricingPlan> MonthlySubscriptions { get; set; } = new List<PricingPlan>();
        public List<PricingPlan> AnnualSubscriptions { get; set; } = new List<PricingPlan>();
        public List<SubscriptionPlan> MonthlySubPlans { get; set; } = new List<SubscriptionPlan>();
        public List<SubscriptionPlan> AnnualSubPlans { get; set; } = new List<SubscriptionPlan>();

        public CS_Authentication AuthService { get; set; }

        public bool IsMonthlyFreeSelected { get; set; } = false;
        public bool IsMonthlyBasicSelected { get; set; } = false;
        public bool IsMonthlyProfessionalSelected { get; set; } = false;
        public bool IsYearlyFreeSelected { get; set; } = false;
        public bool IsYearlyBasicSelected { get; set; } = false;
        public bool IsYearlyProfessionalSelected { get; set; } = false;


        public decimal MonthlyFreePlanPrice { get; set; }
        public decimal MonthlyBasicPlanPrice { get; set; }
        public decimal MonthlyProPlanPrice { get; set; }
        public decimal AnnualFreePlanPrice { get; set; }
        public decimal AnnualBasicPlanPrice { get; set; }
        public decimal AnnualProPlanPrice { get; set; }

        public string CurrencySymbol { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;


        public void LoadData(string persona)
        {
            foreach (var sub in MonthlySubscriptions)
            {
                if (sub.Persona == persona)
                {
                    MonthlySubPlans = sub.Plans;
                }
            }
            foreach (var sub in AnnualSubscriptions)
            {
                if (sub.Persona == persona)
                {
                    AnnualSubPlans = sub.Plans;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                State = LoadingContainerState.Loading;
                await SyncLoggedInUserStatus();
                AuthService = new CS_Authentication(ClientExchange, EngineServices.MarketNetwork);
                AuthModel = await AuthService.GetAuthModelByUserId(UXUtility.LoggedInUser.Id);
                MonthlySubscriptions = await AuthService.GetMonthlyPlans();
                AnnualSubscriptions = await AuthService.GetAnnualPlans();
                AuthModel.Currency = Currency.USD.ToString();
                CurrencySymbol = "$";
                LoadData(AuthModel.Persona);
                State = LoadingContainerState.Loaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }

        }


        public async Task SelectPlan(Duration duration, Plans plan, decimal price)
        {
            try
            {
                switch (duration, plan)
                {
                    case (Duration.Monthly, Plans.Free):
                        if (IsMonthlyFreeSelected)
                        {
                            IsMonthlyFreeSelected = false;
                        }
                        else
                        {
                            IsMonthlyFreeSelected = true;
                            IsMonthlyBasicSelected = false;
                            IsMonthlyProfessionalSelected = false;
                            IsYearlyFreeSelected = false;
                            IsYearlyBasicSelected = false;
                            IsYearlyProfessionalSelected = false;
                        }
                        break;
                    case (Duration.Monthly, Plans.Basic):
                        if (IsMonthlyBasicSelected)
                        {
                            IsMonthlyBasicSelected = false;
                        }
                        else
                        {
                            IsMonthlyBasicSelected = true;
                            IsMonthlyFreeSelected = false;
                            IsMonthlyProfessionalSelected = false;
                            IsYearlyFreeSelected = false;
                            IsYearlyBasicSelected = false;
                            IsYearlyProfessionalSelected = false;
                        }
                        break;
                    case (Duration.Monthly, Plans.Pro):
                        if (IsMonthlyProfessionalSelected)
                        {
                            IsMonthlyProfessionalSelected = false;
                        }
                        else
                        {
                            IsMonthlyProfessionalSelected = true;
                            IsMonthlyBasicSelected = false;
                            IsMonthlyFreeSelected = false;
                            IsYearlyFreeSelected = false;
                            IsYearlyBasicSelected = false;
                            IsYearlyProfessionalSelected = false;
                        }
                        break;
                    case (Duration.Annually, Plans.Free):
                        if (IsYearlyFreeSelected)
                        {
                            IsYearlyFreeSelected = false;
                        }
                        else
                        {
                            IsYearlyFreeSelected = true;
                            IsMonthlyBasicSelected = false;
                            IsMonthlyProfessionalSelected = false;
                            IsMonthlyFreeSelected = false;
                            IsYearlyBasicSelected = false;
                            IsYearlyProfessionalSelected = false;
                        }
                        break;
                    case (Duration.Annually, Plans.Basic):
                        if (IsYearlyBasicSelected)
                        {
                            IsYearlyBasicSelected = false;
                        }
                        else
                        {
                            IsYearlyBasicSelected = true;
                            IsMonthlyBasicSelected = false;
                            IsMonthlyProfessionalSelected = false;
                            IsMonthlyFreeSelected = false;
                            IsYearlyFreeSelected = false;
                            IsYearlyProfessionalSelected = false;
                        }
                        break;
                    case (Duration.Annually, Plans.Pro):
                        if (IsYearlyProfessionalSelected)
                        {
                            IsYearlyProfessionalSelected = false;
                        }
                        else
                        {
                            IsYearlyProfessionalSelected = true;
                            IsMonthlyBasicSelected = false;
                            IsMonthlyProfessionalSelected = false;
                            IsYearlyFreeSelected = false;
                            IsYearlyBasicSelected = false;
                            IsMonthlyFreeSelected = false;
                        }
                        break;
                    default:
                        break;
                }
                StateHasChanged();
                System.Guid guid1 = System.Guid.NewGuid();
                Model.GuiId = guid1.ToString();
                AuthModel.Duration = duration.ToString();
                AuthModel.Plan = plan.ToString();
                AuthModel.PlanPrice = price;
                HttpResponseMessage response = await AuthService.PutAsync(AuthModel);
                if (response.IsSuccessStatusCode)
                {
                    //StripeCheckout(AuthModel.UserId, Model.GuiId);
                }
                else
                {
                    //ToastService.ShowError(await response.Content.ReadAsStringAsync(), "Checkout Failed");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
    }
}
