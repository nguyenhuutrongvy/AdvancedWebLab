using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;

        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Posts.Any())
            {
                return;
            }

            var authors = AddAuthors();
            var categories = AddCategories();
            var tags = AddTags();
            var posts = AddPosts(authors, categories, tags);

            //var posts = AddPosts(_dbContext.Authors.ToList(), _dbContext.Categories.ToList(), _dbContext.Tags.ToList());
        }

        private IList<Post> AddPosts(IList<Author> authors, IList<Category> categories, IList<Tag> tags)
        {
            var posts = new List<Post>()
            {
                new()
                {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "6 Productivity Shortcuts on Windows 10 & 11",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 9, 25, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 14,
                    Author = authors[1],
                    Category = categories[3],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },
                new()
                {
                    Title = "Azure Virtual Machines vs App Services",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 7, 19, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 6,
                    Author = authors[0],
                    Category = categories[3],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Array of object JSON deserialization",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 11, 5, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 19,
                    Author = authors[0],
                    Category = categories[5],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                }
            };

            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }

        private IList<Tag> AddTags()
        {
            var tags = new List<Tag>()
            {
                new() { Name = "Google", Description = "Google application", UrlSlug = "google" },
                new() { Name = "ASP.NET MVC", Description = "ASP.NET MVC", UrlSlug = "asp-dot-net-mvc" },
                new() { Name = "Razor Page", Description = "Razor Page", UrlSlug = "razor-page" },
                new() { Name = "Blazor", Description = "Blazor", UrlSlug = "blazor" },
                new() { Name = "Deep Learning", Description = "Deep Learning", UrlSlug = "deep-learning" },
                new() { Name = "Neural Network", Description = "Neural Network", UrlSlug = "neural-network" }
            };

            _dbContext.AddRange(tags);
            _dbContext.SaveChanges();

            return tags;
        }

        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
            {
                new() { Name = ".NET Core", Description = ".NET Core", UrlSlug="dot-net-core" },
                new() { Name = "Architecture", Description = "Architecture", UrlSlug="architecture" },
                new() { Name = "Messaging", Description = "Messaging", UrlSlug="messaging" },
                new() { Name = "OOP", Description = "Object Oriented Programming", UrlSlug="oop" },
                new() { Name = "Design Patterns", Description = "Design Patterns", UrlSlug="design-patterns" },
                new() { Name = "Programming Languages", Description = "Programming Languages", UrlSlug="programming-languages" }
            };

            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }

        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new()
                {
                    FullName = "Jason Mouth",
                    UrlSlug = "jason-mouth",
                    Email = "json@gmail.com",
                    JoinedDate = new DateTime(2022, 21, 10)
                },
                new()
                {
                    FullName = "Jessica Wonder",
                    UrlSlug = "jessica-wonder",
                    Email = "jessica665@motip.com",
                    JoinedDate = new DateTime(2020, 19, 19)
                },
                new()
                {
                    FullName = "Kathy Smith",
                    UrlSlug = "kathy-smith",
                    Email = "kathy.smith@iworld.com",
                    JoinedDate = new DateTime(2010, 9, 6)
                }
            };

            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();

            return authors;
        }
    }
}
