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
using BlogServerSide.DataBase;
using Microsoft.Extensions.Configuration;

namespace BlogServerSide.Pages
{
    public partial class Index
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Parameter]
        public string? CatSlug { get; set; }

        [Parameter]
        public string? PostSlug { get; set; }

        public List<Post> Posts { get; set; }
        public Post Post { get; set; }
        public Category? Category { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();


            if (CatSlug == null)
            {
                Posts = GetRecentPosts();
            }
            else if (PostSlug == null)
            {
                Category = GetCategory(CatSlug);
                Posts = GetPosts(Category.id);
            }
            else
            {
                Category = GetCategory(CatSlug);
                Post = GetPost(PostSlug);
            }



        }

        private Category? GetCategory(string catSlug)
        {
            using (var context = new BlogContext(Configuration))
            {


                var categories = context.Categorys.Where(x => x.Slug == catSlug).First();

                return categories;
            }

        }
        private List<Post> GetRecentPosts()
        {
            using (var context = new BlogContext(Configuration))
            {

                var categories = context.Posts.OrderByDescending(x=>x.CreatedAt).ToList();

                return categories;
            }
        }
        private List<Post> GetPosts(string catId)
        {
            using (var context = new BlogContext(Configuration))
            {

                var categories = context.Posts.Where(x => x.CategoryId == catId).OrderByDescending(x => x.CreatedAt).ToList();

                return categories;
            }
        }

        private Post? GetPost(string postSlug)
        {
            using (var context = new BlogContext(Configuration))
            {


                var post = context.Posts.Where(x => x.Slug == postSlug).First();

                return post;



            }
        }
        private void NavigateTo(string link)
        {
            NavManager.NavigateTo(link);
        }

    }
}