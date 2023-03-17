using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("URL Slug")]
        public string UrlSlug { get; set; }

        [DisplayName("Nội dung")]
        public string Description { get; set; }
        
        [DisplayName("Hiển thị")]
        public bool ShowOnMenu { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
