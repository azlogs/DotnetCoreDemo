using BlogEngine.DataRepositories.Interfaces.IPostRepositories;
using BlogEngine.Services.Interfaces.IPostServiceses;
using BlogEngine.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implements.PostServiceses
{
    public class PostServices : IPostServices
    {
        private readonly IPostRepository _postRepository;
        public PostServices(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostViewModel> AddPost(CreatePostViewModel createPostViewModel)
        {
            return await _postRepository.AddPost(createPostViewModel);
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            return await _postRepository.GetByPostId(postId);
        }

        public async Task<IEnumerable<PostViewModel>> SearchPost(PostSearchViewModel createPostViewModel)
        {
            var posts = _postRepository.GetPosts(createPostViewModel);

            // do the paging here
            return posts.ToList();
        }
    }
}