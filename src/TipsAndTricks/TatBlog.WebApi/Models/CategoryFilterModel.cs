namespace TatBlog.WebApi.Models
{
    public class CategoryFilterModel : PagingModel
    {
        public string Name { get; set; }

        public string SortOrder { get; set; } = "ASC";
    }
}
