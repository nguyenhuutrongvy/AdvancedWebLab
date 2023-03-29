using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class SubscribersWidget : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public SubscribersWidget(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
