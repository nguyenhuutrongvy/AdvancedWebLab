using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // Action này xử lý HTTP request đến trang chủ của ứng dụng web hoặc tìm kiếm bài viết theo từ khóa
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                Keyword = keyword
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }
        
        public async Task<IActionResult> Category(
            [FromRoute(Name = "slug")] string slug = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                CategorySlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }
        
        public async Task<IActionResult> Author(
            [FromRoute(Name = "slug")] string slug = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                AuthorSlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }
        
        public async Task<IActionResult> Tag(
            [FromRoute(Name = "slug")] string slug = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                TagSlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }
        
        public async Task<IActionResult> Post(
            int year,
            int month,
            int day,
            [FromRoute(Name = "slug")] string slug = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                PostSlug = slug,
                PostedYear = year,
                PostedMonth = month,
                PostedDay = day
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postsList);
        }

        public IActionResult About() => View();

        public IActionResult Contact() => View();

        public IActionResult Rss() => Content("Nội dung sẽ được cập nhật");
    }
}
