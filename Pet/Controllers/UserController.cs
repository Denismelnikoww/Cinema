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
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest,
                                               CancellationToken cancellationToken)
        {
            var token = await _userService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);

            Response.Cookies.Append(_authOptions.Value.CookieName, token);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest,
            CancellationToken cancellationToken)
        {
            await _userService.Register(registerRequest.UserName,
                registerRequest.Password,
                registerRequest.Email,
                cancellationToken);

            return Ok("User has been successfully registered");
        }

    }
}


