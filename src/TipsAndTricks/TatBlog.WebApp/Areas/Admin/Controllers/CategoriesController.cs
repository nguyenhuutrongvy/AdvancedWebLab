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

            await PopulateCategoryFilterModelAsync(model);

            return View(model);
        }

        private async Task PopulateCategoryFilterModelAsync(CategoryFilterModel model)
        {
            var categories = await _blogRepository.GetCategoriesAsync();

            model.CategoryList = categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }
        private async Task PopulateCategoryEditModelAsync(CategoryEditModel model)
        {
            var categories = await _blogRepository.GetCategoriesAsync();

            model.CategoryList = categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id = 0)
        //{
        //    var post = id > 0 ? await _blogRepository.GetCategoryById(id) : null;

        //    var model = post == null ? new CategoryEditModel() : _mapper.Map<CategoryEditModel>(post);

        //    await PopulateCategoryEditModelAsync(model);

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(CategoryEditModel model)
        //{
        //    var validationResult = await _validator.ValidateAsync(model);

        //    if (!validationResult.IsValid)
        //    {
        //        validationResult.AddToModelState(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        await PopulateCategoryEditModelAsync(model);
        //        //return View(model);
        //    }

        //    //Category category = await _blogRepository.GetCategoryById(model.Id);

        //    //await _blogRepository.CreateOrUpdateCategoryAsync(category);

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
