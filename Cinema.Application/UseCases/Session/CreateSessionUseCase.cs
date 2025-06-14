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
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IValidator<SessionDto> _validator;

        public CreateSessionUseCase(ISessionRepository sessionRepository,
            IMovieRepository movieRepository,
            IHallRepository hallRepository,
            IValidator<SessionDto> validator)
        {
            _sessionRepository = sessionRepository;
            _validator = validator;
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
        }

        public async Task<Result<string>> ExecuteAsync(SessionDto sessionDto,
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(sessionDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(validateResult.ToDictionary()));
            }

            var movie = await _movieRepository.FindAsync(sessionDto.MovieId, cancellationToken);

            if (movie == null || movie.IsDeleted)
            {
                return Error.BadRequest("The movie does not exist");
            }
            if (movie.Duration > sessionDto.Duration)
            {
                return Error.BadRequest("The duration of the session " +
                    "cannot be less than a movie.");
            }

            var hall = await _hallRepository.FindAsync(sessionDto.HallId, cancellationToken);

            if (hall == null || hall.IsDeleted)
            {
                return Error.BadRequest("There is no hall with such an ID");
            }
            if (!hall.IsWorking)
            {
                return Error.BadRequest("This hall is temporarily/permanently closed");
            }

            var sessions = await _sessionRepository.GetAllByHallAsync(hall.Id, cancellationToken);
            if (sessions.Any(s =>
                   (sessionDto.DateTime < s.DateTime + s.Duration) &&
                   (sessionDto.DateTime + sessionDto.Duration > s.DateTime)))
            {
                return Error.BadRequest("The session time is adjusted to another session");
            }

            await _sessionRepository.AddAsync(sessionDto.MovieId,
                                         sessionDto.DateTime,
                                         sessionDto.HallId,
                                         sessionDto.Price,
                                         sessionDto.Duration,
                                         cancellationToken);

            return Result.Success("Session was successfully created");
        }
    }
}