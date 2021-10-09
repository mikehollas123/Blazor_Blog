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
using Microsoft.Extensions.Configuration;
using BlogServerSide.DataBase;

namespace BlogServerSide.Pages
{
    public partial class ShowPosts : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }



        [Parameter]
        public string? PostSlug { get; set; }


        public Post Post { get; set; }
  



        protected override void OnParametersSet()
        {
            base.OnParametersSet();


                
                Post = GetPost(PostSlug);
            



        }

    

        private Post? GetPost(string postSlug)
        {
            using (var context = new BlogContext(Configuration))
            {

                try
                {
  var post = context.Posts.Where(x => x.Slug == postSlug).First();

                return post;
                }
                catch
                {
                    return null;
                }
              



            }
        }
        private void NavigateTo(string link)
        {
            NavManager.NavigateTo(link);
        }
    }
}