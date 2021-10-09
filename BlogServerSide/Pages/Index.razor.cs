using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using BlogServerSide.DataBase;
using Microsoft.Extensions.Configuration;

namespace BlogServerSide.Pages
{
    public partial class Index
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

       

        public List<Post> Posts { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();


           
                Posts = GetRecentPosts();
            



        }


        private List<Post> GetRecentPosts()
        {
            using (var context = new BlogContext(Configuration))
            {

                var categories = context.Posts.OrderByDescending(x=>x.CreatedAt).ToList();

                return categories;
            }
        }


     
        private void NavigateTo(string link)
        {
            NavManager.NavigateTo(link);
        }

    }
}