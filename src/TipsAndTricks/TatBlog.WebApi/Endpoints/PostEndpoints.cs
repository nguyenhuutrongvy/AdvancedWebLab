using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public static class PostEndpoints
    {
        public static WebApplication MapPostEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/posts");

            routeGroupBuilder.MapGet("/", GetPosts)
                .WithName("GetPosts")
                .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/featured/{limit}", GetBestPosts)
                .WithName("GetBestPosts")
                .Produces<ApiResponse<IList<PostItem>>>();

            routeGroupBuilder.MapGet("/random/{limit}", GetRandomPosts)
                .WithName("GetRandomPosts")
                .Produces<ApiResponse<IList<PostItem>>>();

            routeGroupBuilder.MapGet("/archives/{months}", GetAchivePosts)
                .WithName("GetArchivesPosts")
                .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetPostDetails)
                .WithName("GetPostById")
                .Produces<ApiResponse<PostItem>>()
                .Produces(404);

            routeGroupBuilder.MapGet("/byslug/{slug:regex(^[a-z0-9_-]+$)}", GetPostByPostSlug)
                .WithName("GetPostsByPostSlug")
                .Produces<PaginationResult<PostDto>>();

            /*routeGroupBuilder.MapGet("/{id:int}/comments", GetPosts)
                .WithName("GetComments")
                .Produces<PaginationResult<PostItem>>();*/

            /*routeGroupBuilder.MapPost("/", AddPost)
                .WithName("AddNewPost")
                .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
                .Produces(201)
                .Produces(400)
                .Produces(409);*/

            routeGroupBuilder.MapPost("/{id:int}/picture", SetPostPicture)
                .WithName("SetPostPicture")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces<ApiResponse<string>>()
                .Produces(400);

            routeGroupBuilder.MapPut("/{id:int}", UpdatePost)
                .WithName("UpdateAnPost")
                .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
                .WithName("DeleteAnPost")
                .Produces(204)
                .Produces(404);

            routeGroupBuilder.MapGet("/get-posts-filter", GetFilteredPosts)
                .WithName("GetFilteredPost")
                .Produces<ApiResponse<PostDto>>();

            routeGroupBuilder.MapGet("/get-filter", GetFilter)
                .WithName("GetFilter")
                .Produces<ApiResponse<PostFilterModel>>();

            routeGroupBuilder.MapPost("/", AddPost)
                .WithName("AddNewPost")
                .Accepts<PostEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<PostItem>>();

            return app;
        }

        private static async Task<IResult> GetPosts([AsParameters] PostFilterModel model, IPostRepository postRepository)
        {
            PostQuery query = new PostQuery
            {
                Keyword = model.Keyword,
                /*TagName = model.Tag,*/
                CategoryId = model.CategoryId ?? 0,
                AuthorId = model.AuthorId ?? 0,
                Year = model.Year ?? 0,
                Month = model.Month ?? 0
            };

            var postsList = await postRepository.GetPagedPostsAsync(query, model);

            var paginationResult = new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostDetails(int id, IPostRepository postRepository, IMapper mapper)
        {
            var post = await postRepository.GetCachedPostByIdAsync(id);
            //return post == null ? Results.NotFound($"Không tìm thấy bài viết có mã số {id}") : Results.Ok(mapper.Map<PostItem>(post));
            return post == null ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có mã số {id}")) : Results.Ok(ApiResponse.Success(mapper.Map<PostItem>(post)));
        }

        private static async Task<IResult> GetBestPosts(int limit, IPostRepository postRepository)
        {
            var postsList = await postRepository.GetBestPostsAsync(limit);

            //var paginationResult = new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(postsList));
        }

        private static async Task<IResult> GetRandomPosts(int limit, IPostRepository postRepository)
        {
            var postsList = await postRepository.GetRandomPostsAsync(limit);

            //var paginationResult = new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(postsList));
        }

        private static async Task<IResult> GetAchivePosts(int months, IPostRepository postRepository)
        {
            var postsList = await postRepository.GetPagedAchivePostsAsync(months);

            var paginationResult = new PaginationResult<PostItem>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostsByPostId(int id, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                PostId = id,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostByPostSlug([FromRoute] string slug, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                TitleSlug = slug,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        /*private static async Task<IResult> AddPost(PostEditModel model, IPostRepository postRepository, IMapper mapper)
        {
            if (await postRepository.IsPostSlugExistedAsync(0, model.UrlSlug))
            {
                //return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var post = mapper.Map<Post>(model);
            await postRepository.AddOrUpdateAsync(post);

            //return Results.CreatedAtRoute("GetPostById", new { post.Id }, mapper.Map<PostItem>(post));
            return Results.Ok(ApiResponse.Success(mapper.Map<PostItem>(post), HttpStatusCode.Created));
        }*/

        private static async Task<IResult> SetPostPicture(int id, IFormFile imageFile, IPostRepository postRepository, IMediaManager mediaManager)
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

            await postRepository.SetImageUrlAsync(id, imageUrl);

            return Results.Ok(ApiResponse.Success(imageUrl));
        }

        private static async Task<IResult> UpdatePost(int id, PostEditModel model, IPostRepository postRepository, IMapper mapper)
        {
            if (await postRepository.IsPostSlugExistedAsync(id, model.UrlSlug))
            {
                //return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng");
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var post = mapper.Map<Post>(model);
            post.Id = id;

            //return await postRepository.AddOrUpdateAsync(post) ? Results.NoContent() : Results.NotFound();
            return await postRepository.AddOrUpdateAsync(post) ? Results.Ok(ApiResponse.Success("Post is updated", HttpStatusCode.NoContent)) : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Could not find post"));
        }

        private static async Task<IResult> DeletePost(int id, IPostRepository postRepository)
        {
            //return await postRepository.DeletePostAsync(id) ? Results.NoContent() : Results.NotFound($"Could not find post with id = {id}");
            return await postRepository.DeletePostAsync(id) ? Results.Ok(ApiResponse.Success("Post is deleted", HttpStatusCode.NoContent)) : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Could not find post"));
        }

        private static async Task<IResult> GetFilter(IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            var model = new PostFilterModel()
            {
                AuthorList = (await authorRepository.GetAuthorsAsync())
                .Select(a => new SelectListItem()
                {
                    Text = a.FullName,
                    Value = a.Id.ToString()
                }),
                CategoryList = (await categoryRepository.GetCategoriesAsync(default))
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return Results.Ok(ApiResponse.Success(model));
        }

        private static async Task<IResult> GetFilteredPosts([AsParameters] PostFilterModel model, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                Keyword = model.Keyword,
                CategoryId = model.CategoryId ?? 0,
                AuthorId = model.AuthorId ?? 0,
                Year = model.Year ?? 0,
                Month = model.Month ?? 0
            };
            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, model, posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddPost(HttpContext context, IBlogRepository blogRepository, IMapper mapper, IMediaManager mediaManager)
        {
            var model = await PostEditModel.BindAsync(context);
            var slug = model.Title.GenerateSlug();
            if (await blogRepository.IsPostSlugExistedAsync(model.Id, slug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho bài viết khác"));
            }
            var post = model.Id > 0 ? await blogRepository.GetPostByIdAsync(model.Id) : null;

            if (post == null)
            {
                post = new Post()
                {
                    PostedDate = DateTime.Now
                };
            }

            post.Title = model.Title;
            post.AuthorId = model.AuthorId;
            post.CategoryId = model.CategoryId;
            post.ShortDescription = model.ShortDescription;
            post.Description = model.Description;
            post.Meta = model.Meta;
            post.Published = model.Published;
            post.ModifiedDate = DateTime.Now;
            post.UrlSlug = model.Title.GenerateSlug();

            if (model.ImageFile?.Length > 0)
            {
                string hostname = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
                uploadedPath = await mediaManager.SaveFileAsync(
                    model.ImageFile.OpenReadStream(),
                    model.ImageFile.FileName,
                    model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    post.ImageUrl = uploadedPath;
                }
            }
            await blogRepository.CreateOrUpdatePostAsync(post,
            model.GetSelectedTags());
            return Results.Ok(ApiResponse.Success(
            mapper.Map<PostItem>(post), HttpStatusCode.Created));
        }
    }
}
