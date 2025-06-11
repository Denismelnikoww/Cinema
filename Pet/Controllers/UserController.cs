using Cinema.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Cinema.Contracts;
using ResultSharp.HttpResult;
using Cinema.API.Attribute;
using Cinema.Enums;
using System.Threading;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            [FromServices] LoginUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request,
            [FromServices] RegisterUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Refresh(
            [FromServices] RefreshUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(cancellationToken);

            return result.ToResponse();
        }

        [RequirementsPermission(Permission.SuperRead)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(
            int id,
            [FromServices] GetUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [RequirementsPermission(Permission.SuperRead)]
        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> GetByEmail(
            string email,
            [FromServices] GetUserByEmailUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(email, cancellationToken);

            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Delete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] DeleteUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [RequirementsPermission(Permission.SuperDelete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDelete(
            int id,
            [FromServices] SuperDeleteUserByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }
    }
}