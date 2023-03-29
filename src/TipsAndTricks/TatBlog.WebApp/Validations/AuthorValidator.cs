using FluentValidation;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class AuthorValidator : AbstractValidator<AuthorEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public AuthorValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Bạn phải nhập họ và tên cho tác giả");

            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Url Slug không được để trống");

            RuleFor(x => x.UrlSlug)
                .MustAsync(async (postModel, slug, CancellationToken) => !await blogRepository.IsAuthorSlugExistedAsync(postModel.Id, slug, default))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");

            When(x => x.Id <= 0, () =>
            {
                RuleFor(x => x.ImageFile)
                .Must(x => x is { Length: > 0 })
                .WithMessage("Bạn phải chọn hình ảnh cho tác giả");
            }).Otherwise(() =>
            {
                RuleFor(x => x.ImageFile)
                .MustAsync(SetImageIfNotExist)
                .WithMessage("Bạn phải chọn hình ảnh cho tác giả");
            });
        }

        // Kiểm tra xem tác giả có hình ảnh chưa
        // Nếu chưa có, bắt buộc người dùng phải chọn file
        private async Task<bool> SetImageIfNotExist(AuthorEditModel authorModel, IFormFile imageFile, CancellationToken cancellationToken)
        {
            // Lấy thông tin tác giả từ CSDL
            var author = await _blogRepository.GetAuthorByIdAsync(authorModel.Id);

            // Nếu tác giả đã có hình ảnh -> Không bắt buộc chọn file
            if (!string.IsNullOrWhiteSpace(author?.ImageUrl))
            {
                return true;
            }

            // Ngược lại (tác giả chưa có hình ảnh), kiểm tra xem người dùng đã chọn file hay chưa. Nếu chưa thì báo lỗi
            return imageFile is { Length: > 0 };
        }
    }
}
