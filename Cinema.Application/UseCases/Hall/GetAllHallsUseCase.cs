using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Hall
{
    public class GetAllHallsUseCase
    {
        private readonly IHallRepository _hallRepository;

        public GetAllHallsUseCase(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result<List<HallDto>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var halls = await _hallRepository.GetAllAsync(cancellationToken);

            var hallsDto = halls.Select(Mapper.MapToDto).ToList();

            return Result.Success(hallsDto);
        }
    }
}