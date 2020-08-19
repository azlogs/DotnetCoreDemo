using BlogEngine.DataModels.Models;
using BlogEngine.ViewModels.UserViewmodels;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.IUserRepositories
{
    public interface IUserRepository : ICommonRepository<User>
    {
        Task<UserViewModel> GetUserByUsername(string usrename);

        Task<UserViewModel> RegisterUser(RegisterUserViewModel userViewModel);

        Task<bool> CheckUsernamePassword(string username, string password);
    }
}