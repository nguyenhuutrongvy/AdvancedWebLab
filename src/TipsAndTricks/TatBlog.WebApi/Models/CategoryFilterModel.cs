namespace TatBlog.WebApi.Models
{
    public class CategoryFilterModel
    {
        public string Name { get; set; }

        public string SortColumn { get; set; } = "Id";

        public string SortOrder { get; set; } = "DESC";
    }
}
