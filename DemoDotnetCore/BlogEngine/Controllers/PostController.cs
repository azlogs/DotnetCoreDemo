using BlogEngine.Services.Interfaces.IPostServiceses;
using BlogEngine.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IPostServices _postServices;
        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult<PostViewModel>> GetPostDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Can not found the post");
            }

            var post = await _postServices.GetPost(id);
            if (post == null)
            {
                return NotFound("Can not found the post");
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostViewModel>> CreatePost(CreatePostViewModel postViewModel)
        {
            var post = await _postServices.AddPost(postViewModel);

            if (post == null || post.Id == Guid.Empty)
            {
               return BadRequest("Can not create Post");
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<PostViewModel>> SearchPost([FromQuery] PostSearchViewModel postViewModel)
        {
            var posts = await _postServices.SearchPost(postViewModel);
 
            return Ok(posts);
        } 
    }
}