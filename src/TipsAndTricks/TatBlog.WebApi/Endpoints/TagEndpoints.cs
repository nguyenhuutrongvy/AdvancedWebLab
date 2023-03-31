using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Collections;
using TatBlog.Core.Constants;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class TagEndpoints
    {
        public static WebApplication MapTagEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/tags");

            routeGroupBuilder.MapGet("/", GetTags)
                .WithName("GetTags")
                .Produces<PaginationResult<TagItem>>();

            routeGroupBuilder.MapGet("/{id:int}", GetTagDetails)
                .WithName("GetTagById")
                .Produces<TagItem>()
                .Produces(404);

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/posts", GetPostByTagSlug)
                .WithName("GetPostsByTagSlug")
                .Produces<PaginationResult<PostDto>>();

            routeGroupBuilder.MapPost("/", AddTag)
                .WithName("AddNewTag")
                .AddEndpointFilter<ValidatorFilter<TagEditModel>>()
                .Produces(201)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapPut("/{id:int}", UpdateTag)
                .WithName("UpdateAnTag")
                .AddEndpointFilter<ValidatorFilter<TagEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapDelete("/{id:int}", DeleteTag)
                .WithName("DeleteAnTag")
                .Produces(204)
                .Produces(404);

            return app;
        }

        private static async Task<IResult> GetTags([AsParameters] TagFilterModel model, ITagRepository categoryRepository)
        {
            var tagsList = await categoryRepository.GetPagedTagsAsync(model, model.Name);

            var paginationResult = new PaginationResult<TagItem>(tagsList);

            return Results.Ok(paginationResult);
        }

        private static async Task<IResult> GetTagDetails(int id, ITagRepository categoryRepository, IMapper mapper)
        {
            var category = await categoryRepository.GetCachedTagByIdAsync(id);
            return category == null ? Results.NotFound($"Không tìm thấy chuyên mục có mã số {id}") : Results.Ok(mapper.Map<TagItem>(category));
        }

        private static async Task<IResult> GetPostsByTagId(int id, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                TagId = id,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(paginationResult);
        }

        private static async Task<IResult> GetPostByTagSlug([FromRoute] string slug, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                TagSlug = slug,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(paginationResult);
        }

        private static async Task<IResult> AddTag(TagEditModel model, ITagRepository categoryRepository, IMapper mapper)
        {
            if (await categoryRepository.IsTagSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
            }

            var category = mapper.Map<Tag>(model);
            await categoryRepository.AddOrUpdateAsync(category);

            return Results.CreatedAtRoute("GetTagById", new { category.Id }, mapper.Map<TagItem>(category));
        }

        private static async Task<IResult> UpdateTag(int id, TagEditModel model, ITagRepository categoryRepository, IMapper mapper)
        {
            if (await categoryRepository.IsTagSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
            }

            var category = mapper.Map<Tag>(model);
            category.Id = id;

            return await categoryRepository.AddOrUpdateAsync(category) ? Results.NoContent() : Results.NotFound();
        }

        private static async Task<IResult> DeleteTag(int id, ITagRepository categoryRepository)
        {
            return await categoryRepository.DeleteTagAsync(id) ? Results.NoContent() : Results.NotFound($"Could not find category with id = {id}");
        }
    }
}
