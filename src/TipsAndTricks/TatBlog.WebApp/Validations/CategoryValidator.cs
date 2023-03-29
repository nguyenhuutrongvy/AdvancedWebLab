using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public CategoryValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Bạn phải nhập tên cho chủ đề");

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Url Slug không được để trống");

            RuleFor(x => x.UrlSlug)
                .MustAsync(async (categoryModel, slug, CancellationToken) => !await blogRepository.IsCategorySlugExistedAsync(categoryModel.Id, slug, default))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");
        }
    }
}
