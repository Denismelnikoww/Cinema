using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity userEntity);
    }
}