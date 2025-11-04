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
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserReolverRepository _userReolverRepository;

        public RoleServices(
            IRoleRepository roleRepository,
            IUserReolverRepository userReolverRepository)
        {
            _roleRepository = roleRepository;
            _userReolverRepository = userReolverRepository;
        }

        private async Task<Guid> GetCurrentUserIdAsync()
        {
            var currentUser = await _userReolverRepository.GetCurrentUser();
            return currentUser?.Id ?? Guid.Empty;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                CreatedDate = r.CreatedDate
            });
        }

        public async Task<RoleViewModel> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
                return null;

            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = role.CreatedDate
            };
        }

        public async Task<RoleViewModel> CreateRoleAsync(CreateRoleViewModel model)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            var role = await _roleRepository.CreateRoleAsync(model, currentUserId);

            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = role.CreatedDate
            };
        }

        public async Task<RoleViewModel> UpdateRoleAsync(UpdateRoleViewModel model)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            var role = await _roleRepository.UpdateRoleAsync(model, currentUserId);
            if (role == null)
                return null;

            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = role.CreatedDate
            };
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            return await _roleRepository.DeleteRoleAsync(id, currentUserId);
        }
    }
}
