using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using Cinema.Repositories;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionRepository _sessionRepository;

        public SessionController(SessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
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
        public async Task<IActionResult> CreateSession([FromBody] SessionDto session,
            CancellationToken cancellationToken)
        {
            await _sessionRepository.Create(session, cancellationToken);

            return Ok("Session was successfully created");
        }
    }
}
