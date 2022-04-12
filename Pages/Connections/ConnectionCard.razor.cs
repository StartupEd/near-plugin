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
using StartupEd.UX.Incubation.Shared.Pages;
using StartupEd.Engine.MarketNetwork;
using StartupEd.UX.One.Services;

namespace StartupEd.UX.Incubation
{
    public partial class ConnectionCard : BaseComponent
    {
        private ReportModal ConfirmReportRequest { get; set; }
        [Parameter] public EventCallback<string> OnMessageClick { get; set; }
        [Parameter] public EventCallback OnConnectionClick { get; set; }
        [Parameter] public User Connection { get; set; }
        [Parameter] public Profile Profile { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private CS_User UserService { get; set; }
        private CS_Profile ProfileService { get; set; }
        private CS_ConnectionRequest ConnectionService { get; set; }
        private ConnectionRequest RequestModel = new ConnectionRequest();
        private CS_ChatThread ClientService { get; set; }

        private Report ReportModel = new Report();
        private CS_Report ReportService { get; set; }
        private string ElementDisabled { get; set; } = "";

        protected void CallUserReportModal()
        {
            ConfirmReportRequest.ShowReportModal();
        }

        protected async Task OnMessageClickCallBack(string Value)
        {
            //await SubmitChatThreadModel(Value);
            //await OnMessageClick.InvokeAsync(Value);
        }

        protected async Task SubmitChatThreadModel(string Value)
        {
            ClientService = new CS_ChatThread(ClientExchange, EngineServices.MarketNetwork);
            try
            {
                ChatThread ThreadModel = new ChatThread();
                List<string> ParticipantIds = new List<string>();
                ParticipantIds.Add(UXUtility.LoggedInUser.Id);
                ParticipantIds.Add(Connection.Id);
                ThreadModel.ParticipantUserIds = ParticipantIds;

                HttpResponseMessage response = await ClientService.PostAsync(ThreadModel, "CreateThread");
                if (response.IsSuccessStatusCode)
                {
                    await OnMessageClick.InvokeAsync(Value);
                    //navigationManager.NavigateTo($"inbox/message/{Connection.Id}");
                    SLog.Write("Participants Chat Thread Created Successfully..!!");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }
        }

        protected async void RemoveConnectionAsync()
        {
            try
            {
                ConnectionService = new CS_ConnectionRequest(ClientExchange, EngineServices.MarketNetwork);
                string loggedinUserId = UXUtility.LoggedInUser.Id;
                RequestModel.FromUserId = loggedinUserId;
                RequestModel.ToUserId = Connection.Id;
                RequestModel.Status = ConnectionStatus.Removed;
                var result = await ConnectionService.RemoveConnectionFromUserConnections(RequestModel);
                if (result == true)
                {
                    await OnConnectionClick.InvokeAsync();
                    //ToastService.ShowSuccess("Connection Has Been Removed..!", "Successful");
                }
                else
                {
                    //ToastService.ShowError("Unble To Respond Your Request!", "Error");
                }
            }
            catch (Exception ex)
            {
                SLog.Write(ex.Message);
            }

        }

        protected async void SubmitBlockUserOnNetwork()
        {
            try
            {
                UserService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                User User = new User();
                User.Id = UXUtility.LoggedInUser.Id;
                User.Core.Id = UXUtility.LoggedInUser.Id;
                User.BlockedIds.Add(Connection.Id);

                HttpResponseMessage response = await UserService.PutAsync(User, "Block");
                if (response.IsSuccessStatusCode)
                {
                    await OnConnectionClick.InvokeAsync();
                    User.BlockedIds.Clear();
                    //ToastService.ShowSuccess("User Blocked!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
            }
        }

        protected async void SubmitProfileReport(string Value)
        {
            try
            {
                ReportService = new CS_Report(ClientExchange, EngineServices.MarketNetwork);
                if (!string.IsNullOrEmpty(Value))
                {
                    ReportModel.Core.CreatorId = UXUtility.LoggedInUser.Id;
                    ReportModel.ReportedId = Connection.Id;
                    ReportModel.Content = Value.TrimEnd(';');
                    HttpResponseMessage response = await ReportService.PostAsync(ReportModel, "CreateReport");
                    if (response.IsSuccessStatusCode)
                    {
                        //ToastService.ShowSuccess("Report has been submitted..");
                    }
                    else
                    {
                        //ToastService.ShowError("You Can't Able To Report", "Something Wrong!");
                    }
                }
                else
                {
                    //.ShowInfo("Please Select Option(s) to Report.", "Empty Option!");
                }
            }
            catch (Exception e)
            {
                SLog.Write(e.Message);
                //ToastService.ShowError(e.Message, "Report Exception!");
            }
        }
    }
}
