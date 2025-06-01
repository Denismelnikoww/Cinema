
namespace Cinema.Services
{
    public interface IUserService
    {
        Task<string> Login(string email, string password, CancellationToken cancellationToken);
        Task Register(string userName, string password, string email, CancellationToken cancellationToken);
    }
}