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
using Newtonsoft.Json;
using StartupEd.UX.One.Shared_Classes;
using System.Net.Http;
using Microsoft.JSInterop;
using StartupEd.Engine.MarketNetwork;

namespace StartupEd.UX.Incubation.Pages.BlogPost
{
    public partial class BlogALP : BaseComponent
    {
        public Blog Model { get; set; } = new Blog();
        public List<Blog> Blogs = new List<Blog>();
        public CS_Blog ModelService { get; set; }
        public enum LoadingContainerState { Loading, Loaded, Error }
        public LoadingContainerState State;
        private string SearchString { get; set; }
        public string ProgramView { get; set; } = "Flix";
        private OneUX_SweetAlertBox ConfirmSweetAlert { get; set; }
        private BlogReactionListModal OpenReactionList { get; set; }
        private string BlogDeleteId { get; set; }
        public List<CachedUser> cachedUsers { get; set; }
        public CS_User User_clientService { get; set; }
        public List<User> Users { get; set; }
        public List<CachedUser> cachedUserss { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await SyncLoggedInUserStatus();
                State = LoadingContainerState.Loading;
                ModelService = new CS_Blog(ClientExchange,EngineServices.MarketNetwork);
                cachedUsers = new List<CachedUser>();
                User_clientService = new CS_User(ClientExchange, EngineServices.MarketNetwork);
                Users = await User_clientService.GetUserConnectionsListAsync(UXUtility.LoggedInUser.Id);
                cachedUserss = new List<CachedUser>();
                //Reactions = new List<Reaction<Model_Blog>>();   Reaction Services
                await LoadData();

            }
            catch (Exception ex)
            {
                SLog.Write(ex);
                State = LoadingContainerState.Error;
            }
        }

        protected async Task LoadData()
        {
            Blogs = await ModelService.GetAllBlogsFromServer();
            Blogs = Blogs.OrderByDescending(x => x.Core.CreatedAt).ToList();
            State = LoadingContainerState.Loaded;
            await LoadCacheForUser();
            StateHasChanged();
        }

        protected async Task CallReactionListModal(string BlogId)
        {
            Model = Blogs.Find(p => p.Id == BlogId);
            cachedUserss = await GetReactions(Model);
            //OpenReactionList.ShowModal();
        }

        public void GetSearchedString(string str)
        {

            SearchString = str;
            StateHasChanged();
        }

        protected bool IsVisible(Model_Blog blog)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return true;
            }

            if (blog.Name.ToUpper().StartsWith(SearchString) || blog.Name.ToLower().StartsWith(SearchString))
            {
                return true;
            }
            return false;
        }

        public void ToggleView(string view)
        {

            ProgramView = view;
            StateHasChanged();
        }

        private async void Bookmark(Blog Model)
        {
            bool result = false;
            result = await ModelService.BookmarkBlogById(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                ToastService.ShowInfo("Post Bookmarked");
                await LoadData();
                //await PostCallBack.InvokeAsync();
            }
            else
            {
                ToastService.ShowError("Post UnBookmarked");
            }
        }

        private async void Unbookmark(Blog Model)
        {
            bool result = false;
            result = await ModelService.UnBookmarkBlogById(Model, UXUtility.LoggedInUser.Id);
            if (result == true)
            {
                ToastService.ShowInfo("Post UnBookmarked");
                await LoadData();
                //await PostCallBack.InvokeAsync();
            }
            else
            {
                ToastService.ShowError("Post Not UnPinned");
            }
        }


        protected void ShowSweetAlert(string Id)
        {
            if (Id != null)
            {
                BlogDeleteId = Id;
            }
            ConfirmSweetAlert.ShowSweetAlert();
        }

        public async void DeleteBlog()
        {
            try
            {
                Blogs = await ModelService.DeleteObject(Blogs, BlogDeleteId);
                if (Blogs != null)
                {
                    StateHasChanged();
                    ToastService.ShowSuccess("Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message, "Exception");
            }
        }

        public Dictionary<string, string> Routes = new Dictionary<string, string>()
    {
        { "Blogs","/blog" }
    };
        public async Task LoadCacheForUser()
        {
            foreach (var Item in Blogs)
            {
                if (!cachedUsers.Any(cu => cu.UserId == Item.Core.CreatorId))
                {
                    CachedUser cachedUser = await User_clientService.CacheUser(Item.Core.CreatorId);
                    cachedUsers.Add(cachedUser);
                }
            }
            State = LoadingContainerState.Loaded;
            StateHasChanged();
        }
        public async Task<List<CachedUser>> GetReactions(Model_Blog model)
        {
            foreach (var item in model.Reactions)
            {
                if (!cachedUserss.Any(cu => cu.UserId == item.AuthorId))
                {
                    CachedUser cachedUser = await User_clientService.CacheUser(item.AuthorId);
                    cachedUserss.Add(cachedUser);
                }

            }
            return cachedUserss;

        }
    }
}
