using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class TagValidator : AbstractValidator<TagEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public TagValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Bạn phải nhập tên cho thẻ/ từ khóa");

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Url Slug không được để trống");

            RuleFor(x => x.UrlSlug)
                .MustAsync(async (postModel, slug, CancellationToken) => !await blogRepository.IsTagSlugExistedAsync(postModel.Id, slug, default))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");
        }
    }
}
