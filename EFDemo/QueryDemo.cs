using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFDemo
{
    public class QueryDemo
    {
        public QueryDemo()
        {
            QueryFirstOrDefault();
            QueryList();
        }    

        public void QueryFirstOrDefault()
        {
            using var con = new DemoDBContext();
            var result1 = con.Blogs.FirstOrDefault();
            var result2 = (from blogdata in con.Blogs select blogdata).FirstOrDefault();
        }

        public void QueryList()
        {
            using var con = new DemoDBContext();
            // 链式查询
            var result1 = con.Blogs.ToList();

            // linq查询
            var result2 = from blogdata in con.Blogs select blogdata;

            // linq查询匿名对象
            var result3 = from blogdata in con.Blogs
                          select new
                          {
                              Id = blogdata.Id,
                              BlogName = blogdata.Title
                          };

            // linq-Inner Join
            var result4 = from blogdata in con.Blogs

                          join comment in con.Comment
                          on blogdata.Id equals comment.BlogId

                          select new
                          {
                              Id = blogdata.Id,
                              BlogName = blogdata.Title
                          };

            // linq-Left Join 左连接
            var result5 = from blogdata in con.Blogs

                          join comment in con.Comment
                          on blogdata.Id equals comment.BlogId into commentData

                          from commentResult in commentData.DefaultIfEmpty()

                          select new
                          {
                              BlogName = blogdata.Title,
                              commentTitle = commentResult.Title
                          };
            var ssss = result5.ToList();
            /* Linq-Right Join 右连接
             SELECT [b].[Title] AS [BlogName], [c].[Title] AS [commentTitle]
             FROM [Blogs] AS [b]
             LEFT JOIN [Comment] AS [c] ON [b].[Id] = [c].[BlogId]
            */
            var result6 = from comment in con.Comment

                          join blogdata in con.Blogs
                          on comment.BlogId equals blogdata.Id into blogData

                          from blogResult in blogData.DefaultIfEmpty()

                          select new
                          {
                              commentTitle = comment.Title,
                              BlogName = blogResult.Title
                          };

            /* Linq-Let 子查询
             SELECT [b].[Title], [b].[Id], [c].[Title], [c].[Content], [c].[Id]
             FROM [Blogs] AS[b]
             LEFT JOIN [Comment] AS[c] ON[b].[Id] = [c].[BlogId]
             ORDER BY [b].[Id], [c].[Id]
            */
            var result7 = from blogdata in con.Blogs

                          let comment = (from comment in con.Comment
                                        where comment.BlogId == blogdata.Id
                                        select new {
                                            commentTitle = comment.Title,
                                            commentContent = comment.Content
                                        }).ToList()
                          select new
                          {
                              BlogName = blogdata.Title,
                              commentList = comment
                          };

            var sss = result7.ToList();

            // https://docs.microsoft.com/zh-cn/ef/core/querying/tracking 
            // 不跟踪查询

            // https://docs.microsoft.com/zh-cn/ef/core/querying/related-data/explicit
            // 显式加载查询

            // https://docs.microsoft.com/zh-cn/ef/core/querying/raw-sql
            // 原始SQL查询

            // https://docs.microsoft.com/zh-cn/ef/core/querying/single-split-queries
            // 拆分查询

            // https://docs.microsoft.com/zh-cn/ef/core/querying/database-functions
            // 使用内置函数查询

            // https://docs.microsoft.com/zh-cn/ef/core/querying/user-defined-function-mapping
            // 自定义函数查询
        }
    }
}
