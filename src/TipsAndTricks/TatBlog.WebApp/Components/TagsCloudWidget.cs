using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class TagsCloudWidget : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public TagsCloudWidget(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listTags = await _blogRepository.GetTagsAsync();

            return View(listTags);
        }
    }
}
