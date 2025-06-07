
namespace Cinema.Infrastucture.Repositories
{
    public interface IRoleRepository
    {
        Task<List<int>> GetPermissionAsync(int roleId, CancellationToken cancellationToken);
    }
}