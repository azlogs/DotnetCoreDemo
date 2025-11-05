using BlogEngine.Services.Interfaces;
using BlogEngine.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Services.Interfaces.IRoleServiceses
{
    public interface IRoleServices : IBaseService
    {
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task<RoleViewModel> GetRoleByIdAsync(Guid id);
        Task<RoleViewModel> CreateRoleAsync(CreateRoleViewModel model);
        Task<RoleViewModel> UpdateRoleAsync(UpdateRoleViewModel model);
        Task<bool> DeleteRoleAsync(Guid id);
    }
}
