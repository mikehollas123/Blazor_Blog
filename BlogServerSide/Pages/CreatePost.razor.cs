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

namespace BlogServerSide.Pages
{
    public partial class CreatePost
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        IDialogService DialogService { get; set; }

        public Post PostDraft { get; set; } = new Post();

        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
      
            Categories = GetCategories();
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
                context.Database.EnsureCreated();

                var categories = context.Categorys.ToList();

                return categories;
            } 
        }

        private void AddPost()
        {
            //add the post etc
        }
    }
}