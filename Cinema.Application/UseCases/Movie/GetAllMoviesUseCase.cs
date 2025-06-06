using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;

namespace Cinema.Application.UseCases.Movie
{
    public class GetAllMoviesUseCase
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesUseCase(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result<List<MovieDto>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAll(cancellationToken);

            var moviesDto = movies.Select(Mapper.MapToDto).ToList();

            return Result.Success(moviesDto);
        }
    }
}