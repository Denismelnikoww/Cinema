using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Application.UseCases.Hall;
using ResultSharp.HttpResult;
using Cinema.API.Attribute;
using Cinema.Enums;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HallController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(
            [FromServices] GetAllHallsUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(cancellationToken);
            return result.ToResponse();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(int id,
            [FromServices] GetHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Create)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] HallDto hallDto,
            [FromServices] CreateHallUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(hallDto, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Delete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id,
            [FromServices] DeleteHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.SuperDelete)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDelete(int id,
            [FromServices] SuperDeleteHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [RequirementsPermission(Permission.Create)]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromBody] HallDto hall, int id,
            [FromServices] UpdateHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, hall, cancellationToken);
            return result.ToResponse();
        }
    }
}



