using Cinema.Domain.Models;
using Cinema.Interfaces;
using Cinema.Options;
using Microsoft.Extensions.Options;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.User
{
    public class RefreshUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly HttpContext _context;
        private readonly AuthOptions _authOptions;
        private readonly JwtOptions _jwtOptions;

        public RefreshUseCase(IUserRepository userRepository,
            IRefreshTokenRepository tokenRepository,
            IJwtProvider jwtProvider,
            IHttpContextAccessor contextAccessor,
            IOptions<AuthOptions> authOptions,
            IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _jwtProvider = jwtProvider;
            _context = contextAccessor.HttpContext;
            _authOptions = authOptions.Value;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<string>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var oldToken = _context.Request.Cookies[_authOptions.RefreshTokenName];

            var jti = _jwtProvider.GetJtiFromToken(oldToken);

            var refreshTokenEntity = await _tokenRepository.FindAsync(jti, cancellationToken);

            if (refreshTokenEntity != null)
            {
                return Error.Unauthorized("Invalid refresh token. Please re-authenticate.");
            }

            if (refreshTokenEntity.ExpiryAt < DateTime.UtcNow)
            {
                return Error.Unauthorized("Refresh token has expired. Please re-authenticate.");
            }

            var userId = _jwtProvider.GetUserIdFromToken(oldToken);

            var accessToken = _jwtProvider.GenerateAccessToken(userId);
            var refreshToken = _jwtProvider.GenerateRefreshToken(userId);

            _context.Response.Cookies.Append(_authOptions.AcсessTokenName, accessToken);
            _context.Response.Cookies.Append(_authOptions.RefreshTokenName, refreshToken);

            await _tokenRepository.UseToken(jti, cancellationToken);

            await _tokenRepository.Add(userId,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(_jwtOptions.RefreshExpiresDays),
                jti = _jwtProvider.GetJtiFromToken(refreshToken),
                refreshToken,
                cancellationToken);

            return Result.Success("Tokens have been successfully updated");
        }
    }
}