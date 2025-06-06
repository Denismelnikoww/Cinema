using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Text.Json;

namespace Cinema.Application.UseCases.Movie
{
    public class CreateMovieUseCase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<MovieDto> _validator;

        public CreateMovieUseCase(IMovieRepository movieRepository, IValidator<MovieDto> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<Result<string>> ExecuteAsync(MovieDto movieDto, 
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(movieDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(validateResult.ToDictionary()));
            }

            await _movieRepository.Add(
                movieDto.Author,
                movieDto.Description,
                movieDto.Rating,
                movieDto.Duration,
                movieDto.Title,
                cancellationToken);

            return Result.Success($"Film {movieDto.Title} successfully created");
        }
    }
}