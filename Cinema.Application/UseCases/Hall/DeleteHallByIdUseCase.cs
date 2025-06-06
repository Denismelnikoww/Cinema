using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Hall
{
    public class DeleteHallByIdUseCase
    {
        private readonly IHallRepository _hallRepository;
        private readonly GetHallByIdUseCase _getHallByIdUseCase;

        public DeleteHallByIdUseCase(IHallRepository hallRepository,
            GetHallByIdUseCase getHallByIdUseCase)
        {
            _hallRepository = hallRepository;
            _getHallByIdUseCase = getHallByIdUseCase;
        }

        public async Task<Result<string>> ExecuteAsync(int id,
            CancellationToken cancellationToken)
        {
            var hallResult = await _getHallByIdUseCase.ExecuteAsync(id, cancellationToken);

            if (hallResult.IsFailure)
            {
                return Error.BadRequest("Incorrect ID");
            }

            await _hallRepository.DeleteById(id, cancellationToken);
            return Result.Success("The hall has been deleted");
        }
    }
}