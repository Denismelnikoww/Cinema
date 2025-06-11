using Microsoft.Extensions.Options;
using Cinema.Models;
using Cinema.Options;
using System.Security.Claims;
using System.Text;
using Cinema.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Cinema.Domain.Exceptions;

namespace Cinema.Infrastucture.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateAccessToken(int userId)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.AcсessSecretKey)),
                SecurityAlgorithms.HmacSha256);

            Claim[] claims = [new("userId", userId.ToString())];

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddHours(_options.AccessExpiresHours)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
        public string GenerateRefreshToken(int userId)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.RefreshSecretKey)),
                SecurityAlgorithms.HmacSha256);

            var jti = Guid.NewGuid().ToString();

            Claim[] claims = [
                new("userId", userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, jti)
            ];

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(_options.RefreshExpiresDays)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GetJtiFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
                throw new UnauthorizedException("Invalid JWT.Please re-authenticate.");

            var jwtToken = tokenHandler.ReadJwtToken(token);
            var jti = jwtToken.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrEmpty(jti))
                throw new UnauthorizedException("JTI claim not found. Please re-authenticate.");

            return jti;
        }

        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
                throw new UnauthorizedException("Invalid JWT.Please re-authenticate.");

            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userId = jwtToken.Claims
                .FirstOrDefault(c => c.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedException("User ID not found. Please re-authenticate.");

            return int.Parse(userId);
        }
    }
}
