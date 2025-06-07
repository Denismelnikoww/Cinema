using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;

namespace Cinema.Application.UseCases.Movie
{
    public class GetMoviesByPageUseCase
    {
        private readonly IMovieRepository _movieRepository;

        public GetMoviesByPageUseCase(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result<List<MovieDto>>> ExecuteAsync(int page, int pageSize, 
            CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetByPageAsync(page, pageSize, cancellationToken);

            var moviesDto = movies.Select(Mapper.MapToDto).ToList();

            return Result.Success(moviesDto);
        }
    }
}