using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;


namespace StartupEd.UX.Incubation
{
    public partial class EventCard : BaseComponent
    {
        public NavigationManager navigationManager;
        [Parameter]
        public Event Model { get; set; }
        [Parameter]
        public EventCallback<string> GetId { get; set; }
        private OneUX_SweetAlertBox ConfirmGoToProgram { get; set; }
        public IncubationProgram Incubation_Model = new IncubationProgram();
        public CS_Event ModelService { get; set; }
        public CS_IncubationProgram Incubation_ModelService { get; set; }
        public CS_Registration RegistrationService { get; set; }
        public Registration RegistrationModel = new Registration();
        public Registration UserRegistered = new Registration();
        public bool ButtonClick { get; set; } = true;
        public bool IsIPRegistered { get; set; } = false;
        public DateTime LocalDate = DateTime.UtcNow;

        protected void ShowSweetAlert2()
        {
            ConfirmGoToProgram.ShowSweetAlert();
        }

        protected void GoToProgram()
        {
            navigationManager.NavigateTo($"incubation-program/detail/{Model.IncubationProgramId}");
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                Incubation_ModelService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                RegistrationService = new CS_Registration(ClientExchange, EngineServices.MarketNetwork);
                UserRegistered = await RegistrationService.CheckUserIsRegisteredOrNot(Model.Id);
                IsIPRegistered = await RegistrationService.CheckUserRegisterationInIncubationProgram(Model.IncubationProgramId);
                if (Model.IncubationProgramId != null)
                {
                    Incubation_Model = await Incubation_ModelService.GetIncubationProgramById(Model.IncubationProgramId);
                }
                if (UserRegistered == null)
                {
                    ButtonClick = false;
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        public async void ApplyButtonClick()
        {
            try
            {

                RegistrationModel.EmailId = UXUtility.LoggedInUser.Email;
                RegistrationModel.Name = UXUtility.LoggedInUser.Name;
                RegistrationModel.ApplyDate = DateTime.UtcNow;
                RegistrationModel.EventsId = Model.Core.Id;
                HttpResponseMessage response = await RegistrationService.PostAsync(RegistrationModel, "User");
                if (response.IsSuccessStatusCode)
                {
                    ButtonClick = true;
                    StateHasChanged();
                    //ToastService.ShowSuccess("Registration Successfull");

                }
                else
                {
                    SLog.Write("Registration Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }

        protected void CallShowSweetAlertDeleteEvent(string Id)
        {
            GetId.InvokeAsync(Id);
        }
    }
}
