using BlogEngine.Services.Interfaces.ICommentServiceses;
using BlogEngine.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly ICommentServices _commentServices;
        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpGet]
        [Route("post/{postId}")]
        public ActionResult<List<CommentResultViewModel>> GetComment(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                return BadRequest("Not found");
            }

            var comments = _commentServices.GetCommentByPostId(postId);

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<CommentViewModel>> CreateComment(CreateCommentViewmodel createViewModel)
        {
            var comment = await _commentServices.AddComment(createViewModel);
            if (comment == null || comment.CommentId == Guid.Empty)
            {
                return BadRequest("Not found");
            }

            return Ok(comment);
        }

        [HttpPut]
        public async Task<ActionResult<CommentViewModel>> UpdateComment(UpdateCommentViewModel updateViewModel)
        {
            var comment = await _commentServices.UpdateComment(updateViewModel);

            return Ok(comment);
        }

        [HttpDelete]
        [Route("Delete/{commentId}")]
        public async Task<ActionResult<CommentViewModel>> DeleteComment(Guid commentId)
        {
            var comment = await _commentServices.DeleteComment(commentId);

            return Ok(comment);
        }


        [HttpPost]
        [Route("DeleteByPostId/{postId}")]
        public async Task<ActionResult<CommentViewModel>> DeleteCommentByPostId(Guid postId)
        {
            var comment = await _commentServices.DeleteByPostId(postId);

            return Ok(comment);
        }
    }
} 