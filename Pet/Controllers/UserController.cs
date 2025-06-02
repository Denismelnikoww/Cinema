using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Cinema.Options;
using Cinema.Repositories;
using FluentValidation;
using Cinema.Interfaces;

namespace Cinema.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;
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
            _usersRepository = userRepository;
            _userService = userService;
            _authOptions = authOptions;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] Contracts.LoginRequest loginRequest,
                                               CancellationToken cancellationToken)
        {
            var validateResult = await _loginValidator.ValidateAsync(loginRequest, cancellationToken);

            if (!validateResult.IsValid)
            {
                //return BadRequest(result.ToDictionary());
            }

            var token = await _userService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);

            Response.Cookies.Append(_authOptions.Value.CookieName, token);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] Contracts.RegisterRequest registerRequest,
           CancellationToken cancellationToken)
        {
            var validateResult = await _registerValidator.ValidateAsync(registerRequest, cancellationToken);

            if (!validateResult.IsValid)
            {
                //return BadRequest(validateResult.ToDictionary());
            }

            var isExist = await GetByEmail(registerRequest.Email,cancellationToken) 
                == null ? true : false ;

            if (isExist)
            {
                return BadRequest("An account has already been registered for this email");
            }

            await _userService.Register(registerRequest.UserName,
                registerRequest.Password,
                registerRequest.Email,
                cancellationToken);

            return Ok("User has been successfully registered");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id
            , CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetById(id, cancellationToken);

            if (userEntity == null)
            {
                return BadRequest("User with this id does not exist");
            }

            return Ok(userEntity);
        }

        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> GetByEmail(string email
            , CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetByEmail(email, cancellationToken);

            if (userEntity == null)
            {
                return BadRequest("User with this id does not exist");
            }

            return Ok(userEntity);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(int id,
            CancellationToken cancellationToken)
        {
            var delete = await GetById(id, cancellationToken);

            if (delete == null)
            {
                return BadRequest("Incorrect ID");
            }

            await _usersRepository.DeleteById(id, cancellationToken);
            return Ok("The user has been deleted");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDeleteById(int id,
    CancellationToken cancellationToken)
        {
            var delete = await GetById(id, cancellationToken);

            if (delete == null)
            {
                return BadRequest("Incorrect ID");
            }

            await _usersRepository.SuperDeleteById(id, cancellationToken);
            return Ok("The user has been deleted");
        }
    }
}


