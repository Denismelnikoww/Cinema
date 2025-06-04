using Cinema.Application.UseCases.Booking;
using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<BookingDto> _validator;

        public BookingController(IBookingRepository bookingRepository,
            IValidator<BookingDto> validator)
        {
            _bookingRepository = bookingRepository;
            _validator = validator;
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id,
                                                 CancellationToken cancellationToken,
                                                 [FromServices] GetBookingById useCase)
        {
            var useCaseResult = await useCase.Execute(id, cancellationToken);

            return useCaseResult;
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetBySessionId(int id,
                                                        CancellationToken cancellationToken,
                                                        [FromServices] GetBookingBySessionId useCase)
        {
            var useCaseResult = await useCase.Execute(id, cancellationToken);

            return useCaseResult;
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByUserId(int id,
            CancellationToken cancellationToken)
        {
            var bookingsEntity = await _bookingRepository.GetByUserId(id, cancellationToken);

            if (bookingsEntity.Count == 0)
            {
                return BadRequest("Booking on this user does not exist");
            }

            List<BookingDto> bookingsDto = [];

            foreach (var bookingEntity in bookingsEntity)
            {
                bookingsDto.Add(Mapper.MapToDto(bookingEntity));
            }

            return Ok(bookingsDto);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] BookingDto bookingDto,
                                                [FromServices] CreateBookingUseCase useCase,
                                                CancellationToken cancellationToken)
        {
            var useCaseResult = await useCase.Execute(bookingDto, cancellationToken);

            return useCaseResult;
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

            await _bookingRepository.DeleteById(id, cancellationToken);

            return Ok("The booking has been deleted");
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

            await _bookingRepository.SuperDeleteById(id, cancellationToken);

            return Ok("The booking has been deleted");
        }
    }
}
