using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBlog.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           using (var con = new BlogContext())
            {
                //con.Database.EnsureDeleted();
                //con.Database.EnsureCreated();

      

                var author = con.Users.First(x => x.FirstName == "Michael");
                var category = con.Categorys.First(x => x.Title == "Blazor");
                //con.Posts.Add(new Post()
                //{
                //    AuthorId = author.id,
                //    CategoryId = category.id,
                //    Title = "Second Post Attempt",
                //    CreatedAt = DateTime.Now,
                //    id = Guid.NewGuid().ToString(),
                //    Slug = "secondpost",
                //    Content = "<b> This is my second test post<b> <br /> Hope This works"

                //}) ;

                //con.SaveChanges();

                var postCount = con.Posts.Where(x => x.CategoryId == category.id).ToList();



                Console.WriteLine(postCount);
            }
        }
    }

    }


