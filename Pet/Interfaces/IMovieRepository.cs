using Cinema.Contracts;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Interfaces
{
    public interface IMovieRepository
    {
        Task Add([FromBody] MovieDto movieDto, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetAll(CancellationToken cancellationToken);
        Task<MovieEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetByPage(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<MovieEntity>> GetFilterTitle(string title, CancellationToken cancellationToken);
    }
}