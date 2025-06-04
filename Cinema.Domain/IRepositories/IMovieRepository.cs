using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IMovieRepository
    {
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetAll(CancellationToken cancellationToken);
        Task<MovieEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetByPage(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetFilterTitle(string title, CancellationToken cancellationToken);
        Task Add(string author, string description, float rating, TimeSpan duration, string title, CancellationToken cancellationToken);
    }
}