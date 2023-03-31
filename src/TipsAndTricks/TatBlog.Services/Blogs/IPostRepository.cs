using TatBlog.Core.Constants;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface IPostRepository
{
	Task<Post> GetPostBySlugAsync(
		string slug,
		CancellationToken cancellationToken = default);

	Task<Post> GetCachedPostBySlugAsync(
		string slug, CancellationToken cancellationToken = default);

	Task<Post> GetPostByIdAsync(int postId);

	Task<Post> GetCachedPostByIdAsync(int postId);

	Task<IPagedList<PostItem>> GetPagedBestPostsAsync(int amount = 1, CancellationToken cancellationToken = default);

	Task<IPagedList<PostItem>> GetPagedRandomPostsAsync(int amount = 1, CancellationToken cancellationToken = default);

	Task<IPagedList<PostItem>> GetPagedAchivePostsAsync(int amount = 1, CancellationToken cancellationToken = default);

    Task<IList<PostItem>> GetPostsAsync(
		CancellationToken cancellationToken = default);

	Task<IPagedList<PostItem>> GetPagedPostsAsync(
		PostQuery query,
		IPagingParams pagingParams,
		CancellationToken cancellationToken = default);

	Task<IPagedList<T>> GetPagedPostsAsync<T>(
		Func<IQueryable<Post>, IQueryable<T>> mapper,
		IPagingParams pagingParams,
		string name = null,
		CancellationToken cancellationToken = default);

    Task<bool> AddOrUpdateAsync(
		Post post, 
		CancellationToken cancellationToken = default);
	
	Task<bool> DeletePostAsync(
		int postId, 
		CancellationToken cancellationToken = default);

	Task<bool> IsPostSlugExistedAsync(
		int postId, string slug, 
		CancellationToken cancellationToken = default);

	Task<bool> SetImageUrlAsync(
		int postId, string imageUrl,
		CancellationToken cancellationToken = default);
}