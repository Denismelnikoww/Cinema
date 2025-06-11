using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IAuthService

    {
        Task Login(string email, string password, UserEntity user, CancellationToken cancellationToken);
        Task Register(string userName, string password, string email, CancellationToken cancellationToken);
    }
}