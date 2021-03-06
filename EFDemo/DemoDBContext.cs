using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFDemo
{
    public class DemoDBContext : DbContext
    {
        public DbSet<Blog> Blogs { set; get; }

        public DbSet<Comment> Comment { set; get; }

        public DbSet<BlogConfig> BlogConfig { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(@"data source=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=EFDemoDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// 博客
    /// </summary>
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatTime { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual BlogConfig Config { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }

    public class BlogConfig
    {
        [Key]
        public int Id { get; set; }

        public Guid BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public string Tag { get; set; }
    }

    /// <summary>
    /// 评论
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }

        public DateTime CreatTime { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Guid BlogId { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
