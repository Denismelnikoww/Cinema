using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using FluentValidation;
using Cinema.Interfaces;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HallController : ControllerBase
    {
        private readonly IHallRepository _hallRepository;
        private readonly IValidator<HallDto> _validator;

        public HallController(IHallRepository hallRepository,
            IValidator<HallDto> validator)
        {
            _hallRepository = hallRepository;
            _validator = validator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] HallDto hallDto,
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(hallDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return BadRequest(validateResult.ToDictionary());
            }

            await _hallRepository.Add(hallDto, cancellationToken);

            return Ok("The hall was successfully created");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {

            var halls = await _hallRepository.GetAll(cancellationToken);

            var hallsDto = new List<HallDto>();

            foreach (var hall in halls)
            {
                var hallDto = Mapper.MapToDto(hall);

                hallsDto.Add(hallDto);
            }

            return Ok(hallsDto);

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {

            var hall = await _hallRepository.GetById(id, cancellationToken);

            if (hall == null)
            {
                return NotFound("Hall with this id does not exist");
            }

            var hallDto = Mapper.MapToDto(hall);

            return Ok(hallDto);

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

            await _hallRepository.DeleteById(id, cancellationToken);
            return Ok("The hall has been deleted");
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

            await _hallRepository.SuperDeleteById(id, cancellationToken);
            return Ok("The hall has been deleted");
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateById([FromBody] HallDto hall, int id,
            CancellationToken cancellationToken)
        {

            await _hallRepository.UpdateById(id,
                hall.CountSeats,
                hall.Name,
                hall.IsWorking,
                cancellationToken);

            return Ok("The information has been updated");

        }
    }
}



