using BlogEngine.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Services.Interfaces.IPostServiceses
{
    public interface IPostServices : IBaseService
    {
        Task<PostViewModel> AddPost(CreatePostViewModel createPostViewModel);

        Task<IEnumerable<PostViewModel>> SearchPost(PostSearchViewModel createPostViewModel);

        Task<PostViewModel> GetPost(Guid postId);
    }
}