using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Services.Blogs;

public class CommonRepository : ICommonRepository
{
	private readonly BlogDbContext _context;
	private readonly IMemoryCache _memoryCache;

	public CommonRepository(BlogDbContext context, IMemoryCache memoryCache)
	{
		_context = context;
		_memoryCache = memoryCache;
	}

	public async Task<int> GetPostsCommonsAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Set<Post>()
			.Where(p => p.Published)
			.CountAsync(cancellationToken);
	}
	
	public async Task<int> GetUnpublishedPostsCommonsAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Set<Post>()
			.Where(p => !p.Published)
			.CountAsync(cancellationToken);
	}
	
	public async Task<int> GetCategoriesCommonsAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Set<Category>()
			.Where(p => !p.ShowOnMenu)
			.CountAsync(cancellationToken);
	}
	
	public async Task<int> GetAuthorsCommonsAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Set<Author>()
			.CountAsync(cancellationToken);
	}
}