using Cinema.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Cinema.Contracts;
using ResultSharp.HttpResult;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            [FromServices] LoginUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request,
            [FromServices] RegisterUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] GetUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(
            string email,
            [FromServices] GetUserByEmailUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(email, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(
            int id,
            [FromServices] DeleteUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("SuperDeleteById/{id}")]
        public async Task<IActionResult> SuperDeleteById(
            int id,
            [FromServices] SuperDeleteUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }
    }
}