using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IMovieRepository
    {
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task SuperDeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<MovieEntity?> FindAsync(int id, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetByPageAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetFilterTitleAsync(string title, CancellationToken cancellationToken);
        Task AddAsync(string author, string description, float rating, TimeSpan duration, string title, CancellationToken cancellationToken);
    }
}