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
    public class UserRoleRepository : CommonRepository<UserRole>, IUserRoleRepository
    {
        private readonly BlogEngineDatabaseContext _context;

        public UserRoleRepository(BlogEngineDatabaseContext context, IUserReolverRepository currentUser)
            : base(context, context.UserRoles, currentUser)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync(Guid userId)
        {
            return await _entity
                .Include(ur => ur.Role)
                .Include(ur => ur.User)
                .Where(ur => ur.UserId == userId && !ur.IsDelete)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetRoleUsersAsync(Guid roleId)
        {
            return await _entity
                .Include(ur => ur.Role)
                .Include(ur => ur.User)
                .Where(ur => ur.RoleId == roleId && !ur.IsDelete)
                .ToListAsync();
        }

        public async Task<UserRole> AssignRoleToUserAsync(AssignRoleViewModel model, Guid currentUserId)
        {
            // Check if assignment already exists
            var existing = await _entity
                .FirstOrDefaultAsync(ur => ur.UserId == model.UserId && ur.RoleId == model.RoleId && !ur.IsDelete);

            if (existing != null)
                return existing;

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = model.UserId,
                RoleId = model.RoleId,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = currentUserId,
                IsDelete = false
            };

            _entity.Add(userRole);
            await _context.SaveChangesAsync();

            return userRole;
        }

        public async Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId, Guid currentUserId)
        {
            var userRole = await _entity
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDelete);

            if (userRole == null)
                return false;

            userRole.IsDelete = true;
            userRole.UpdatedDate = DateTime.UtcNow;
            userRole.UpdatedUserId = currentUserId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId)
        {
            return await _entity
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDelete);
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, string roleName)
        {
            return await _entity
                .Include(ur => ur.Role)
                .AnyAsync(ur => ur.UserId == userId && ur.Role.Name == roleName && !ur.IsDelete);
        }
    }
}
