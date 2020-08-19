using BlogEngine.DataModels.Models;
using BlogEngine.ViewModels.CommentViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.ICommentRepositories
{
    public interface ICommentRepository: ICommonRepository<Comment>
    {
        IEnumerable<CommentResultViewModel> GetCommentByPostId(Guid postId);

        Task<CommentViewModel> AddComment(CreateCommentViewmodel createViewModel);
         
        Task<CommentViewModel> UpdateComment(UpdateCommentViewModel updateViewModel);

        Task<bool> DeleteByPostId(Guid postId);

        Task<bool> DeleteComment(Guid commentId);
    }
}