#region Old

//using Microsoft.EntityFrameworkCore;
//using TatBlog.Data.Contexts;
//using TatBlog.Data.Seeders;
//using TatBlog.Services.Blogs;

//var builder = WebApplication.CreateBuilder(args);
//{
//    // Thêm các dịch vụ được yêu cầu bởi MVC Framework
//    builder.Services.AddControllersWithViews();

//    // Đăng ký các dịch vụ với DI Container
//    builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//    builder.Services.AddScoped<IBlogRepository, BlogRepository>();
//    builder.Services.AddScoped<IDataSeeder, DataSeeder>();
//}

//var app = builder.Build();
//{
//    // Cấu hình HTTP Request pipeline

//    // Thêm middleware để hiển thị thông báo lỗi
//    if (app.Environment.IsDevelopment())
//    {
//        app.UseDeveloperExceptionPage();
//    }
//    else
//    {
//        app.UseExceptionHandler("/Blog/Error");

//        // Thêm middleware cho việc áp dụng HSTS
//        // (Thêm header Strict-Transport-Security vào HTTP Response)
//        app.UseHsts();
//    }

//    // Thêm middleware để chuyển hướng HTTP sang HTTPS
//    app.UseHttpsRedirection();

//    // Thêm middleware phục vụ các yêu cầu liên quan tới các tập tin nội dung tĩnh như hình ảnh, css, ...
//    app.UseStaticFiles();

//    // Thêm middleware lựa chọn endpoint phù hợp nhất để xử lý một HTTP request
//    app.UseRouting();

//    // Định nghĩa route template, route constraint cho các endpoints kết hợp với các action trong các controller
//    app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Blog}/{action=Index}/{id?}");

//    // Định nghĩa route template, route constraint cho các endpoints kết hợp với các action trong các controller
//    app.MapControllerRoute(
//        name: "posts-by-category",
//        pattern: "blog/category/{slug}",
//        defaults: new { controller = "Blog", action = "Category" });

//    app.MapControllerRoute(
//        name: "posts-by-tag",
//        pattern: "blog/tag/{slug}",
//        defaults: new { controller = "Blog", action = "Tag" });

//    app.MapControllerRoute(
//        name: "single-posts",
//        pattern: "blog/post/{year:int}/{month:int}/{day:int}/{slug}",
//        defaults: new { controller = "Blog", action = "Post" });

//    app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Blog}/{action=Index}/{id?}");
//}

////app.MapGet("/", () => "Hello World!");

//// Thêm dữ liệu mẫu vào CSDL trước khi ứng dụng Web khởi chạy
//using (var scope = app.Services.CreateScope())
//{
//    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
//    seeder.Initialize();
//}

//app.Run();

#endregion

using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Mapster;
using TatBlog.WebApp.Validations;
using NLog;
using NLog.Web;
using System;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    {
        builder
            .ConfigureMvc()
            .ConfigureNLog()
            .ConfigureServices()
            .ConfigureMapster()
            .ConfigureFluentValidation();
    }

    var app = builder.Build();
    {
        app.UseRequestPipeline();
        app.UseBlogRoutes();
        app.UseDataSeeder();
    }

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
