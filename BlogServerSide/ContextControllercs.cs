using BlogServerSide.DataBase;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace BlogServerSide
{
    public class ContextControllercs
    {
        public ContextControllercs(IConfiguration configuration)
        {
            Configuration = configuration;
            using (var context = new BlogContext(Configuration))
            {
                context.Database.Migrate();
            }
        }


        public IConfiguration Configuration { get; set; }

        public Category? GetCategory(string catSlug)
        {
            using (var context = new BlogContext(Configuration))
            {
           

                var categories = context.Categorys.Where(x => x.Slug == catSlug).First();

                return categories;
            }

        }
        public List<Post> GetRecentPosts()
        {
            using (var context = new BlogContext(Configuration))
            {

                var categories = context.Posts.OrderByDescending(x => x.CreatedAt).ToList();

                return categories;
            }
        }
       public List<Post> GetPosts(string catId)
        {
            using (var context = new BlogContext(Configuration))
            {

                var categories = context.Posts.Where(x => x.CategoryId == catId).OrderByDescending(x => x.CreatedAt).ToList();

                return categories;
            }
        }

        public Post? GetPost(string postSlug)
        {
            using (var context = new BlogContext(Configuration))
            {


                var post = context.Posts.Where(x => x.Slug == postSlug).First();

                return post;



            }
        }
    }
}
