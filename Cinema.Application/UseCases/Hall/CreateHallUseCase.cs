using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using ResultSharp.Core;
using ResultSharp.Errors;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace Cinema.Application.UseCases.Hall
{
    public class CreateHallUseCase
    {
        private readonly IHallRepository _hallRepository;
        private readonly IValidator<HallDto> _validator;

        public CreateHallUseCase(IHallRepository hallRepository,
            IValidator<HallDto> validator)
        {
            _hallRepository = hallRepository;
            _validator = validator;
        }

        public async Task<Result<string>> ExecuteAsync(HallDto hallDto,
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(hallDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(validateResult.ToDictionary()));
            }

            await _hallRepository.AddAsync(hallDto.Name,
                                      hallDto.CountSeats,
                                      hallDto.IsWorking,
                                      cancellationToken);

            return Result.Success("The hall was successfully created");
        }
    }
}