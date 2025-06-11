using Microsoft.AspNetCore.Mvc;
using Cinema.Application.UseCases.Movie;
using Microsoft.AspNetCore.Authorization;
using Cinema.Contracts;
using ResultSharp.HttpResult;
using Cinema.API.Attribute;
using Cinema.Enums;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(
            [FromServices] GetAllMoviesUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPage(
            int page,
            int pageSize,
            [FromServices] GetMoviesByPageUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(page, pageSize, cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]/{title}")]
        public async Task<IActionResult> GetByTitle(
            string title,
            [FromServices] GetMoviesByNameUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(title, cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(
            int id,
            [FromServices] GetMovieByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Create)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(
            [FromBody] MovieDto movieDto,
            [FromServices] CreateMovieUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(movieDto, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Delete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] DeleteMovieByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.SuperDelete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDelete(
            int id,
            [FromServices] SuperDeleteMovieByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }
    }
}