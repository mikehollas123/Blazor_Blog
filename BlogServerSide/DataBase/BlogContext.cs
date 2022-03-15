using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using System;

namespace BlogServerSide.DataBase
{

    public class BlogContext : DbContext
    {
        private const string localSqlLiteFile = "DataBase\\Blog.db";


        public BlogContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;


        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<PostComment> Comments { get; set; }
        public DbSet<Category> Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={localSqlLiteFile}");

           // optionsBuilder.UseCosmos(Configuration.GetSection("ConnectionStrings:AccountEndpoint").Value, Configuration.GetSection("ConnectionStrings:database").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToContainer("Users");


            modelBuilder.Entity<Category>().ToContainer("Categories");

            modelBuilder.Entity<Category>().HasData(new Category()
            {
                id = Guid.NewGuid().ToString(),
                Title = "Blazor",
                Slug = "blazor",
                Content = "Discussions about Blazor and this blog site."
            }); ;

            modelBuilder.Entity<Post>().ToContainer("Posts");

            modelBuilder.Entity<User>().HasData(new User()
            {
                id = "100090254159622635325",
                FirstName = "Michael",
                LastName = "Hollas",
                Email = "hollas@gmail.com",
                RegisteredAt = DateTime.Now,
                Role = "Admin"

            });

        }
    }
    //public class PostComment
    //{
    //    public User User { get; set; }
    //    public string UserId { get; set; }
    //    public Guid Id { get; set; }
    //    public string Title { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public string Content { get; set; }
    //    public Guid PostId { get; set; }

    //    public Post Post { get; set; }

    //}

    public class User
    {

        public string id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }

        public DateTime RegisteredAt { get; set; }


    }

    public class Post
    {

        public string id { get; set; }

        public string AuthorId { get; set; }

        public string CategoryId { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }

        public string? SplashImageURL { get; set; }

        //public List<PostComment> PostComments { get; set; } 

    }

    public class Category
    {

        public string id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

    }
}
