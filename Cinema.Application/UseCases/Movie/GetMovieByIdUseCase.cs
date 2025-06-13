using Cinema.Contracts;
using Cinema.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Text.Json;

namespace Cinema.Application.UseCases.Movie
{
    public class GetMovieByIdUseCase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IDistributedCache _distributedCache;

        public GetMovieByIdUseCase(IMovieRepository movieRepository,
            IDistributedCache distributedCache)
        {
            _movieRepository = movieRepository;
            _distributedCache = distributedCache;
        }

        public async Task<Result<MovieDto>> ExecuteAsync(int id,
            CancellationToken cancellationToken)
        {
            string cacheKey = $"movie_{id}";

            var cachedMovie = await _distributedCache.GetStringAsync(cacheKey, 
                cancellationToken);

            if (cachedMovie != null)
            {
                return Result.Success(JsonSerializer.Deserialize<MovieDto>(cachedMovie));
            }

            var movie = await _movieRepository.FindAsync(id, cancellationToken);

            if (movie == null)
            {
                return Error.NotFound("Movie with this id does not exist");
            }

            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(Mapper.MapToDto(movie)),
                cacheOptions,
                cancellationToken);

            return Result.Success(Mapper.MapToDto(movie));
        }
    }
}