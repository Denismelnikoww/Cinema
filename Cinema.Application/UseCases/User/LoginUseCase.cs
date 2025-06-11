using Cinema.Contracts;
using Cinema.Exceptions;
using Cinema.Interfaces;
using Cinema.Options;
using Cinema.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cinema.Application.UseCases.User
{
    public class LoginUseCase
    {
        private readonly IAuthService _authService;
        private readonly AuthOptions _authOptions;
        private readonly IValidator<LoginRequest> _validator;
        private readonly HttpContext _httpContext;
        private readonly IUserRepository _userRepository;

        public LoginUseCase(
            IAuthService userService,
            IOptions<AuthOptions> authOptions,
            IValidator<LoginRequest> validator,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = userService;
            _authOptions = authOptions.Value;
            _validator = validator;
            _httpContext = httpContextAccessor.HttpContext;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> ExecuteAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(loginRequest, cancellationToken);

            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(
                    validateResult.ToDictionary()));
            }

            var user = await _userRepository.GetByEmailAsync(loginRequest.Email, cancellationToken);

            if (user == null)
            {
                return Error.BadRequest("There is no user with this email address");
            }

            await _authService.Login(loginRequest.Email, loginRequest.Password, user, 
                cancellationToken);

            return Result.Success("You have successfully logged in");
        }
    }
}