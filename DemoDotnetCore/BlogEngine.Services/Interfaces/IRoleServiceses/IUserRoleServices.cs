using BlogEngine.Services.Interfaces;
using BlogEngine.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Services.Interfaces.IRoleServiceses
{
    public interface IUserRoleServices : IBaseService
    {
        Task<IEnumerable<UserRoleViewModel>> GetUserRolesAsync(Guid userId);
        Task<IEnumerable<UserRoleViewModel>> GetRoleUsersAsync(Guid roleId);
        Task<UserRoleViewModel> AssignRoleToUserAsync(AssignRoleViewModel model);
        Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId);
        Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
        Task<bool> UserHasRoleAsync(Guid userId, string roleName);
    }
}
