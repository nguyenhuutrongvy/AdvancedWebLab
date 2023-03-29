using TatBlog.Core.Constants;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs
{
    public interface IBlogRepository
    {
        Task IncreaseViewCountAsync(int postId, CancellationToken cancellationToken = default);
        
        #region Get<>

        Task<IList<Post>> GetPopularArticlesAsync(int numPosts, CancellationToken cancellationToken = default);

        Task<Post> GetPostAsync(int year, int month, string slug, CancellationToken cancellationToken = default);

        Task<Post> GetPostAsync(string slug, CancellationToken cancellationToken = default);

        Task<IList<CategoryItem>> GetCategoriesAsync(bool showOnMenu = false, CancellationToken cancellationToken = default);

        Task<Author> GetAuthorAsync(string slug, CancellationToken cancellationToken = default);

        Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default);

        Task<IList<TagItem>> GetTagsAsync(CancellationToken cancellationToken = default);


        #endregion

        #region Get<>ById

        Task<Post> GetPostByIdAsync(int postId, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<Category> GetCategoryById(int id, CancellationToken cancellationToken = default);

        Task<Author> GetAuthorByIdAsync(int authorId);

        Task<Tag> GetTagByIdAsync(int tagId);

        #endregion

        #region GetPaged

        Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(PostQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default);

        Task<IPagedList<TagItem>> GetPagedTagsAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default);

        Task<IPagedList<TagItem>> GetPagedTagsAsync(PostQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default);

        Task<IPagedList<Post>> GetPagedPostsAsync(PostQuery condition, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedPostsAsync<T>(PostQuery condition, IPagingParams pagingParams, Func<IQueryable<Post>, IQueryable<T>> mapper);

        Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default);

        Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(PostQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default);

        #endregion

        #region Is<>Existed

        Task<bool> IsPostSlugExistedAsync(int postId, string slug, CancellationToken cancellationToken = default);

        Task<bool> IsCategorySlugExistedAsync(int categoryId, string slug, CancellationToken cancellationToken = default);

        Task<bool> IsCategorySlugExistedAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> IsAuthorSlugExistedAsync(int authorId, string authorSlug, CancellationToken cancellationToken = default);

        Task<bool> IsTagSlugExistedAsync(int tagId, string tagSlug, CancellationToken cancellationToken = default);

        #endregion

        #region CreateOrUpdate<>

        Task<Category> CreateOrUpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);

        Task<Post> CreateOrUpdatePostAsync(Post post, IEnumerable<string> tags, CancellationToken cancellationToken = default);

        Task<Author> CreateOrUpdateAuthorAsync(Author author, CancellationToken cancellationToken = default);

        Task<bool> CreateOrUpdateTagAsync(Tag tag, CancellationToken cancellationToken = default);

        Task<bool> UpdatePostStatusAsync(int postId, CancellationToken cancellationToken = default);

        Task<bool> UpdateCategoryStatusAsync(int categoryId, CancellationToken cancellationToken = default);

        #endregion

        #region Delete<>

        Task<bool> DeletePostAsync(int postId, CancellationToken cancellationToken = default);

        Task<bool> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAuthorAsync(int authorId, CancellationToken cancellationToken = default);

        Task<bool> DeleteTagAsync(int tagId, CancellationToken cancellationToken = default);

        #endregion
    }
}
