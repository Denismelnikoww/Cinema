using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Session
{
    public class DeleteSessionByIdUseCase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly GetSessionByIdUseCase _getSessionByIdUseCase;

        public DeleteSessionByIdUseCase(
            ISessionRepository sessionRepository,
            GetSessionByIdUseCase getSessionByIdUseCase)
        {
            _sessionRepository = sessionRepository;
            _getSessionByIdUseCase = getSessionByIdUseCase;
        }

        public async Task<Result<string>> ExecuteAsync(int id, 
            CancellationToken cancellationToken)
        {
            var sessionResult = await _getSessionByIdUseCase.ExecuteAsync(id, cancellationToken);

            if (sessionResult.IsFailure)
            {
                return Error.BadRequest("Incorrect ID");
            }

            await _sessionRepository.DeleteAsync(id, cancellationToken);

            return Result.Success("The session has been deleted");
        }
    }
}