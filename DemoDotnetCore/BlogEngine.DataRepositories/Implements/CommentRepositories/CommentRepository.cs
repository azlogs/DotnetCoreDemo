using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.ICommentRepositories;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.CommentViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements.CommentRepositories
{
    public class CommentRepository : CommonRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogEngineDatabaseContext context, IUserReolverRepository currentUser)
          : base(context, context.Comments, currentUser)
        {
        }

        public async Task<CommentViewModel> AddComment(CreateCommentViewmodel createViewModel)
        {
            var currentUser =  await _currentUser.GetCurrentUser();
            if (currentUser == null)
            {
                throw new Exception("Need to login");
            }
            var comment = ConvertToModel(createViewModel, currentUser.Id);

            if (await InsertAsync(comment))
            {
                return ConvertToViewModel(comment);
            }

            return null;
        }

        public async Task<bool> DeleteByPostId(Guid postId)
        {
            var commnets = await GetEntity().Where(c => c.PostId == postId && c.IsDelete == false).ToListAsync();
            if (commnets.Any())
            {
                foreach(var comment in commnets)
                {
                    await DeleteAsync(comment.Id);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            return await DeleteAsync(commentId);
        }

        public IEnumerable<CommentResultViewModel> GetCommentByPostId(Guid postId)
        {
            var commnets = GetEntity()
                    .Where(c => c.PostId == postId && c.IsDelete == false);

            return commnets.Select(c => ConvertToResultViewModel(c));
        }

        public async Task<CommentViewModel> UpdateComment(UpdateCommentViewModel updateViewModel)
        {
            var comment = await GetEntity()
                    .FirstOrDefaultAsync(c => c.PostId == updateViewModel.PostId && 
                        c.Id == updateViewModel.CommentId &&
                        c.IsDelete == false);

            if (comment == null)
            {
                throw new Exception("Not found");
            }

            if (await UpdateAsync(comment))
            {
                return ConvertToViewModel(comment);
            }

            throw new Exception("Can not update commnet");
        }

        private CommentViewModel ConvertToViewModel(Comment comment)
        {
            return new CommentViewModel
            {
                CommentContent = comment.CommentContent,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

        private CommentResultViewModel ConvertToResultViewModel(Comment comment)
        {
            return new CommentResultViewModel
            {
                CommentContent = comment.CommentContent,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

        private Comment ConvertToModel(CreateCommentViewmodel commentViewModel, Guid userId)
        { 
            return new Comment
            {
                CommentContent = commentViewModel.CommentContent,
                PostId = commentViewModel.PostId,
                UserId = userId
            };
        }
    }
}