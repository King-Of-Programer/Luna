using Luna_Edge.Model;
using Luna_Edge.Repositiries;
using BCrypt.Net;
using Luna_Edge.Services.Token;
using Luna_Edge.Services.UserService.UserService;
using Microsoft.AspNetCore.Mvc;
namespace Luna_Edge.Services.User
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async System.Threading.Tasks.Task RegisterUser(Model.UserModel userModel, string password)
        {
            if (await _userRepository.GetByUsernameAsync(userModel.Username) != null ||
                await _userRepository.GetByEmailAsync(userModel.Email) != null)
            {
                throw new Exception("Username or email already exists.");
            }

            
            var user = new Model.User
            {
                Username = userModel.Username,
                Email = userModel.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) 
            };

            await _userRepository.AddAsync(user); 
        }

        public async Task<string> Authenticate(string usernameOrEmail, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(usernameOrEmail) ??
                       await _userRepository.GetByEmailAsync(usernameOrEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            
            return _tokenService.GenerateJwtToken(user);
        }
    }
}
