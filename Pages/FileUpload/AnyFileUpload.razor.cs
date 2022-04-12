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
    public partial class AnyFileUpload : BaseComponent
    {
        //public File Model = new File();
        public string data { get; set; }
        public string fileurl { get; set; }
        public CS_FileUploader UploaderService { get; set; }
        public CS_FileUpload ModelService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            UploaderService = new CS_FileUploader(ClientExchange, EngineServices.MarketNetwork);
            ModelService = new CS_FileUpload(ClientExchange, EngineServices.MarketNetwork);
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            try
            {
                var maxAllowedFiles = 1;
                foreach (var File in e.GetMultipleFiles(maxAllowedFiles))
                {
                    string Ext = File.Name.Substring(File.Name.IndexOf(".") + 1);

                    var buffer = new byte[File.Size];
                    await File.OpenReadStream(5242880).ReadAsync(buffer);
                    //Model.Type = File.ContentType;
                    //Model.Size = File.Size;
                    //Model.Name = File.Name;
                    data = Ext + "." + Convert.ToBase64String(buffer);
                }
                e = null;
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
                //ToastService.ShowWarning("your File is grather the 5 Mb");
            }
        }


        public async Task SubmitModel()
        {
            try
            {
                if (data == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Upload File .....");
                    return;
                }
                //Model.Core.CreatorId = UXUtility.LoggedInUser.Id;
                //Model.UploadUrl = await UploaderService.UploadFile(data,GetType());
                //HttpResponseMessage response = await ModelService.PostAsync(Model);
                //if (response.IsSuccessStatusCode)
                //{
                    //ToastService.ShowSuccess("File UploadFailed successfully!", "");
                    //navigationManager.NavigateTo("/uploadedFiles");
                //}
                //else
                //{
                //    SLog.Write("File UploadFailed  Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");

                //}
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Event Failed Exception");
            }
        }


        public Dictionary<string, string> Routes = new Dictionary<string, string>()
    {
        { "UploadFile","/anyupload" }
    };
    }
}
