using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class TagsController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<TagEditModel> _validator;

        public TagsController(IBlogRepository blogRepository, IMapper mapper, IValidator<TagEditModel> validator)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index(TagFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 5)
        {
            var postQuery = _mapper.Map<PostQuery>(model);

            ViewBag.TagsList = await _blogRepository.GetPagedTagsAsync(new PagingParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = "Name",
                SortOrder = "ASC"
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var tag = id > 0 ? await _blogRepository.GetTagByIdAsync(id) : null;

            var model = tag == null ? new TagEditModel() : _mapper.Map<TagEditModel>(tag);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagEditModel model)
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

            Tag tag = await _blogRepository.GetTagByIdAsync(model.Id);

            tag = _mapper.Map(model, tag);

            await _blogRepository.CreateOrUpdateTagAsync(tag);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id = 0)
        {
            bool result = await _blogRepository.DeleteTagAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyTagSlug(int id, string urlSlug)
        {
            var slugExisted = await _blogRepository.IsTagSlugExistedAsync(id, urlSlug);

            return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        }

        public async Task<IActionResult> Filtrate(TagFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 5)
        {
            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            var postQuery = _mapper.Map<PostQuery>(model);

            ViewBag.TagsList = await _blogRepository.GetPagedTagsAsync(postQuery, new PagingParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = "Name"
            });

            return View("Index", model);
        }
    }
}
