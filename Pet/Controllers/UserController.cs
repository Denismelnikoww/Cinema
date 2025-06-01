using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Cinema.Options;
using Cinema.Repositories;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Services;

namespace Cinema.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IValidator<Contracts.LoginRequest> _loginValidator;
        private readonly IValidator<Contracts.RegisterRequest> _registerValidator;


        public UserController(
            IUserService userService,
            IUserRepository userRepository,
            IOptions<AuthOptions> authOptions,
            IValidator<Contracts.RegisterRequest> registerValidator,
            IValidator<Contracts.LoginRequest> loginValidator)
        {
            _repository = userRepository;
            _userService = userService;
            _authOptions = authOptions;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] Contracts.LoginRequest loginRequest,
                                               CancellationToken cancellationToken)
        {
            var result = await _loginValidator.ValidateAsync(loginRequest, cancellationToken);
            
            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            var token = await _userService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);

            Response.Cookies.Append(_authOptions.Value.CookieName, token);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] Contracts.RegisterRequest registerRequest,
            CancellationToken cancellationToken)
        {
            var result = await _registerValidator.ValidateAsync(registerRequest, cancellationToken);

            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            await _userService.Register(registerRequest.UserName,
                registerRequest.Password,
                registerRequest.Email,
                cancellationToken);

            return Ok("User has been successfully registered");
        }

    }
}


