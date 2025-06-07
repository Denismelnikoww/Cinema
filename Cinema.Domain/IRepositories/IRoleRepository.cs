
namespace Cinema.Infrastucture.Repositories
{
    public interface IRoleRepository
    {
        Task<List<int>> GetPermission(int roleId, CancellationToken cancellationToken);
    }
}