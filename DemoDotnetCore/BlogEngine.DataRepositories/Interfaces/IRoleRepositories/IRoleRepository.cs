using BlogEngine.DataModels.Models;
using BlogEngine.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.IRoleRepositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(Guid id);
        Task<Role> GetRoleByNameAsync(string name);
        Task<Role> CreateRoleAsync(CreateRoleViewModel model, Guid currentUserId);
        Task<Role> UpdateRoleAsync(UpdateRoleViewModel model, Guid currentUserId);
        Task<bool> DeleteRoleAsync(Guid id, Guid currentUserId);
    }
}
