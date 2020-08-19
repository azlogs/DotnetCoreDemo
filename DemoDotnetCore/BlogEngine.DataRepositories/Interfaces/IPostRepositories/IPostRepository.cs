using BlogEngine.DataModels.Models;
using BlogEngine.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.IPostRepositories
{
    public interface IPostRepository : ICommonRepository<Post>
    {
        IEnumerable<PostViewModel> GetPosts(PostSearchViewModel postSearchViewModel);

        Task<PostViewModel> GetPostBySlug(string slug);

        Task<PostViewModel> GetByPostId(Guid postId);

        IEnumerable<PostViewModel> GetPostByTag(string tag);

        Task<PostViewModel> GetPostDetail(Guid postId);

        Task<PostViewModel> AddPost(CreatePostViewModel createPostViewModel);

        Task<PostViewModel> UpdatePost(PostViewModel postViewModel);

        Task<bool> DeletePost(Guid postId); 
    }
}