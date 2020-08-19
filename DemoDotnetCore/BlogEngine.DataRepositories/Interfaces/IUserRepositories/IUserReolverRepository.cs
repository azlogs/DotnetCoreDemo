using BlogEngine.ViewModels.UserViewmodels;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces.IUserRepositories
{
    public interface IUserReolverRepository
    {
        Task<UserViewModel> GetCurrentUser();
    }
}