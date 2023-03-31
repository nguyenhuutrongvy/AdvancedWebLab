namespace TatBlog.Services.Blogs;

public interface ICommonRepository
{
    Task<int> GetPostsCommonsAsync(CancellationToken cancellationToken = default);

    Task<int> GetUnpublishedPostsCommonsAsync(CancellationToken cancellationToken = default);

    Task<int> GetCategoriesCommonsAsync(CancellationToken cancellationToken = default);

    Task<int> GetAuthorsCommonsAsync(CancellationToken cancellationToken = default);

}