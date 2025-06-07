using Cinema.Interfaces;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.User
{
    public class DeleteUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(id, cancellationToken);

            if (user == null)
            {
                return Error.BadRequest("Incorrect ID");
            }

            await _userRepository.DeleteAsync(id, cancellationToken);

            return Result.Success("The user has been deleted");
        }
    }
}