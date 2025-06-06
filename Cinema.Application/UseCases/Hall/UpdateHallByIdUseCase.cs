using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;

namespace Cinema.Application.UseCases.Hall
{
    public class UpdateHallByIdUseCase
    {
        private readonly IHallRepository _hallRepository;

        public UpdateHallByIdUseCase(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result<string>> ExecuteAsync(int id, HallDto hall,
            CancellationToken cancellationToken)
        {
            await _hallRepository.UpdateById(
                id,
                hall.CountSeats,
                hall.Name,
                hall.IsWorking,
                cancellationToken);

            return Result.Success("The information has been updated");
        }
    }
}