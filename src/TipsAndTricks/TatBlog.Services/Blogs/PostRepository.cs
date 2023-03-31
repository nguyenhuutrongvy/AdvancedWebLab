using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using TatBlog.Core.Constants;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;
using TatBlog.Services.Timing;
using TatBlog.WinApp;

namespace TatBlog.Services.Blogs;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public PostRepository(BlogDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<Post> GetPostBySlugAsync(
        string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .FirstOrDefaultAsync(a => a.UrlSlug == slug, cancellationToken);
    }

    public async Task<Post> GetCachedPostBySlugAsync(
        string slug, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            $"post.by-slug.{slug}",
            async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await GetPostBySlugAsync(slug, cancellationToken);
            });
    }

    public async Task<Post> GetPostByIdAsync(int postId)
    {
        return await _context.Set<Post>().FindAsync(postId);
    }

    public async Task<Post> GetCachedPostByIdAsync(int postId)
    {
        return await _memoryCache.GetOrCreateAsync(
            $"post.by-id.{postId}",
            async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await GetPostByIdAsync(postId);
            });
    }

    public async Task<IPagedList<PostItem>> GetPagedBestPostsAsync(int amount = 1, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AsNoTracking()
            .OrderByDescending(p => p.ViewCount)
            .Take(amount)
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Meta = a.Meta,
                UrlSlug = a.UrlSlug,
                ImageUrl = a.ImageUrl,
                ViewCount = a.ViewCount,
                Published = a.Published,
                PostedDate = a.PostedDate,
                ModifiedDate = a.ModifiedDate,
                CategoryId = a.CategoryId,
                AuthorId = a.AuthorId,
                Category = a.Category,
                Author = a.Author,
                Tags = a.Tags
            })
            .ToPagedListAsync(new PagingParams
            {
                PageSize = amount,
                PageNumber = 1,
                SortColumn = "Id",
                SortOrder = "ASC"
            }, cancellationToken);
    }

    public async Task<IPagedList<PostItem>> GetPagedRandomPostsAsync(int amount = 1, CancellationToken cancellationToken = default)
    {
        Random rand = new Random();
        int skipper = rand.Next(0, _context.Posts.Count());

        return await _context.Posts
            .AsNoTracking()
            .OrderBy(p => Guid.NewGuid())
            .Skip(skipper)
            .Take(amount)
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Meta = a.Meta,
                UrlSlug = a.UrlSlug,
                ImageUrl = a.ImageUrl,
                ViewCount = a.ViewCount,
                Published = a.Published,
                PostedDate = a.PostedDate,
                ModifiedDate = a.ModifiedDate,
                CategoryId = a.CategoryId,
                AuthorId = a.AuthorId,
                Category = a.Category,
                Author = a.Author,
                Tags = a.Tags
            })
            .ToPagedListAsync(new PagingParams
            {
                PageSize = amount,
                PageNumber = 1,
                SortColumn = "Id",
                SortOrder = "ASC"
            }, cancellationToken);
    }

    public async Task<IPagedList<PostItem>> GetPagedAchivePostsAsync(int month = 1, CancellationToken cancellationToken = default)
    {
        LocalTimeProvider time = new LocalTimeProvider();

        int count = _context.Set<Post>()
            .AsEnumerable()
            .OrderByDescending(p => p.PostedDate)
            .Where(p => (new TimeSpan(time.Now.Year - p.PostedDate.Year)).TotalDays <= 365 &&
                        (new TimeSpan(time.Now.Month - p.PostedDate.Month)).TotalDays <= month * 30).Count();

        return await _context.Set<Post>()
            .AsNoTracking()
            .OrderByDescending(p => p.PostedDate)
            .Take(month)
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Meta = a.Meta,
                UrlSlug = a.UrlSlug,
                ImageUrl = a.ImageUrl,
                ViewCount = a.ViewCount,
                Published = a.Published,
                PostedDate = a.PostedDate,
                ModifiedDate = a.ModifiedDate,
                CategoryId = a.CategoryId,
                AuthorId = a.AuthorId,
                Category = a.Category,
                Author = a.Author,
                Tags = a.Tags
            })
            .ToPagedListAsync(new PagingParams
            {
                PageSize = count,
                PageNumber = 1,
                SortColumn = "Id",
                SortOrder = "DESC"
            }, cancellationToken);
    }

    public async Task<IList<PostItem>> GetPostsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .OrderBy(a => a.Title)
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Meta = a.Meta,
                UrlSlug = a.UrlSlug,
                ImageUrl = a.ImageUrl,
                ViewCount = a.ViewCount,
                Published = a.Published,
                PostedDate = a.PostedDate,
                ModifiedDate = a.ModifiedDate,
                CategoryId = a.CategoryId,
                AuthorId = a.AuthorId,
                Category = a.Category,
                Author = a.Author,
                Tags = a.Tags
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<PostItem>> GetPagedPostsAsync(
        PostQuery query,
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags)
            .WhereIf(!string.IsNullOrWhiteSpace(query.Keyword), x => x.Title.ToLower().Contains(query.Keyword.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(query.PostedYear.ToString()), x => x.PostedDate.Year == query.PostedYear)
            .WhereIf(!string.IsNullOrWhiteSpace(query.PostedMonth.ToString()), x => x.PostedDate.Month == query.PostedMonth)
            .WhereIf(!string.IsNullOrWhiteSpace(query.PostedDay.ToString()), x => x.PostedDate.Day == query.PostedDay)
            .Select(a => new PostItem()
            {
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                Meta = a.Meta,
                UrlSlug = a.UrlSlug,
                ImageUrl = a.ImageUrl,
                ViewCount = a.ViewCount,
                Published = a.Published,
                PostedDate = a.PostedDate,
                ModifiedDate = a.ModifiedDate,
                CategoryId = a.CategoryId,
                AuthorId = a.AuthorId,
                Category = a.Category,
                Author = a.Author,
                Tags = a.Tags
            })
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<IPagedList<T>> GetPagedPostsAsync<T>(
        Func<IQueryable<Post>, IQueryable<T>> mapper,
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default)
    {
        var postQuery = _context.Set<Post>().AsNoTracking();

        if (!string.IsNullOrEmpty(name))
        {
            postQuery = postQuery.Where(x => x.Title.Contains(name));
        }

        return await mapper(postQuery)
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<bool> AddOrUpdateAsync(
        Post post, CancellationToken cancellationToken = default)
    {
        post.ModifiedDate = DateTime.Now;

        if (post.Id > 0)
        {
            post.PostedDate = DateTime.Now;
            _context.Posts.Update(post);
            _memoryCache.Remove($"post.by-id.{post.Id}");
        }
        else
        {
            post.PostedDate = DateTime.Now;
            _context.Posts.Add(post);
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeletePostAsync(
        int postId, CancellationToken cancellationToken = default)
    {
        return await _context.Posts
            .Where(x => x.Id == postId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<bool> IsPostSlugExistedAsync(
        int postId,
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Posts
            .AnyAsync(x => x.Id != postId && x.UrlSlug == slug, cancellationToken);
    }

    public async Task<bool> SetImageUrlAsync(
        int postId, string imageUrl,
        CancellationToken cancellationToken = default)
    {
        return await _context.Posts
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(a => a.ImageUrl, a => imageUrl),
                cancellationToken) > 0;
    }
}