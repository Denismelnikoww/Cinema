using Cinema.Contracts;
using Cinema.Interfaces;
using Cinema.Options;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Text.Json;

namespace Cinema.Application.UseCases.User
{
    public class LoginUseCase
    {
        private readonly IUserService _userService;
        private readonly AuthOptions _authOptions;
        private readonly IValidator<LoginRequest> _validator;
        private readonly HttpContext _httpContext;

        public LoginUseCase(
            IUserService userService,
            IOptions<AuthOptions> authOptions,
            IValidator<LoginRequest> validator,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _authOptions = authOptions.Value;
            _validator = validator;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<Result<string>> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer.Serialize(
                    validateResult.ToDictionary()));
            }

            var token = await _userService.Login(request.Email, request.Password, cancellationToken);
            _httpContext.Response.Cookies.Append(_authOptions.CookieName, token);

            return Result.Success("");
        }
    }
}