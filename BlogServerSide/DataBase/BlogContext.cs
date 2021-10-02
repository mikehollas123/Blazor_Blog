﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogServerSide.DataBase
{
    public class BlogContext :DbContext
    {
        public BlogContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> Comments { get; set; }
        public DbSet<Category> Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(Configuration.GetSection("ConnectionStrings:AccountEndpoint").Value, Configuration.GetSection("ConnectionStrings:database").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(t => t.Id);
            

            modelBuilder.Entity<Post>().HasKey(t => t.Id);
            modelBuilder.Entity<Post>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<PostComment>().HasKey(t=> t.Id);
            modelBuilder.Entity<PostComment>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PostComment>().Property(t => t.CreatedAt).ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>().HasKey(t=> t.Id);
            modelBuilder.Entity<Category>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }

    public class User
    {
        [Key]
        public string Id {  get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
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
        public Guid Id {  get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
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
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        public Post Post { get; set; }

    }
}
