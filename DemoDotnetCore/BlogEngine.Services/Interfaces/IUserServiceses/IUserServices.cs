using BlogEngine.Services.Interfaces;
using BlogEngine.ViewModels.UserViewmodels;
using System.Threading.Tasks;

namespace BlogEngine.Services.IUserServiceses.Interfaces
{
    public interface IUserServices : IBaseService
    {
        /// <summary>
        /// The method use for login into system
        /// </summary>
        /// <param name="userLogin">UserLogin viewmodel</param>
        /// <returns>The token</returns>
        Task<UserLoginResultViewModel> Login(UserLoginViewModel userLogin);

        /// <summary>
        /// Using to regitr the user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        Task<UserViewModel> RegisterUser(RegisterUserViewModel userViewModel);

        /// <summary>
        /// User for update password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string userId, string password);
    }
}
