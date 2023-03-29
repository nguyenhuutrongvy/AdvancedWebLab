using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.Collections;
using TatBlog.Core.Constants;
using TatBlog.Core.Contracts;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.WinApp;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryEditModel> _validator;

        public CategoriesController(IBlogRepository blogRepository, IMapper mapper, IValidator<CategoryEditModel> validator)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index(CategoryFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 5)
        {
            var postQuery = _mapper.Map<PostQuery>(model);

            ViewBag.CategoriesList = await _blogRepository.GetPagedCategoriesAsync(new PagingParams
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
            var category = id > 0 ? await _blogRepository.GetCategoryById(id) : null;

            var model = category == null ? new CategoryEditModel() : _mapper.Map<CategoryEditModel>(category);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }

            if (!ModelState.IsValid)
            {
                //return View(model);
            }

            Category category = await _blogRepository.GetCategoryById(model.Id);

            category = _mapper.Map(model, category);

            await _blogRepository.CreateOrUpdateCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id = 0)
        {
            bool result = await _blogRepository.DeleteCategoryAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCategorySlug(int id, string urlSlug)
        {
            var slugExisted = await _blogRepository.IsCategorySlugExistedAsync(id, urlSlug);

            return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        }

        public async Task<IActionResult> Filtrate(CategoryFilterModel model, [FromQuery(Name = "p")] int pageNumber = 1, [FromQuery(Name = "ps")] int pageSize = 5)
        {
            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            var postQuery = _mapper.Map<PostQuery>(model);

            ViewBag.CategoriesList = await _blogRepository.GetPagedCategoriesAsync(postQuery, new PagingParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            });

            return View("Index", model);
        }

        public async Task<IActionResult> UpdateType(int id = 0)
        {
            bool result = await _blogRepository.UpdateCategoryStatusAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }
    }
}
