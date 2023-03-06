using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryItem>> GetCategoriesAsync(bool showOnMenu = false, CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _context.Set<Category>();

            if (showOnMenu)
            {
                categories = categories.Where(x => x.ShowOnMenu);
            }

            return await categories
                .OrderBy(x => x.Name)
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = x.ShowOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<TagItem>> GetPagedTagsAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var tagQuery = _context.Set<Tag>()
                .Select(x => new TagItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                });

            return await tagQuery.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IList<Post>> GetPopularArticlesAsync(int numPosts, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderByDescending(p => p.ViewCount)
                .Take(numPosts)
                .ToListAsync(cancellationToken);
        }

        public async Task<Post> GetPostAsync(int year, int month, string slug, CancellationToken cancellationToken = default)
        {
            IQueryable<Post> postsQuery = _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author);

            if (year > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
            }
            
            if (month > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
            }

            if (!string.IsNullOrWhiteSpace(slug))
            {
                postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
            }

            return await postsQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task IncreaseViewCountAsync(int postId, CancellationToken cancellationToken = default)
        {
            await _context.Set<Post>()
                .Where(x => x.Id == postId)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1), cancellationToken);
        }

        public async Task<bool> IsPostSlugExistedAsync(int postId, string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>().AnyAsync(x => x.Id != postId && x.UrlSlug == slug, cancellationToken);
        }

        public async Task<IList<Tag>> GetTagByUrlSlug(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Tag>()
                .Where(x => x.UrlSlug == slug)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<TagItem>> GetTagAndPostAmoutAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Tag> tags = _context.Set<Tag>();

            return await tags
                .Select(x => new TagItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteTagById(string id, CancellationToken cancellationToken = default)
        {
            int count = await _context.Database.ExecuteSqlRawAsync($"DELETE FROM dbo.PostTags WHERE TagsId = {id}");

            await _context.SaveChangesAsync();

            if (count > 0)
            {
                await _context.Database.ExecuteSqlRawAsync($"DELETE FROM dbo.Tags WHERE Id = {id}");
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Category>> GetCategoryByUrlSlug(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>()
                .Where(x => x.UrlSlug == slug)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<IList<Category>> GetCategoryById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>()
                .Where(x => x.Id == id)
                .ToListAsync(cancellationToken);
        }

        public async Task AddCategory(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryById(string id, CancellationToken cancellationToken = default)
        {
            int count = await _context.Database.ExecuteSqlRawAsync($"DELETE FROM dbo.PostTags WHERE TagsId = {id}");

            await _context.SaveChangesAsync();

            if (count > 0)
            {
                await _context.Database.ExecuteSqlRawAsync($"DELETE FROM dbo.Categories WHERE Id = {id}");
                await _context.SaveChangesAsync();
            }
        }

        public bool IsCategorySlugExisted(string slug, CancellationToken cancellationToken = default)
        {
            return _context.Categories.FirstOrDefault(x => x.UrlSlug.Equals(slug)) == null ? false : true;
        }
    }
}
