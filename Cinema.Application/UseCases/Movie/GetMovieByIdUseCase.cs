using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Movie
{
    public class GetMovieByIdUseCase
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdUseCase(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result<MovieDto>> ExecuteAsync(int id, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetById(id, cancellationToken);

            if (movie == null)
            {
                return Error.NotFound("Movie with this id does not exist");
            }

            return Result.Success(Mapper.MapToDto(movie));
        }
    }
}