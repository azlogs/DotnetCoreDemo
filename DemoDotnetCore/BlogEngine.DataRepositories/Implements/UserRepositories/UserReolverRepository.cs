using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.UserViewmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements.UserRepositories
{
    public class UserReolverRepository : IUserReolverRepository
    {
        private readonly BlogEngineDatabaseContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public UserReolverRepository(BlogEngineDatabaseContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<UserViewModel> GetCurrentUser()
        {
            var username = _httpContext.HttpContext.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {
                var user = await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Username == username);

                if (user == null) return null;

                return new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Username = user.Username
                };
            }
            return null;
        }
    }
}