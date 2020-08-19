using BlogEngine.Services.IUserServiceses.Interfaces;
using BlogEngine.ViewModels.UserViewmodels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResultViewModel>> Login([FromBody] UserLoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userServices.Login(userLogin);

                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });// return NotFound();
                }

                return user;
            }

            return BadRequest();
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserViewModel>> Register([FromBody] RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.RegisterUser(userViewModel);

                return Ok(result);
            }

            return BadRequest();
        } 
        #endregion
    }
}