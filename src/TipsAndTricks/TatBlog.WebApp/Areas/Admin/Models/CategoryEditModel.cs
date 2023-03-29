using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Slug")]
        [Remote("VerifyCategorySlug", "Categories", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
        public string UrlSlug { get; set; }

        [DisplayName("Nội dung")]
        public string Description { get; set; }
        
        [DisplayName("Hiển thị")]
        public bool ShowOnMenu { get; set; }
    }
}
