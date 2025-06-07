using Microsoft.AspNetCore.Mvc;
using Cinema.Application.UseCases.Session;
using ResultSharp.Core;
using Cinema.Contracts;
using ResultSharp.HttpResult;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(
            int id,
            [FromServices] GetSessionByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByMovie(
            int movieId,
            [FromServices] GetSessionsByMovieUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(movieId, cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByHall(
            int hallId,
            [FromServices] GetSessionsByHallUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(hallId, cancellationToken);
            return result.ToResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(
            [FromBody] SessionDto sessionDto,
            [FromServices] CreateSessionUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(sessionDto, cancellationToken);
            return result.ToResponse();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] DeleteSessionByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDelete(
            int id,
            [FromServices] SuperDeleteSessionByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }
    }
}