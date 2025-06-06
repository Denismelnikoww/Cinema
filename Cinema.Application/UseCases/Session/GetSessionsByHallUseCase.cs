using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Session
{
    public class GetSessionsByHallUseCase
    {
        private readonly ISessionRepository _sessionRepository;

        public GetSessionsByHallUseCase(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<List<SessionDto>>> ExecuteAsync(int hallId, 
            CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.GetAllByHall(hallId, cancellationToken);

            if (sessions.Count == 0)
            {
                return Error.NotFound("Sessions in this hall does not exist");
            }

            var sessionsDto = sessions.Select(Mapper.MapToDto).ToList();
            return Result.Success(sessionsDto);
        }
    }
}