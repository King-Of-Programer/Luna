using Luna_Edge.Model;

namespace Luna_Edge.Services.UserService.UserService
{
    public interface IUserService
    {
        System.Threading.Tasks.Task RegisterUser(Model.UserModel user, string password);
        Task<string> Authenticate(string usernameOrEmail, string password);
    }
}
