using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Repositories;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IValidator<SessionDto> _validator;

        public SessionController(ISessionRepository sessionRepository,
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
            var validateResult = await _validator.ValidateAsync(sessionDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return BadRequest(validateResult.ToDictionary());
            }

            await _sessionRepository.Create(sessionDto, cancellationToken);

            return Ok("Session was successfully created");
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

            await _sessionRepository.DeleteById(id, cancellationToken);
            return Ok("The session has been deleted");
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

            await _sessionRepository.SuperDeleteById(id, cancellationToken);
            return Ok("The session has been deleted");
        }
    }
}
