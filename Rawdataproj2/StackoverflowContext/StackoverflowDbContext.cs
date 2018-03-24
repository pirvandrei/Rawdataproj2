using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text; 

namespace StackoverflowContext
{ 
    class StackoverflowDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        //public DbSet<Posttag> Posttags { get; set; }
        //public DbSet<Answer> Answers { get; set; }
        //public DbSet<Question> Questions { get; set; }
        //public DbSet<Comment> Comments { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        //public DbSet<Bookmark> Bookmarks { get; set; }
        //public DbSet<Note> Notes { get; set; }

        //public DbSet<Link> Links { get; set; }
        //public DbSet<Search> Searches { get; set; } 
         


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("server=wt-220.ruc.dk;database=raw2;uid=aip;pwd=aip_325");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("ID");
            modelBuilder.Entity<Post>().Property(x => x.Score).HasColumnName("Score"); 
        }
    }
}
