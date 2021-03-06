using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFDemo
{
    public class InsertDemo
    {
        public InsertDemo()
        {
            using DemoDBContext con = new DemoDBContext();
            if (con.Blogs.Count() < 1)
            {
                con.Blogs.Add(new Blog()
                {
                    CreatTime = DateTime.Now,
                    Title = "第一篇博客",
                    Content = "第一篇博客内容",
                    Comments = new List<Comment>() { 
                        new Comment() { 
                        Title = "第一篇博客的第一条评论",
                        Content = "第一条评论",
                        CreatTime = DateTime.Now
                    },
                        new Comment(){
                       Title = "第一篇博客的第二条评论",
                        Content = "第二条评论",
                        CreatTime = DateTime.Now
                    } },
                    Config = new BlogConfig()
                    {
                        Tag = "第一"
                    }
                });
                con.SaveChanges();
            }
        }
    }
}
