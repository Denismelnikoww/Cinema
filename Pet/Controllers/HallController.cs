using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Application.UseCases.Hall;
using ResultSharp.HttpResult;

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

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id,
            [FromServices] GetHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] HallDto hallDto,
            [FromServices] CreateHallUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(hallDto, cancellationToken);
            return result.ToResponse();
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteById(int id,
            [FromServices] DeleteHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> SuperDeleteById(int id,
            [FromServices] SuperDeleteHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);
            return result.ToResponse();
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateById([FromBody] HallDto hall, int id,
            [FromServices] UpdateHallByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, hall, cancellationToken);
            return result.ToResponse();
        }
    }
}



