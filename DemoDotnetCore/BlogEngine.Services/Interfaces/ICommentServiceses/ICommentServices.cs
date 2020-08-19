using BlogEngine.ViewModels.CommentViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Services.Interfaces.ICommentServiceses
{
    public interface ICommentServices : IBaseService
    {
        IEnumerable<CommentResultViewModel> GetCommentByPostId(Guid postId);

        Task<CommentViewModel> AddComment(CreateCommentViewmodel createViewModel);

        Task<CommentViewModel> UpdateComment(UpdateCommentViewModel updateViewModel);

        Task<bool> DeleteByPostId(Guid postId);

        Task<bool> DeleteComment(Guid commentId);
    }
}