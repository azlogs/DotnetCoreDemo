using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.IRoleRepositories;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.RoleViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements.RoleRepositories
{
    public class RoleRepository : CommonRepository<Role>, IRoleRepository
    {
        private readonly BlogEngineDatabaseContext _context;

        public RoleRepository(BlogEngineDatabaseContext context, IUserReolverRepository currentUser)
            : base(context, context.Roles, currentUser)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _entity
                .Where(r => !r.IsDelete)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            return await _entity
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDelete);
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _entity
                .FirstOrDefaultAsync(r => r.Name == name && !r.IsDelete);
        }

        public async Task<Role> CreateRoleAsync(CreateRoleViewModel model, Guid currentUserId)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = currentUserId,
                IsDelete = false
            };

            _entity.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role> UpdateRoleAsync(UpdateRoleViewModel model, Guid currentUserId)
        {
            var role = await GetRoleByIdAsync(model.Id);
            if (role == null)
                return null;

            role.Name = model.Name;
            role.Description = model.Description;
            role.UpdatedDate = DateTime.UtcNow;
            role.UpdatedUserId = currentUserId;

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<bool> DeleteRoleAsync(Guid id, Guid currentUserId)
        {
            var role = await GetRoleByIdAsync(id);
            if (role == null)
                return false;

            role.IsDelete = true;
            role.UpdatedDate = DateTime.UtcNow;
            role.UpdatedUserId = currentUserId;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
