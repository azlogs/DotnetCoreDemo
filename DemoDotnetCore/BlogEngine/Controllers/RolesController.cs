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
    public class RolesController : BaseController
    {
        private readonly IRoleServices _roleServices;

        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleServices.GetAllRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Get role by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _roleServices.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            return Ok(role);
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var role = await _roleServices.CreateRoleAsync(model);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }

        /// <summary>
        /// Update an existing role
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.Id)
                return BadRequest(new { message = "ID mismatch" });

            var role = await _roleServices.UpdateRoleAsync(model);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            return Ok(role);
        }

        /// <summary>
        /// Delete a role
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _roleServices.DeleteRoleAsync(id);
            if (!result)
                return NotFound(new { message = "Role not found" });

            return NoContent();
        }
    }
}
