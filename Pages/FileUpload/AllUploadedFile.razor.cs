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

namespace StartupEd.UX.Incubation
{
    public partial class AllUploadedFile : BaseComponent
    {
        public List<File> AllFiles = new List<File>();
        //public CS_FileUpload ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private string SearchString { get; set; }
        public string ProgramView { get; set; } = "Flix";

        public void ToggleView(string view)
        {

            ProgramView = view;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            State = LoadingContainerState.Loaded;
            try
            {
                //ModelService = new CS_FileUpload(ClientExchange,EngineServices.MarketNetwork);
                //AllFiles = await ModelService.GetMyFilesFromServer(UXUtility.LoggedInUser.Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }


        public async void DeleteFile(string Id)
        {
            try
            {
                //var response = await ModelService.DeleteAsync(Id);
                //AnyFile data = AllFiles.Where(f => f.Id == Id).FirstOrDefault();
                //AllFiles.Remove(data);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
{
        { "UploadedFile","/uploadedFiles" }
    };
    }
}
