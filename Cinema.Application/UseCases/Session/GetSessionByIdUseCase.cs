using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Session
{
    public class GetSessionByIdUseCase
    {
        private readonly ISessionRepository _sessionRepository;

        public GetSessionByIdUseCase(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<SessionDto>> ExecuteAsync(int id, 
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.FindAsync(id, cancellationToken);

            if (session == null)
            {
                return Error.BadRequest("Session with this id does not exist");
            }

            return Result.Success(Mapper.MapToDto(session));
        }
    }
}