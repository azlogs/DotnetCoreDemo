using BlogEngine.DataRepositories.Interfaces.ICommentRepositories;
using BlogEngine.Services.Interfaces.ICommentServiceses;
using BlogEngine.ViewModels.CommentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implements.CommentServiceses
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentViewModel> AddComment(CreateCommentViewmodel createViewModel)
        {
            return await _commentRepository.AddComment(createViewModel);
        }

        public async Task<bool> DeleteByPostId(Guid postId)
        {
            return await _commentRepository.DeleteByPostId(postId);
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            return await _commentRepository.DeleteComment(commentId);
        }

        public IEnumerable<CommentResultViewModel> GetCommentByPostId(Guid postId)
        {
            return _commentRepository.GetCommentByPostId(postId).ToList();
        }

        public async Task<CommentViewModel> UpdateComment(UpdateCommentViewModel updateViewModel)
        {
            return await _commentRepository.UpdateComment(updateViewModel);
        }
    }
}