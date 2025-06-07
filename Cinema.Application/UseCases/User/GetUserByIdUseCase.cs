using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.User
{
    public class GetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(id, cancellationToken);

            if (user == null)
            {
                return Error.BadRequest("Пользователь не существует");
            }

            return Result.Success(Mapper.MapToDto(user));
        }
    }
}