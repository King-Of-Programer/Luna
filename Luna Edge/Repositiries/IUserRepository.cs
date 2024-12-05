using Luna_Edge.Model;
using System.Threading.Tasks;
namespace Luna_Edge.Repositiries
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        System.Threading.Tasks.Task AddAsync(User user);
    }
}
