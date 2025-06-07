using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Text.Json;

namespace Cinema.Application.UseCases.User
{
    public class RegisterUseCase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterRequest> _validator;

        public RegisterUseCase(
            IUserService userService,
            IUserRepository userRepository,
            IValidator<RegisterRequest> validator)
        {
            _userService = userService;
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<Result<string>> ExecuteAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(
                    validateResult.ToDictionary()));
            }

            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (existingUser != null)
            {
                return Error.BadRequest("An account has already been registered for this email");
            }

            await _userService.Register(
                request.UserName,
                request.Password,
                request.Email,
                cancellationToken);

            return Result.Success("User has been successfully registered");
        }
    }
}