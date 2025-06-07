using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Movie
{
    public class DeleteMovieByIdUseCase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly GetMovieByIdUseCase _getMovieByIdUseCase;

        public DeleteMovieByIdUseCase(
            IMovieRepository movieRepository,
            GetMovieByIdUseCase getMovieByIdUseCase)
        {
            _movieRepository = movieRepository;
            _getMovieByIdUseCase = getMovieByIdUseCase;
        }

        public async Task<Result<string>> ExecuteAsync(int id, 
            CancellationToken cancellationToken)
        {
            var movieResult = await _getMovieByIdUseCase.ExecuteAsync(id, cancellationToken);

            if (movieResult.IsFailure)
            {
                return Error.BadRequest("Incorrect ID");
            }

            await _movieRepository.DeleteAsync(id, cancellationToken);
            return Result.Success("The movie has been deleted");
        }
    }
}