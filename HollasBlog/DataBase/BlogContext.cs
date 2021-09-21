using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HollasBlog.DataBase
{
    public class BlogContext :DbContext
    {
       
        public DbSet<Users> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> Comments { get; set; }
        public DbSet<Category> Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                @"AccountEndpoint=https://mikehollas.documents.azure.com:443/;AccountKey=xrfaqN2srXs96egsJY44pSM2Q2BAq6WHexKLu720kHvoOlXVrzRUwZESx0reZTpHvgk4JgS9RGpB7dXwe8dxEw==;", "blog_db");
        }
    }

    public class Users
    {
        [Key]
        public string Id {  get; set; }public string UserName { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName {  get; set; }
        public string Email {  get; set; }
        public string? Photo {  get; set; }

        public DateTime RegisteredAt { get; set; }

    public List<Post> Posts { get; set; }
    public List<PostComment> Categories {  get;}

}

    public class Post
    {   [Key]
        public int Id {  get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public List<PostComment> PostComments { get; set; } 

}

    public class Category
    {   [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        public List<Post> Posts {  get; set; }
    }
    public class PostComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        public Post Post { get; set; }

    }
}
