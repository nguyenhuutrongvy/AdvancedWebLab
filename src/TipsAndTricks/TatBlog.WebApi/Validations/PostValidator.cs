using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class PostValidator : AbstractValidator<PostEditModel>
    {
        public PostValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("Tiêu đề bài viết không được để trống")
                .MaximumLength(500)
                .WithMessage("Tiêu đề bài viết tối đa 500 ký tự");

            RuleFor(a => a.ShortDescription)
                .NotEmpty()
                .WithMessage("Mô tả ngắn không được để trống");
            
            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Mô tả không được để trống");

            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống")
                .MaximumLength(200)
                .WithMessage("UrlSlug tối đa 200 ký tự");
        }
    }
}
