using System.Collections.Generic;
using System.Threading.Tasks;
using AppTiengAnhBE.Models;

namespace AppTiengAnhBE.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(int id);
    }
}
