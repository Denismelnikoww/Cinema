using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Movie
{
    public class GetMoviesByNameUseCase
    {
        private readonly IMovieRepository _movieRepository;

        public GetMoviesByNameUseCase(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result<List<MovieDto>>> ExecuteAsync(string title, 
            CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetFilterTitleAsync(title, cancellationToken);

            if (movies.Count == 0)
            {
                return Error.NotFound("Movies with this title does not exist");
            }

            var moviesDto = movies.Select(Mapper.MapToDto).ToList();
            return Result.Success(moviesDto);
        }
    }
}