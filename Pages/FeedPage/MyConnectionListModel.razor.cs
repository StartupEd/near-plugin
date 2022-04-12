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
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using StartupEd.Engine.MarketNetwork;
namespace StartupEd.UX.Incubation
{
    public partial class MyConnectionListModel : BaseComponent
    {
        private string SearchString { get; set; }
        protected bool IsNoteAlertPopUp { get; set; }
        public List<UserFullInfo> ConnectionsFullInfo { get; set; }
        private string UserImageUrl { get; set; }
        private string ConnectionImageUrl { get; set; }
        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }
        [Parameter] public string UserName { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public EventCallback ButtonClickCallBack { get; set; }
        [Parameter]
        public Post Model { get; set; }
        private CS_Post ClientService { get; set; }
        public List<string> CheckBox { get; set; } = new List<string>();
        private bool Checked { get; set; } = false;
        protected async Task OnButtonConfirmClick(bool value)
        {
            // await module.InvokeAsync<object>("HideModal", "confirmationModal");
            if (value)
            {
                bool result = false;
                Model.IsVisibleMe = false;
                Model.IsPublic = false;
                Model.IsMyConnection = false;
                Model.IsMySpecificConnection = true;
                Model.SpecificConnectionIds = CheckBox;
                result = await ClientService.SetPrivacy(Model);
                if (result == true)
                {
                    //ToastService.ShowInfo("Privacy Updated");
                }
                else
                {
                    //ToastService.ShowError("Privacy Not Updated");
                }
            }

            IsNoteAlertPopUp = false;
            await ButtonClickCallBack.InvokeAsync(value);
        }

        public void ShowModal()
        {
            IsNoteAlertPopUp = true;
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                ProfileService = new CS_Profile(ClientExchange, EngineServices.MarketNetwork);
                ClientService = new CS_Post(ClientExchange, EngineServices.MarketNetwork);
                if (UXUtility.LoggedInUser.Id != null)
                {
                    await GetConnectionListOfUser(UXUtility.LoggedInUser.Id);
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowError(ex.Message, "Error!");
            }
        }
        protected async Task GetConnectionListOfUser(string UserId)
        {
            //Getting Connections of User..
            ConnectionsFullInfo = await UserService.GetConnectionsListFromServer(UXUtility.LoggedInUser.Id);
            StateHasChanged();

            // Getting Profile Picture Of LoggedIn User..
            Model_Profile UserProfile = await ProfileService.GetProfileByUserId(UserId);
            UserImageUrl = UserProfile.Core.ImageUrl;
        }
        private void OnConnectionChanged(string connectionId)
        {
            // Getting Profile Picture Of Connected user..
            UserFullInfo ConnectionProfile = ConnectionsFullInfo.Find(pr => pr.ProfileModel.Core.CreatorId == connectionId);
            ConnectionImageUrl = ConnectionProfile.ProfileModel.Core.ImageUrl;
        }
        //Search connection filter
        protected bool IsVisible(User Connection)
        {
            if (string.IsNullOrEmpty(SearchString))
                return true;

            if (Connection.FirstName.ToUpper().StartsWith(SearchString) || Connection.FirstName.ToLower().StartsWith(SearchString) || Connection.LastName.ToUpper().StartsWith(SearchString) || Connection.LastName.ToLower().StartsWith(SearchString))
                return true;

            else if (Connection.Persona.ToUpper().StartsWith(SearchString) || Connection.Persona.ToLower().StartsWith(SearchString))
                return true;

            return false;
        }
        void CheckboxClicked(string CheckID, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!CheckBox.Contains(CheckID))
                {
                    CheckBox.Add(CheckID);
                }
            }
            else
            {
                if (CheckBox.Contains(CheckID))
                {
                    CheckBox.Remove(CheckID);
                }
            }
        }
    }
}
