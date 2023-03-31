namespace TatBlog.WebApi.Models
{
    public class PostFilterModel : PagingModel
    {
        public string Name { get; set; }

        public string Tag { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public int PostedYear { get; set; }

        public int PostedMonth { get; set; }

        public int PostedDay { get; set; }

        public string SortOrder { get; set; } = "ASC";
    }
}
