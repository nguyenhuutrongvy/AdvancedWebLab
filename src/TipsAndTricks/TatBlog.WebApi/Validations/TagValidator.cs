using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class TagValidator : AbstractValidator<TagEditModel>
    {
        public TagValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên thẻ không được để trống")
                .MaximumLength(50)
                .WithMessage("Tên thẻ tối đa 50 ký tự");
            
            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống")
                .MaximumLength(50)
                .WithMessage("UrlSlug tối đa 50 ký tự");

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Miêu tả không được để trống")
                .MaximumLength(500)
                .WithMessage("Miêu tả tối đa 500 ký tự");
        }
    }
}
