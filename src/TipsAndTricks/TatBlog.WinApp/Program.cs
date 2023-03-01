using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;
using static System.Console;

var context = new BlogDbContext();

#region P16_Exec

//var posts = context.Posts.ToList();
//var categories = context.Categories.ToList();
//var authors = context.Authors.ToList();

//WriteLine("{0,-4}{1,-45}{2,-20}{3,-20}", "ID", "Title", "Category", "Author");

//foreach (var post in posts)
//{
//    WriteLine("{0,-4}{1,-45}{2,-20}{3,-20}", post.Id, post.Title, categories.FirstOrDefault(x => x.Id == post.CategoryId).Name, authors.FirstOrDefault(x => x.Id == post.AuthorId).FullName);
//}

#endregion

#region P16_Tutorial

//var seeder = new DataSeeder(context);

//seeder.Initialize();

//var authors = context.Authors.ToList();

//WriteLine("{0,-4}{1,-30}{2,-30}{3,12}", "ID", "Full Name", "Email", "Joined Date");

//foreach (var auth in authors)
//{
//    WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}", auth.Id, auth.FullName, auth.Email, auth.JoinedDate);
//}

#endregion

#region P18_Tutorial

//var posts = context.Posts
//    .Where(p => p.Published)
//    .OrderBy(p => p.Title)
//    .Select(p => new
//    {
//        Id = p.Id,
//        Title = p.Title,
//        ViewCount = p.ViewCount,
//        PostedDate = p.PostedDate,
//        Author = p.Author.FullName,
//        Category = p.Category.Name
//    })
//    .ToList();

//foreach (var post in posts)
//{
//    WriteLine($"ID      : {post.Id}");
//    WriteLine($"Title   : {post.Title}");
//    WriteLine($"View    : {post.ViewCount}");
//    WriteLine($"Date    : {post.PostedDate:dd/MM/yyyy}");
//    WriteLine($"Author  : {post.Author}");
//    WriteLine($"Category: {post.Category}");
//    WriteLine("".PadRight(80, '-'));
//}

#endregion

#region P22_Tutorial

//IBlogRepository blogRepo = new BlogRepository(context);

//var posts = await blogRepo.GetPopularArticlesAsync(3);

//foreach (var post in posts)
//{
//    WriteLine($"ID      : {post.Id}");
//    WriteLine($"Title   : {post.Title}");
//    WriteLine($"View    : {post.ViewCount}");
//    WriteLine($"Date    : {post.PostedDate:dd/MM/yyyy}");
//    WriteLine($"Author  : {post.Author.FullName}");
//    WriteLine($"Category: {post.Category.Name}");
//    WriteLine("".PadRight(80, '-'));
//}

#endregion

#region P25_Tutorial

//IBlogRepository blogRepo = new BlogRepository(context);

//var categories = await blogRepo.GetCategoriesAsync();

//WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");

//foreach (var category in categories)
//{
//    WriteLine("{0,-5}{1,-50}{2,10}", category.Id, category.Name, category.PostCount);
//}

#endregion

#region P31_Tutorial

IBlogRepository blogRepo = new BlogRepository(context);

var pagingParams = new PagingParams
{
    PageNumber = 1,
    PageSize = 5,
    SortColumn = "Name",
    SortOrder = "DESC"
};

var tagList = await blogRepo.GetPagedTagsAsync(pagingParams);

WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");

foreach (var item in tagList)
{
    WriteLine("{0,-5}{1,-50}{2,10}", item.Id, item.Name, item.PostCount);
}

#endregion