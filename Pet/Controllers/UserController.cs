using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pet.Contracts;
using Pet.Options;
using Pet.Repositories;
using Pet.Services;

namespace Pet.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;
        private readonly UserService _userService;
        private readonly IOptions<AuthOptions> _authOptions;


        public UserController(UserService userService, UserRepository userRepository,
            IOptions<AuthOptions> authOptions)
        {
            _repository = userRepository;
            _userService = userService;
            _authOptions = authOptions;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _userService.Login(loginRequest.Email, loginRequest.Password);

            Response.Cookies.Append(_authOptions.Value.CookieName, token);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            await _userService.Register(registerRequest.UserName,
                registerRequest.Password,
                registerRequest.Email);

            return Ok("User has been successfully registered");
        }

    }
}


