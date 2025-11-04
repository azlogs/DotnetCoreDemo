using BlogEngine.Services.Interfaces.IRoleServiceses;
using BlogEngine.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : BaseController
    {
        private readonly IUserRoleServices _userRoleServices;

        public UserRolesController(IUserRoleServices userRoleServices)
        {
            _userRoleServices = userRoleServices;
        }

        /// <summary>
        /// Get all roles for a specific user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            var userRoles = await _userRoleServices.GetUserRolesAsync(userId);
            return Ok(userRoles);
        }

        /// <summary>
        /// Get all users for a specific role
        /// </summary>
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetRoleUsers(Guid roleId)
        {
            var userRoles = await _userRoleServices.GetRoleUsersAsync(roleId);
            return Ok(userRoles);
        }

        /// <summary>
        /// Assign a role to a user
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userRole = await _userRoleServices.AssignRoleToUserAsync(model);
            return Ok(userRole);
        }

        /// <summary>
        /// Remove a role from a user
        /// </summary>
        [HttpDelete("user/{userId}/role/{roleId}")]
        [Authorize]
        public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
        {
            var result = await _userRoleServices.RemoveRoleFromUserAsync(userId, roleId);
            if (!result)
                return NotFound(new { message = "User role assignment not found" });

            return NoContent();
        }

        /// <summary>
        /// Check if a user has a specific role
        /// </summary>
        [HttpGet("user/{userId}/role/{roleId}/check")]
        public async Task<IActionResult> CheckUserHasRole(Guid userId, Guid roleId)
        {
            var hasRole = await _userRoleServices.UserHasRoleAsync(userId, roleId);
            return Ok(new { hasRole });
        }

        /// <summary>
        /// Check if a user has a specific role by role name
        /// </summary>
        [HttpGet("user/{userId}/role/{roleName}/check-by-name")]
        public async Task<IActionResult> CheckUserHasRoleByName(Guid userId, string roleName)
        {
            var hasRole = await _userRoleServices.UserHasRoleAsync(userId, roleName);
            return Ok(new { hasRole });
        }
    }
}
