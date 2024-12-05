using Luna_Edge.Model;
using Luna_Edge.Services.UserService.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Luna_Edge.Controllers
{
    [ApiController]
    [Route("/users/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            try
            {
                await _userService.RegisterUser(userModel, userModel.Password);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await _userService.Authenticate(loginModel.UsernameOrEmail, loginModel.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
    
}
