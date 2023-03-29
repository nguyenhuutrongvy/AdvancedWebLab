using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class PopularPostsWidget : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public PopularPostsWidget(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách bài viết nổi bật
            var posts = await _blogRepository.GetPopularArticlesAsync(3);

            return View(posts);
        }
    }
}
