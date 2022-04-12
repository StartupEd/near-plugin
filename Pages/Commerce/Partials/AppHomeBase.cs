using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;
using StartupEd.UX.One;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StartupEd.UX.Incubation
{
    public class AppHomeBase : BaseComponent
    {
        public List<Subscription> Products { get; set; }
        public List<Subscription> SubscriptionPlans { get; set; }
        public List<string> CartProductIds { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Products = await LoadProducts();
            SubscriptionPlans = await LoadPlans();
            CartProductIds = await GetCartProducts();
            await base.OnInitializedAsync();
        }

        public static readonly string ProductCartKey = "ProductInfo";

        public static readonly string SubscriptionPlanKey = "SubscriptionPlan";

        //public void GetSubscriptionPlan(string Plan)
        //{
        //    SubscriptionPlan = Plan;
        //}

        private async Task<List<Subscription>> LoadPlans()
        {
            HttpClient request = await GetAuthenticatedRequest();
            string endPoint = string.Format("{0}/Product/plans", EngineServices.MarketNetwork.SystemEndpoint.Host);
            return await request.GetFromJsonAsync<List<Subscription>>(endPoint);
        }
        private async Task<List<Subscription>> LoadProducts()
        {
            HttpClient request = await GetAuthenticatedRequest();
            string endPoint = string.Format("{0}/Product/list", EngineServices.MarketNetwork.SystemEndpoint.Host);
            return await request.GetFromJsonAsync<List<Subscription>>(endPoint);
        }
        protected bool IsProductAddedToCart(string productId)
        {
            return CartProductIds.Contains(productId);
        }
        private async Task<List<string>> GetCartProducts()
        {
            string productStr = await JSRuntime.GetFromLocalStorage(ProductCartKey);
            return string.IsNullOrEmpty(productStr) ? new List<string>() :
                    productStr.Split(',').ToList();
        }
        protected async void onAddToCart(MouseEventArgs e, string productId)
        {
            var productIds = await GetCartProducts();
            if (productIds.Contains(productId))
                productIds.Remove(productId);
            else
                productIds.Add(productId);

            CartProductIds = productIds.Distinct().ToList();
            StateHasChanged();
            await JSRuntime.SetInLocalStorage(ProductCartKey, string.Join(',', CartProductIds));
        }
    }
}
