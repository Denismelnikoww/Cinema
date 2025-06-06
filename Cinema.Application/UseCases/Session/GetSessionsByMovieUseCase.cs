using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Session
{
    public class GetSessionsByMovieUseCase
    {
        private readonly ISessionRepository _sessionRepository;

        public GetSessionsByMovieUseCase(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<List<SessionDto>>> ExecuteAsync(int movieId, 
            CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.GetAllByMovie(movieId, cancellationToken);

            if (sessions.Count == 0)
            {
                return Error.NotFound("Sessions with this movie does not exist");
            }

            var sessionsDto = sessions.Select(Mapper.MapToDto).ToList();
            return Result.Success(sessionsDto);
        }
    }
}