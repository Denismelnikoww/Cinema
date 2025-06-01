using Cinema.Infrastructure;
using Cinema.Repositories;
using Cinema.Services;
using System.Security.Authentication;

namespace Cinema.Interfaces
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task Register(string userName, string password,
            string email, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            await _userRepository.Add(userName, hashedPassword, email, false, cancellationToken);
        }

        public async Task<string> Login(string email, string password,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(email, cancellationToken);

            if (user == null)
            {
                throw new Exception("Users with this email not found");
            }

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

    }
}