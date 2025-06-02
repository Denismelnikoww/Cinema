using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
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

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id,
            CancellationToken cancellationToken)
        {
            var bookingEntity = await _bookingRepository.GetById(id, cancellationToken);

            if (bookingEntity == null)
            {
                return BadRequest("Booking with this Id does not exist");
            }

            return Ok(Mapper.MapToDto(bookingEntity));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBySessionId(int id,
            CancellationToken cancellationToken)
        {
            var bookingsEntity = await _bookingRepository.GetBySessionId(id, cancellationToken);

            if (bookingsEntity.Count == 0)
            {
                return BadRequest("Booking on this session does not exist");
            }

            List<BookingDto> bookingsDto = [];

            foreach (var bookingEntity in bookingsEntity)
            {
                bookingsDto.Add(Mapper.MapToDto(bookingEntity));
            }

            return Ok(bookingsDto);
        }

        [HttpGet("[action]/{id}")]
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
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(bookingDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return BadRequest(validateResult.ToDictionary());
            }

            await _bookingRepository.Add(bookingDto, cancellationToken);

            return Ok();
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
