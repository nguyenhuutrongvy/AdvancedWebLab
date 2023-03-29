using System.ComponentModel;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class AuthorFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; }
    }
}
