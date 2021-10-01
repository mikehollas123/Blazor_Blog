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

namespace BlogServerSide.Pages
{
    public partial class CreatePost
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Categories = GetCategories();
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

        private void addCategory()
        {
            using (var context = new BlogContext(Configuration))
            {
                context.Database.EnsureCreated();

               context.Categorys.Add( new Category() { Title = "Blazor", Slug="blazor", Content="some stuff about blazor"});


                context.SaveChanges();
            }

            this.Categories = GetCategories();

        }
    }
}