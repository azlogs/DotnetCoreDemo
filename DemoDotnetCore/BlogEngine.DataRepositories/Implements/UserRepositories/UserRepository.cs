using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.UserViewmodels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements.UserRepositories
{
    public class UserRepository : CommonRepository<User>, IUserRepository
    {
        public UserRepository(BlogEngineDatabaseContext context, IUserReolverRepository currentUser)
            : base(context, context.Users, currentUser)
        {

        }

        #region Check username and password
        public async Task<bool> CheckUsernamePassword(string username, string password)
        {
            var user = await GetEntity().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null) return false;

            return true;
        }
        #endregion

        #region get user by username
        public async Task<UserViewModel> GetUserByUsername(string username)
        {
            var user = await GetEntity().FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.Fullname,
                Username = user.Username
            };
        }
        #endregion

        #region Register user
        public async Task<UserViewModel> RegisterUser(RegisterUserViewModel userViewModel)
        {
            // check exist
            var currentEntity = GetEntity();
            if (currentEntity.Where(u => u.Username.ToLower() == userViewModel.Username.ToLower()).Any())
            {
                throw new Exception("Username already existed");
            }

            var user = new User
            {
               Email = userViewModel.Email,
               Password = userViewModel.Password,
               Fullname = userViewModel.Fullname,
               Username = userViewModel.Username
            };

            if (await InsertAsync(user))
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Username = user.Username
                };
            }

            throw new Exception("Can not create user");
        }
        #endregion

        #region Private functions

        #endregion
    }
}