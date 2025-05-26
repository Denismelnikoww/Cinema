using Microsoft.AspNetCore.Mvc;
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


        public UserController(UserService userService, UserRepository userRepository)
        {
            _repository = userRepository;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.Login(loginRequest.Email, loginRequest.Password);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            await _userService.Register(registerRequest.UserName,
                registerRequest.Password,
                registerRequest.Email);

            return Ok("User has been successfully registered");
        }

    }



    public record RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public record LoginRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}


