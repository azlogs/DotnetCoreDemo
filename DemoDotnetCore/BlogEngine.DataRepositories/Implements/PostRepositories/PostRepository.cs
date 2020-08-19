using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.IPostRepositories;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.PostViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements.PostRepositories
{
    public class PostRepository : CommonRepository<Post>, IPostRepository
    {
        public PostRepository(BlogEngineDatabaseContext context, IUserReolverRepository currentUser)
          : base(context, context.Posts, currentUser)
        {
        }

        public async Task<PostViewModel> AddPost(CreatePostViewModel createPostViewModel)
        {
            var post = new Post
            {
                Author = createPostViewModel.Author,
                IsDelete = false,
                PostContent = createPostViewModel.PostContent,
                SlugTitle = createPostViewModel.SlugTitle,
                Title = createPostViewModel.Title,
                Tags = createPostViewModel.Tags
            };

            await InsertAsync(post);

            return ConvertToViewModel(post);
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            return await DeleteAsync(postId);
        }

        public async Task<PostViewModel> GetByPostId(Guid postId)
        {
            var post = await GetEntity().FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return null;
            }

            return ConvertToViewModel(post);
        }


        public async Task<PostViewModel> GetPostBySlug(string slug)
        {
            var post = await GetEntity().FirstOrDefaultAsync(p => p.SlugTitle == slug);
            if (post == null)
            {
                return null;
            }

            return ConvertToViewModel(post);
        }

        public IEnumerable<PostViewModel> GetPostByTag(string tag)
        {
            var query = GetEntity().AsEnumerable();

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(p => p.Tags.IndexOf(tag) >= 0);
            }

            return query.Select(p => ConvertToViewModel(p));
        }

        public async Task<PostViewModel> GetPostDetail(Guid postId)
        {
            var post = await GetById(postId);
            if (post == null)
            {
                return null;
            }

            return ConvertToViewModel(post);
        }

        public IEnumerable<PostViewModel> GetPosts(PostSearchViewModel postSearchViewModel)
        {
            var query = GetEntity().AsEnumerable();

            if (postSearchViewModel.Id != Guid.Empty)
            {
                query = query.Where(p => p.Id == postSearchViewModel.Id);
            }

            if (!string.IsNullOrEmpty( postSearchViewModel.Title))
            {
                query = query.Where(p => p.Title.IndexOf(postSearchViewModel.Title) >= 0 || postSearchViewModel.Title.IndexOf(p.Title) >= 0);
            }

            if (!string.IsNullOrEmpty(postSearchViewModel.PostContent))
            {
                query = query.Where(p => p.PostContent.IndexOf(postSearchViewModel.PostContent) >= 0);
            }

            if (!string.IsNullOrEmpty(postSearchViewModel.Author))
            {
                query = query.Where(p => p.Author.IndexOf(postSearchViewModel.Author) >= 0);
            }

            if (!string.IsNullOrEmpty(postSearchViewModel.Tags))
            {
                query = query.Where(p => p.Tags.IndexOf(postSearchViewModel.Tags) >= 0);
            }

            return query.Select(p => ConvertToViewModel(p));
        }

        public Task<PostViewModel> UpdatePost(PostViewModel postViewModel)
        {
            throw new NotImplementedException();
        }

        private PostViewModel ConvertToViewModel(Post p)
        {
            return new PostViewModel()
            {
                Id = p.Id,
                Author = p.Author,
                PostContent = p.PostContent,
                SlugTitle = p.SlugTitle,
                Tags = p.Tags,
                Title = p.Title
            };
        }
    }
}