using System;
using System.Net.Http;
using System.Threading.Tasks;
using StartupEd.Lib.Core;
using StartupEd.UX.One;
using StartupEd.Engine.MarketNetwork;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Components.Layout.Base;
using StartupEd.UX.One.Shared_Classes;
using System.Collections.Generic;
using Newtonsoft.Json;
using StartupEd.Lib.Engine;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace StartupEd.UX.Incubation
{
    public partial class EditEvent : BaseComponent
    {
        public NavigationManager navigationManager;
        [Parameter] public string Id { get; set; }
        [Parameter] public string EventName { get; set; }

        public bool Validation = true;

        public DateRange Dates = new DateRange();

        public Event Model = new Event();
        public CS_Event ModelService { get; set; }
        public CS_FileUploader UploaderService { get; set; }
        public List<IncubationProgram> Programs { get; set; } = new List<IncubationProgram>();
        public CS_IncubationProgram ProgramService { get; set; }
        public Round Round { get; set; }
        public string data { get; set; }
        public string RoundContainer { get; set; } = "d-none";
        public string RoundCard { get; set; } = "d-none";
        public string RoundTitle { get; set; }
        public string RoundDescription { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ModelService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                UploaderService = new CS_FileUploader(ClientExchange, EngineServices.MarketNetwork);
                ProgramService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                Programs = await ProgramService.GetIncubationProgramByUserId(UXUtility.LoggedInUser.Id);
                Model = await ModelService.GetEventById(Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Events Exception");
            }
        }


        public async void SubmitModel()
        {
            try
            {
                if (!UXUtility.IsNumberOrDigit(Model.ContactInfo.PhoneNo))
                {
                    //ToastService.ShowWarning("Please enter valid phone number.", "Alphabet Not Allowed");
                    return;
                }

                var Object = await JSRuntime.InvokeAsync<object>("getDateValues", "Events");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartEventDate == "" || Dates.EndEventDate == "" || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartEventDate == null || Dates.EndEventDate == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Model.Deadlines.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Model.Deadlines.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Model.Deadlines.EventStart = Convert.ToDateTime(Dates.StartEventDate);
                Model.Deadlines.EventEnd = Convert.ToDateTime(Dates.EndEventDate);

                Model.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "eventDetail");
                if (Model.Core.Description == "<p><br></p>" || Model.ContactInfo.Name == "" || Model.ContactInfo.PhoneNo == "" || Model.ContactInfo.Email == "" || Model.Name == "")
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                HttpResponseMessage response = await ModelService.PutAsync(Model);
                if (response.IsSuccessStatusCode)
                {
                    //ToastService.ShowSuccess("Event updated successfully!", "");
                    navigationManager.NavigateTo($"/event/{Model.Id}/{Model.Name}");
                }
                else
                {
                    SLog.Write("Event Updation Failed");
                    //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");

                }

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Event Failed Exception");
            }
        }

        public async void ShowRoundForm()
        {
            RoundContainer = "d-block";
            Round = new Round();
            Round.ClearRoundValue();
            //await module.InvokeVoidAsync("ClearDateValues");
            StateHasChanged();
        }

        public void CancelRound()
        {
            RoundContainer = "d-none";
            RoundCard = "d-none";
        }

        public async void SaveRound()
        {
            var Object = await JSRuntime.InvokeAsync<object>("getDateValues", "Rounds");
            Dates = Newtonsoft.Json.JsonConvert.DeserializeObject<DateRange>(Object.ToString());
            if (Dates.StartRoundDate == "" || Dates.EndRoundDate == "" || Dates.StartRoundDate == null || Dates.EndRoundDate == null)
            {
                //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                return;
            }
            Round.StartDate = Convert.ToDateTime(Dates.StartRoundDate);
            Round.EndDate = Convert.ToDateTime(Dates.EndRoundDate);
            int index = Model.Rounds.FindIndex(item => item.Id == Round.Id);
            if (index != -1)
            {
                Model.Rounds[index] = Round;
            }
            else
            {
                System.Guid guid = System.Guid.NewGuid();
                Round.Id = guid.ToString();
                Model.Rounds.Add(Round);
            }

            RoundCard = "d-block";
            RoundContainer = "d-none";
            StateHasChanged();
        }

        public async void UpdateRound(Round data)
        {
            Console.WriteLine(data);
            RoundContainer = "d-block";
            Round = data;
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var maxAllowedFiles = 1;
            var format = "image/png";
            foreach (var imageFile in e.GetMultipleFiles(maxAllowedFiles))
            {
                string Ext = imageFile.Name.Substring(imageFile.Name.IndexOf(".") + 1);
                var resizedImageFile = await imageFile.RequestImageFileAsync(format,
                100, 100);
                var buffer = new byte[resizedImageFile.Size];
                await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                data = Ext + "." + Convert.ToBase64String(buffer);
                Model.Core.ImageUrl = await UploaderService.UploadFile(data, Model.GetType());
                StateHasChanged();
            }
        }

        public List<string> EventType = new List<string>() { "Competition", "Workshop", "Hackthon", "Webinars", "Festivals", "Scholarship", "Quizzes", "Led by Social Issue" };
        IJSObjectReference module;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {

                    module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OneUX/assets/js/script.js");

                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
            }
        }
    }
}
