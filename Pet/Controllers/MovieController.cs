using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using Cinema.Models;
using Cinema.Repositories;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _repository;
        public MovieController(MovieRepository movieRepository)
        {
            _repository = movieRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var movies = await _repository.GetAll(cancellationToken);

            var moviesDto = new List<MovieDto>();

            foreach (var movie in movies)
            {
                moviesDto.Add(Mapper.MapToDto(movie));
            }

            return Ok(moviesDto);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MovieDto movieDto,
            CancellationToken cancellationToken)
        {

            await _repository.Add(movieDto, cancellationToken);

            return Ok($"Film {movieDto.Title} successfully created");

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPage(int page, int pageSize, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetByPage(page, pageSize, cancellationToken);

            var moviesDto = new List<MovieDto>();
            foreach (var movie in movies)
            {
                moviesDto.Add(Mapper.MapToDto(movie));
            }

            return Ok(moviesDto);
        }

        [HttpGet("[action]/{title}")]
        public async Task<IActionResult> GetByName(string title, CancellationToken cancellationToken)
        {

            var movies = await _repository.GetFilterTitle(title, cancellationToken);

            if (movies.Count == 0)
            {
                return NotFound("Movies with this title does not exist");
            }

            var moviesDto = new List<MovieDto>();

            foreach (var movie in movies)
            {
                moviesDto.Add(Mapper.MapToDto(movie));
            }

            return Ok(moviesDto);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetById(id, cancellationToken);

            if (movie == null)
            {
                return NotFound("Movie with this id does not exist");
            }

            var movieDto = Mapper.MapToDto(movie);

            return Ok(movieDto);
        }
    }
}
