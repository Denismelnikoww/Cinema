using Cinema.Exceptions;
using Cinema.Options;
using Cinema.Models;
using Microsoft.Extensions.Options;

namespace Cinema.Interfaces
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly HttpContext _context;
        private readonly AuthOptions _authOptions;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IOptions<AuthOptions> authOptions,
            IOptions<JwtOptions> jwtOptions,
            IHttpContextAccessor httpContextAccessor,
            IRefreshTokenRepository refreshToken)
        {
            _tokenRepository = refreshToken;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _context = httpContextAccessor.HttpContext;
            _authOptions = authOptions.Value;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task Register(string userName, string password,
            string email, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            await _userRepository.AddAsync(userName, hashedPassword, email, cancellationToken);
        }

        public async Task Login(string email,
            string password,
            UserEntity user,
            CancellationToken cancellationToken)
        {
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new BadRequestException("Failed to login");
            }

            var accessToken = _jwtProvider.GenerateAccessToken(user.Id);
            var refreshToken = _jwtProvider.GenerateRefreshToken(user.Id);

            await _tokenRepository.Add(user.Id,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(_jwtOptions.RefreshExpiresDays),
                _jwtProvider.GetJtiFromToken(refreshToken),
                refreshToken,
                cancellationToken
                );

            _context.Response.Cookies.Append(_authOptions.AcсessTokenName, accessToken);
            _context.Response.Cookies.Append(_authOptions.RefreshTokenName, refreshToken);
        }
    }
}