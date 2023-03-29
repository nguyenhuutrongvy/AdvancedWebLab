using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class CategoryFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Keyword { get; set; } = string.Empty;
    }
}
