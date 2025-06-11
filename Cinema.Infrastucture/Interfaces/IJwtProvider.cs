using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(int userId);
        string GenerateRefreshToken(int userId);
        string GetJtiFromToken(string token);
        int GetUserIdFromToken(string token);
    }
}