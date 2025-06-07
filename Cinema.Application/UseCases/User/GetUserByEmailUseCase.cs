using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.User
{
    public class GetUserByEmailUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> ExecuteAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user == null)
            {
                return Error.BadRequest("Пользователь не существует");
            }

            return Result.Success(Mapper.MapToDto(user));
        }
    }
}