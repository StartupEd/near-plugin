using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;
using StartupEd.UX.One;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StartupEd.UX.Incubation
{
    public class CheckoutBase : BaseComponent
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        IJSObjectReference _appModule = null;
        public List<Subscription> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _appModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/checkout.js");
            Products = await LoadProducts();
            await base.OnInitializedAsync();
        }
        private async Task<List<Subscription>> LoadProducts()
        {
            //HttpClient request = await GetAuthenticatedRequest();
            string productStr = await JSRuntime.GetFromLocalStorage(AppHomeBase.ProductCartKey);
            string endPoint = string.Format("{0}/Product/list/{1}", EngineServices.MarketNetwork.SystemEndpoint.Host,
                productStr);
            return null;
            //return await request.GetFromJsonAsync<List<Subscription>>(endPoint);
        }
        protected async void OnRemoveFromCart(string productId)
        {
            var _product = Products.Where(x => x.Id == productId).FirstOrDefault();
            if (_product != null) { Products.Remove(_product); }
            StateHasChanged();
            await JSRuntime.SetInLocalStorage(AppHomeBase.ProductCartKey, string.Join(',', Products.Select(x => x.Id)));
        }
        protected async void OnCheckoutRedirect()
        {
            //HttpClient request = await GetAuthenticatedRequest();
            string productStr = await JSRuntime.GetFromLocalStorage(AppHomeBase.ProductCartKey);
            string endPoint = string.Format("{0}/checkout/create-session", EngineServices.MarketNetwork.SystemEndpoint.Host);
            Checkout payload = new Checkout();
            //payload.ProductIds = productStr.Split(',').ToList();
            //payload.ReturnUrl = _NavigationManager.BaseUri;//dont know from where return url can come. taking current page as url
            //var response = await request.PostAsJsonAsync<dynamic>(endPoint, payload);
            //string sessionId = await response.Content.ReadAsStringAsync();
            string publishKey = Configuration.GetSection("Stripe").GetSection("ApiKey").Value;
            try
            {
                //await _appModule.InvokeVoidAsync("redirectToStripeCheckout", publishKey, sessionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        protected async void StripeCheckout(string UserId,string PlanId)
        {
            //HttpClient request = await GetAuthenticatedRequest();
            string endPoint = string.Format("{0}/checkout/CreateSessionForPlans/" + PlanId + "/" + UserId, EngineServices.MarketNetwork.SystemEndpoint.Host);
            Checkout payload = new Checkout();
            //payload.ReturnUrl = _NavigationManager.BaseUri;//dont know from where return url can come. taking current page as url
            //var response = await request.PostAsJsonAsync<dynamic>(endPoint, payload);
            //string SessionId = await response.Content.ReadAsStringAsync();
            //payload.ProductIds.Add(PlanId);
            string publishKey = Configuration.GetSection("Stripe").GetSection("ApiKey").Value;
            try
            {
                IJSObjectReference _appModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./assets/js/checkout.js");
                //await _appModule.InvokeVoidAsync("redirectToStripeCheckout", publishKey, SessionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
