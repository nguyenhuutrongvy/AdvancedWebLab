using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface ITagRepository
{
	Task<Tag> GetTagBySlugAsync(
		string slug,
		CancellationToken cancellationToken = default);

	Task<Tag> GetCachedTagBySlugAsync(
		string slug, CancellationToken cancellationToken = default);

	Task<Tag> GetTagByIdAsync(int categoryId);

	Task<Tag> GetCachedTagByIdAsync(int categoryId);

	Task<IList<TagItem>> GetTagsAsync(
		CancellationToken cancellationToken = default);

	Task<IPagedList<TagItem>> GetPagedTagsAsync(
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default);

	Task<IPagedList<T>> GetPagedTagsAsync<T>(
		Func<IQueryable<Tag>, IQueryable<T>> mapper,
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default);

    Task<bool> AddOrUpdateAsync(
		Tag category, 
		CancellationToken cancellationToken = default);
	
	Task<bool> DeleteTagAsync(
		int categoryId, 
		CancellationToken cancellationToken = default);

	Task<bool> IsTagSlugExistedAsync(
		int categoryId, string slug, 
		CancellationToken cancellationToken = default);
}