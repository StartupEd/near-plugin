using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using StartupEd.Lib.Core;
using StartupEd.Lib.Engine;
using StartupEd.UX.One;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using StartupEd.UX.One.Shared_Classes;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation
{
    public partial class EditProgram : BaseComponent
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        public IncubationProgram Program = new IncubationProgram();
        public CS_IncubationProgram ProgramService { get; set; }
        // public List<Events> EventModels { get; set; } = new List<Events>();
        public CS_Event EventService { get; set; }
        //private UserActivity_ClientService Activity_ClientService { get; set; }
        public bool Validation = true;
        public DateRange Dates = new DateRange();

        //public FileUploader_ClientService<IncubationProgram> UploaderService { get; set; }

        public Stage Stage { get; set; }
        public string data { get; set; }
        public string StageContainer { get; set; } = "d-none";
        public string StageCard { get; set; } = "d-none";
        public string StageTitle { get; set; }
        public string StageDescription { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Activity_ClientService = new UserActivity_ClientService(ClientExchange, EngineServices.MarketNetwork);
                //ProgramService = new CS_IncubationProgram(ClientExchange, EngineServices.MarketNetwork);
                //UploaderService = new FileUploader_ClientService<IncubationProgram>(ClientExchange, EngineServices.MarketNetwork);
                //EventService = new CS_Event(ClientExchange, EngineServices.MarketNetwork);
                //EventModels = await EventService.GetEventsByIncubationProgramId(Model.Id);
                Program = await ProgramService.GetIncubationProgramById(Id);
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Exception");
            }
        }

        public async void SubmitModel()
        {
            try
            {
                if (!UXUtility.IsNumberOrDigit(Program.ContactInfo.PhoneNo))
                {
                    //ToastService.ShowWarning("Please enter valid phone number.", "Only Numbers Allowed");
                    return;
                }
                var Object = await JSRuntime.InvokeAsync<object>("getProgramDateValues", "Programs");
                Dates = JsonConvert.DeserializeObject<DateRange>(Object.ToString());
                if (Dates.StartRegDate == "" || Dates.EndRegDate == "" || Dates.StartProgramDate == "" || Dates.EndProgramDate == "" || Dates.StartRegDate == null || Dates.EndRegDate == null || Dates.StartProgramDate == null || Dates.EndProgramDate == null)
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                Program.Deadlines.RegistrationStart = Convert.ToDateTime(Dates.StartRegDate);
                Program.Deadlines.RegistrationEnd = Convert.ToDateTime(Dates.EndRegDate);
                Program.Deadlines.ProgramStart = Convert.ToDateTime(Dates.StartProgramDate);
                Program.Deadlines.ProgramEnd = Convert.ToDateTime(Dates.EndProgramDate);

                Program.Core.Description = await JSRuntime.InvokeAsync<string>("getEditorValue", "programDescription");
                if (Program.Core.Description == "<p><br></p>" || Program.Name == "" || Program.ContactInfo.Email == "" || Program.ContactInfo.PhoneNo == "" || Program.ContactInfo.Name == "")
                {
                    //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                    return;
                }
                else
                {
                    HttpResponseMessage response = await ProgramService.PutAsync(Program);
                    if (response.IsSuccessStatusCode)
                    {
                        await IncubationProgramRecentActivity(Program.Id, Program.Name, "You have made changes into Incubation Program " + Program.Name);
                        //ToastService.ShowSuccess("Program updated successfully!", "");
                        //navigationManager.NavigateTo("/incubationprogram");
                    }
                    else
                    {
                        SLog.Write("Event Updation Failed");
                        //ToastService.ShowError(response.ReasonPhrase, "Server Posting Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                //ToastService.ShowError(ex.Message, "Program Failed Exception");
            }
        }

        public void ShowStageForm()
        {
            StageContainer = "d-block";
            Stage = new Stage();
            Stage.ClearStageValue();
            StateHasChanged();
        }

        public void CancelStage()
        {
            StageContainer = "d-none";
            StageCard = "d-none";
            Stage.ClearStageValue();
        }

        public async void SaveStage()
        {
            var Object = await JSRuntime.InvokeAsync<object>("getProgramDateValues", "Stages");
            Dates = Newtonsoft.Json.JsonConvert.DeserializeObject<DateRange>(Object.ToString());
            if (Dates.StartStageDate == "" || Dates.EndStageDate == "" || Dates.StartStageDate == null || Dates.EndStageDate == null)
            {
                //ToastService.ShowWarning("Please Try Again :)", "Marked fields are mandatory");
                return;
            }
            Stage.StartDate = Convert.ToDateTime(Dates.StartStageDate);
            Stage.EndDate = Convert.ToDateTime(Dates.EndStageDate);

            int index = Program.Stages.FindIndex(item => item.Name == Stage.Name); ;
            if (index != -1)
            {
                Program.Stages[index] = Stage;
            }
            else
            {
                Program.Stages.Add(Stage);
            }

            StageCard = "d-block";
            StageContainer = "d-none";
            StateHasChanged();
        }

        public void UpdateStage(Stage data)
        {
            StageContainer = "d-block";
            Stage = data;
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
                Program.Core.ImageUrl = await  UploaderService.UploadFile(data, GetType());
                StateHasChanged();

            }
        }

        protected async Task IncubationProgramRecentActivity(string Id, string Title, string description)
        {
            //UserActivity userActivity = new UserActivity();
            //Activity Module = new Activity();
            //Module.CreateActivity("Incubation Program", Id, Title, description);
            //userActivity.Core.CreatorId = UXUtility.LoggedInUser.Id;
            //userActivity.Activities.Add(Module);
            //await Activity_ClientService.SubmitUserRecentActivityAsync(userActivity);
        }
    }
}
