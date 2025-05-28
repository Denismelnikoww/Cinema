using Pet.Infrastructure;
using Pet.Repositories;
using System.Security.Authentication;

namespace Pet.Services
{
    public class UserService 
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly UserRepository _userRepository;
        private readonly JwtProvider _jwtProvider;

        public UserService(UserRepository userRepository,
            PasswordHasher passwordHasher,
            JwtProvider jwtProvider)
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