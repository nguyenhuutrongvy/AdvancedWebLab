using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly IValidator<AuthorEditModel> _validator;

        public AuthorsController(ILogger<AuthorsController> logger, IBlogRepository blogRepository, IMediaManager mediaManager, IMapper mapper, IValidator<AuthorEditModel> validator)
        {
            _logger = logger;
            _blogRepository = blogRepository;
            _mediaManager = mediaManager;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index(AuthorFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 4)
        {
            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            var authorQuery = _mapper.Map<PostQuery>(model);

            ViewBag.AuthorsList = await _blogRepository.GetPagedAuthorsAsync(authorQuery, new PagingParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = "FullName"
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var author = id > 0 ? await _blogRepository.GetAuthorByIdAsync(id) : null;

            var model = author == null ? new AuthorEditModel() : _mapper.Map<AuthorEditModel>(author);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorEditModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                //validationResult.AddToModelState(ModelState);
            }

            if (!ModelState.IsValid)
            {
                //return View(model);
            }

            var author = model.Id > 0 ? await _blogRepository.GetAuthorByIdAsync(model.Id) : null;

            if (author == null)
            {
                author = _mapper.Map<Author>(model);

                author.Id = 0;                
            }
            else
            {
                _mapper.Map(model, author);

                author.Posts = null;
            }

            author.JoinedDate = DateTime.Now;

            if (model.ImageFile?.Length > 0)
            {
                // Thực hiện việc lưu tập tin vào thư mục uploads
                var newImagePath = await _mediaManager.SaveFileAsync(
                    model.ImageFile.OpenReadStream(),
                    model.ImageFile.FileName,
                    model.ImageFile.ContentType);

                // Nếu lưu thành công, xóa tập tin hình ảnh cũ (nếu có)
                if (!string.IsNullOrWhiteSpace(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(author.ImageUrl);
                    author.ImageUrl = newImagePath;
                }
            }

            await _blogRepository.CreateOrUpdateAuthorAsync(author);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id = 0)
        {
            bool result = await _blogRepository.DeleteAuthorAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Filtrate(AuthorFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 4)
        {
            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            var authorQuery = _mapper.Map<PostQuery>(model);

            ViewBag.AuthorsList = await _blogRepository.GetPagedAuthorsAsync(authorQuery, new PagingParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = "FullName"
            });

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyAuthorSlug(int id, string urlSlug)
        {
            var slugExisted = await _blogRepository.IsAuthorSlugExistedAsync(id, urlSlug);

            return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        }
    }
}
