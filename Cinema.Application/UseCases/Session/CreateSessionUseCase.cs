using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Text.Json;

namespace Cinema.Application.UseCases.Session
{
    public class CreateSessionUseCase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IValidator<SessionDto> _validator;

        public CreateSessionUseCase(ISessionRepository sessionRepository, IValidator<SessionDto> validator)
        {
            _sessionRepository = sessionRepository;
            _validator = validator;
        }

        public async Task<Result<string>> ExecuteAsync(SessionDto sessionDto,
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(sessionDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(validateResult.ToDictionary()));
            }

            await _sessionRepository.Add(sessionDto.MovieId,
                                         sessionDto.DateTime,
                                         sessionDto.HallId,
                                         sessionDto.Price,
                                         sessionDto.Duration,
                                         cancellationToken);

            return Result.Success("Session was successfully created");
        }
    }
}