using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using Cinema.Repositories;
using FluentValidation;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionRepository _sessionRepository;
        private readonly IValidator<SessionDto> _validator;

        public SessionController(SessionRepository sessionRepository,
            IValidator<SessionDto> validator)
        {
            _sessionRepository = sessionRepository;
            _validator = validator; 
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id,
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetById(id, cancellationToken);

            if (session == null)
            {
                return NotFound("Session with this id does not exist");
            }

            return Ok(session);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByMovie(int movieId,
            CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.GetAllByMovie(movieId, cancellationToken);

            if (sessions.Count == 0)
            {
                return NotFound("Sessions with this movie does not exist");
            }

            return Ok(sessions);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByHall(int hallId,
            CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.GetAllByHall(hallId, cancellationToken);

            if (sessions.Count == 0)
            {
                return NotFound("Sessions with in this hall does not exist");
            }

            return Ok(sessions);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto,
            CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(sessionDto, cancellationToken);

            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            await _sessionRepository.Create(sessionDto, cancellationToken);

            return Ok("Session was successfully created");
        }
    }
}
