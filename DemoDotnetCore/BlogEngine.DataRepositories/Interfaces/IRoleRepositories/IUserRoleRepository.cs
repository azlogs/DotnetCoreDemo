using BlogEngine.DataModels.Models;
using BlogEngine.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.IRoleRepositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetUserRolesAsync(Guid userId);
        Task<IEnumerable<UserRole>> GetRoleUsersAsync(Guid roleId);
        Task<UserRole> AssignRoleToUserAsync(AssignRoleViewModel model, Guid currentUserId);
        Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId, Guid currentUserId);
        Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
        Task<bool> UserHasRoleAsync(Guid userId, string roleName);
    }
}
