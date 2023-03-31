using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên chuyên mục không được để trống")
                .MaximumLength(100)
                .WithMessage("Tên chuyên mục tối đa 100 ký tự");
            
            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống")
                .MaximumLength(100)
                .WithMessage("UrlSlug tối đa 100 ký tự");

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Miêu tả không được để trống")
                .MaximumLength(100)
                .WithMessage("Miêu tả tối đa 1000 ký tự");
        }
    }
}
