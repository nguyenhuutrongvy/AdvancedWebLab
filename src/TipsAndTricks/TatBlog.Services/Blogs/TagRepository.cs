using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs;

public class TagRepository : ITagRepository
{
	private readonly BlogDbContext _context;
	private readonly IMemoryCache _memoryCache;

	public TagRepository(BlogDbContext context, IMemoryCache memoryCache)
	{
		_context = context;
		_memoryCache = memoryCache;
	}

	public async Task<Tag> GetTagBySlugAsync(
		string slug, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Tag>()
			.FirstOrDefaultAsync(a => a.UrlSlug == slug, cancellationToken);
	}

	public async Task<Tag> GetCachedTagBySlugAsync(
		string slug, CancellationToken cancellationToken = default)
	{
		return await _memoryCache.GetOrCreateAsync(
			$"category.by-slug.{slug}",
			async (entry) =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
				return await GetTagBySlugAsync(slug, cancellationToken);
			});
	}

	public async Task<Tag> GetTagByIdAsync(int categoryId)
	{
		return await _context.Set<Tag>().FindAsync(categoryId);
	}

	public async Task<Tag> GetCachedTagByIdAsync(int categoryId)
	{
		return await _memoryCache.GetOrCreateAsync(
			$"category.by-id.{categoryId}",
			async (entry) =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
				return await GetTagByIdAsync(categoryId);
			});
	}

	public async Task<IList<TagItem>> GetTagsAsync(
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Tag>()
			.OrderBy(a => a.Name)
			.Select(a => new TagItem()
			{
				Id = a.Id,
				Name = a.Name,
				UrlSlug = a.UrlSlug,
				Description = a.Description,
				PostCount = a.Posts.Count(p => p.Published)
			})
			.ToListAsync(cancellationToken);
	}

	public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Tag>()
			.AsNoTracking()
			.WhereIf(!string.IsNullOrWhiteSpace(name), 
				x => x.Name.Contains(name))
			.Select(a => new TagItem()
			{
                Id = a.Id,
                Name = a.Name,
                UrlSlug = a.UrlSlug,
                Description = a.Description,
                PostCount = a.Posts.Count(p => p.Published)
            })
			.ToPagedListAsync(pagingParams, cancellationToken);
	}

	public async Task<IPagedList<T>> GetPagedTagsAsync<T>(
		Func<IQueryable<Tag>, IQueryable<T>> mapper,
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default)
	{
		var categoryQuery = _context.Set<Tag>().AsNoTracking();

		if (!string.IsNullOrEmpty(name))
		{
			categoryQuery = categoryQuery.Where(x => x.Name.Contains(name));
		}

		return await mapper(categoryQuery)
			.ToPagedListAsync(pagingParams, cancellationToken);
	}

    public async Task<bool> AddOrUpdateAsync(
		Tag category, CancellationToken cancellationToken = default)
	{
		if (category.Id > 0)
		{
			_context.Tags.Update(category);
			_memoryCache.Remove($"category.by-id.{category.Id}");
		}
		else
		{
			_context.Tags.Add(category);
		}

		return await _context.SaveChangesAsync(cancellationToken) > 0;
	}
	
	public async Task<bool> DeleteTagAsync(
		int categoryId, CancellationToken cancellationToken = default)
	{
		return await _context.Tags
			.Where(x => x.Id == categoryId)
			.ExecuteDeleteAsync(cancellationToken) > 0;
	}

	public async Task<bool> IsTagSlugExistedAsync(
		int categoryId, 
		string slug, 
		CancellationToken cancellationToken = default)
	{
		return await _context.Tags
			.AnyAsync(x => x.Id != categoryId && x.UrlSlug == slug, cancellationToken);
	}
}