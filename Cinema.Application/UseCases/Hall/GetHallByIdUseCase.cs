using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Hall
{
    public class GetHallByIdUseCase
    {
        private readonly IHallRepository _hallRepository;

        public GetHallByIdUseCase(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result<HallDto>> ExecuteAsync(int id,
            CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetById(id, cancellationToken);

            if (hall == null)
            {
                return Error.BadRequest("Hall with this id does not exist");
            }

            return Result.Success(Mapper.MapToDto(hall));
        }
    }
}