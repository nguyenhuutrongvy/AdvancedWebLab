using FluentValidation;
using TatBlog.Core.Collections;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApi.Endpoints
{
    public static class CommonEndpoints
    {
        public static WebApplication MapCommonEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/common");

            routeGroupBuilder.MapGet("/posts", GetPostsCommons)
                .WithName("GetPostsCommons")
                .Produces<int>();
            
            routeGroupBuilder.MapGet("/unpublished-posts", GetUnpublishedPostsCommons)
                .WithName("GetUnpublishedPostsCommons")
                .Produces<int>();
            
            routeGroupBuilder.MapGet("/categories", GetCategoriesCommons)
                .WithName("GetCategoriesCommons")
                .Produces<int>();
            
            routeGroupBuilder.MapGet("/authors", GetAuthorsCommons)
                .WithName("GetAuthorsCommons")
                .Produces<int>();

            return app;
        }

        private static async Task<IResult> GetPostsCommons(ICommonRepository commonRepository)
        {
            var commons = await commonRepository.GetPostsCommonsAsync();

            return Results.Ok(commons);
        }
        
        private static async Task<IResult> GetUnpublishedPostsCommons(ICommonRepository commonRepository)
        {
            var commons = await commonRepository.GetUnpublishedPostsCommonsAsync();

            return Results.Ok(commons);
        }
        
        private static async Task<IResult> GetCategoriesCommons(ICommonRepository commonRepository)
        {
            var commons = await commonRepository.GetCategoriesCommonsAsync();

            return Results.Ok(commons);
        }
        
        private static async Task<IResult> GetAuthorsCommons(ICommonRepository commonRepository)
        {
            var commons = await commonRepository.GetAuthorsCommonsAsync();

            return Results.Ok(commons);
        }
    }
}
