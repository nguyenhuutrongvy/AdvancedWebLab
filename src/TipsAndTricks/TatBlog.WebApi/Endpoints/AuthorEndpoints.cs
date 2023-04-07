using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TatBlog.Core.Collections;
using TatBlog.Core.Constants;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class AuthorEndpoints
    {
        public static WebApplication MapAuthorEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/authors");

            /*routeGroupBuilder.MapGet("/", GetAuthors)
                .WithName("GetAuthors")
                .Produces<PaginationResult<AuthorItem>>();

            routeGroupBuilder.MapGet("/{id:int}", GetAuthorDetails)
                .WithName("GetAuthorById")
                .Produces<AuthorItem>()
                .Produces(404);

            routeGroupBuilder.MapGet("/best/{amount:int}", GetBestAuthors)
                .WithName("GetBestAuthors")
                .Produces<PaginationResult<AuthorItem>>();

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/authors", GetPostByAuthorSlug)
                .WithName("GetPostsByAuthorSlug")
                .Produces<PaginationResult<PostDto>>();

            routeGroupBuilder.MapPost("/", AddAuthor)
                .WithName("AddNewAuthor")
                .AddEndpointFilter<ValidatorFilter<AuthorEditModel>>()
                .Produces(201)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapPost("/{id:int}/avatar", SetAuthorPicture)
                .WithName("SetAuthorPicture")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces<string>()
                .Produces(400);

            routeGroupBuilder.MapPut("/{id:int}", UpdateAuthor)
                .WithName("UpdateAnAuthor")
                .AddEndpointFilter<ValidatorFilter<AuthorEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapDelete("/{id:int}", DeleteAuthor)
                .WithName("DeleteAnAuthor")
                .Produces(204)
                .Produces(404);*/

            routeGroupBuilder.MapGet("/", GetAuthors)
                .WithName("GetAuthors")
                .Produces<ApiResponse<PaginationResult<AuthorItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetAuthorDetails)
                .WithName("GetAuthorById")
                .Produces<ApiResponse<AuthorItem>>();

            routeGroupBuilder.MapGet("/best/{limit:int}", GetBestAuthors)
                .WithName("GetBestAuthors")
                .Produces<ApiResponse<IList<AuthorItem>>>();

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/authors", GetPostByAuthorSlug)
                .WithName("GetPostsByAuthorSlug")
                .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapPost("/", AddAuthor)
                .AddEndpointFilter<ValidatorFilter<AuthorEditModel>>()
                .WithName("AddNewAuthor")
                .Produces(401)
                .Produces<ApiResponse<AuthorItem>>();

            routeGroupBuilder.MapPost("/{id:int}/avatar", SetAuthorPicture)
                .WithName("SetAuthorPicture")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapPut("/{id:int}", UpdateAuthor)
                .WithName("UpdateAnAuthor")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteAuthor)
                .WithName("DeleteAnAuthor")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetAuthors([AsParameters] AuthorFilterModel model, IAuthorRepository authorRepository)
        {
            var authorsList = await authorRepository.GetPagedAuthorsAsync(model, model.Name);

            var paginationResult = new PaginationResult<AuthorItem>(authorsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetAuthorDetails(int id, IAuthorRepository authorRepository, IMapper mapper)
        {
            var author = await authorRepository.GetCachedAuthorByIdAsync(id);
            //return author == null ? Results.NotFound($"Không tìm thấy tác giả có mã số {id}") : Results.Ok(mapper.Map<AuthorItem>(author));
            return author == null ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy tác giả có mã số {id}")) : Results.Ok(ApiResponse.Success(mapper.Map<AuthorItem>(author)));
        }

        private static async Task<IResult> GetBestAuthors(int limit, IAuthorRepository authorRepository)
        {
            var authorsList = await authorRepository.GetBestAuthorsAsync(limit);

            //var paginationResult = new PaginationResult<AuthorItem>(authorsList);

            return Results.Ok(ApiResponse.Success(authorsList));
        }

        private static async Task<IResult> GetPostsByAuthorId(int id, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorId = id,
                PublishedOnly = true
            };

            var authorsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, authors => authors.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(authorsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostByAuthorSlug([FromRoute] string slug, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorSlug = slug,
                PublishedOnly = true
            };

            var authorsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, authors => authors.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(authorsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddAuthor(AuthorEditModel model, IAuthorRepository authorRepository, IMapper mapper)
        {
            if (await authorRepository.IsAuthorSlugExistedAsync(0, model.UrlSlug))
            {
                //return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var author = mapper.Map<Author>(model);
            await authorRepository.AddOrUpdateAsync(author);

            //return Results.CreatedAtRoute("GetAuthorById", new { author.Id }, mapper.Map<AuthorItem>(author));
            return Results.Ok(ApiResponse.Success(mapper.Map<AuthorItem>(author), HttpStatusCode.Created));
        }

        private static async Task<IResult> SetAuthorPicture(int id, IFormFile imageFile, IAuthorRepository authorRepository, IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName,
                imageFile.ContentType);

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                //return Results.BadRequest("Không lưu được tập tin");
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }

            await authorRepository.SetImageUrlAsync(id, imageUrl);

            return Results.Ok(ApiResponse.Success(imageUrl));
        }

        private static async Task<IResult> UpdateAuthor(int id, AuthorEditModel model, IAuthorRepository authorRepository, IMapper mapper)
        {
            if (await authorRepository.IsAuthorSlugExistedAsync(id, model.UrlSlug))
            {
                //return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var author = mapper.Map<Author>(model);
            author.Id = id;

            //return await authorRepository.AddOrUpdateAsync(author) ? Results.NoContent() : Results.NotFound();
            return await authorRepository.AddOrUpdateAsync(author) ? Results.Ok(ApiResponse.Success("Author is updated", HttpStatusCode.NoContent)) : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Could not find author"));
        }

        private static async Task<IResult> DeleteAuthor(int id, IAuthorRepository authorRepository)
        {
            //return await authorRepository.DeleteAuthorAsync(id) ? Results.NoContent() : Results.NotFound($"Could not find author with id = {id}");
            return await authorRepository.DeleteAuthorAsync(id) ? Results.Ok(ApiResponse.Success("Author is deleted", HttpStatusCode.NoContent)) : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Could not find author"));
        }
    }
}
