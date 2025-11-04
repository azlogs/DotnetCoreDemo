using BlogEngine.DataRepositories.Interfaces.IRoleRepositories;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.Services.Interfaces.IRoleServiceses;
using BlogEngine.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Services.Implements.RoleServiceses
{
    public class UserRoleServices : IUserRoleServices
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserReolverRepository _userReolverRepository;

        public UserRoleServices(
            IUserRoleRepository userRoleRepository,
            IUserReolverRepository userReolverRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userReolverRepository = userReolverRepository;
        }

        private async Task<Guid> GetCurrentUserIdAsync()
        {
            var currentUser = await _userReolverRepository.GetCurrentUser();
            return currentUser?.Id ?? Guid.Empty;
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await _userRoleRepository.GetUserRolesAsync(userId);
            return userRoles.Select(ur => new UserRoleViewModel
            {
                Id = ur.Id,
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                UserName = ur.User?.Username,
                RoleName = ur.Role?.Name,
                CreatedDate = ur.CreatedDate
            });
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetRoleUsersAsync(Guid roleId)
        {
            var userRoles = await _userRoleRepository.GetRoleUsersAsync(roleId);
            return userRoles.Select(ur => new UserRoleViewModel
            {
                Id = ur.Id,
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                UserName = ur.User?.Username,
                RoleName = ur.Role?.Name,
                CreatedDate = ur.CreatedDate
            });
        }

        public async Task<UserRoleViewModel> AssignRoleToUserAsync(AssignRoleViewModel model)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            var userRole = await _userRoleRepository.AssignRoleToUserAsync(model, currentUserId);

            // Reload to get navigation properties
            var result = (await _userRoleRepository.GetUserRolesAsync(userRole.UserId))
                .FirstOrDefault(ur => ur.Id == userRole.Id);

            return new UserRoleViewModel
            {
                Id = result.Id,
                UserId = result.UserId,
                RoleId = result.RoleId,
                UserName = result.User?.Username,
                RoleName = result.Role?.Name,
                CreatedDate = result.CreatedDate
            };
        }

        public async Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            return await _userRoleRepository.RemoveRoleFromUserAsync(userId, roleId, currentUserId);
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId)
        {
            return await _userRoleRepository.UserHasRoleAsync(userId, roleId);
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, string roleName)
        {
            return await _userRoleRepository.UserHasRoleAsync(userId, roleName);
        }
    }
}
