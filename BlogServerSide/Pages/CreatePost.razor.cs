using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using BlogServerSide;
using BlogServerSide.Shared;
using MudBlazor;
using BlogServerSide.DataBase;
using Microsoft.Extensions.Configuration;
using BlogServerSide.Dialogs;
using System.Security.Claims;

namespace BlogServerSide.Pages
{
    public partial class CreatePost
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject] NavigationManager  NavigationManager {  get; set; }

        [Inject]
        IDialogService DialogService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider {  get; set; }

        public Post PostDraft { get; set; } = new Post();

        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        private string _userId;
  
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            Categories = GetCategories();
            var state =  await AuthenticationStateProvider.GetAuthenticationStateAsync();

             _userId = state.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }


        public async void OnCategoryAdd()
        {
            var dialog = DialogService.Show<AddCategoryDialog>("Add Category");
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                this.Categories = GetCategories();

                this.SelectedCategory = result.Data as Category;
            }
        }

        private List<Category> GetCategories()
        {
            using (var context = new BlogContext(Configuration))
            {
              

                var categories = context.Categorys.ToList();

                return categories;
            } 
        }

        private async void AddPost()
        {
            //add the post etc

            using (var context = new BlogContext(Configuration))
            {
          
                PostDraft.id = Guid.NewGuid().ToString();
                PostDraft.CreatedAt = DateTime.Now;
                PostDraft.AuthorId = (await GetUser(_userId)).id;
                PostDraft.CategoryId = SelectedCategory.id;
           
            


                context.Posts.Add(PostDraft);
                context.SaveChanges();
            }


            NavigationManager.NavigateTo("/");
        }
        public async Task<User> GetUser(string userID)
        {
           return await Task.Run(() => 
            { 
             using (var context = new BlogContext(Configuration))
            {
                return context.Users.FirstOrDefault(x => x.id == userID);
            }
            });
           
        }
    }
}