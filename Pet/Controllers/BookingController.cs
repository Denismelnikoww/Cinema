using Cinema.API.Attribute;
using Cinema.API.Controllers;
using Cinema.Application.UseCases.Booking;
using Cinema.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.HttpResult;
using Cinema.Enums;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        [RequirementsPermission(Permission.SuperRead)]
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id,
            [FromServices] GetBookingByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("[action]/{sessionId:int}")]
        public async Task<IActionResult> GetBySessionId(int sessionId,
            [FromServices] GetBookingBySessionIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(sessionId, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("[action]/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId,
            [FromServices] GetBookingByUserIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(userId, cancellationToken);

            return result.ToResponse();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] BookingDto bookingDto,
            [FromServices] CreateBookingUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(bookingDto, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteById(int id,
            [FromServices] DeleteBookingByIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> SuperDeleteById(int id,
            [FromServices]SuperDeleteBookingById useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(id, cancellationToken);

            return result.ToResponse();
        }
    }
}
