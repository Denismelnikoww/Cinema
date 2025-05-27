using Microsoft.AspNetCore.Mvc;
using Pet.Contracts;
using Pet.Models;
using Pet.Repositories;

namespace Pet.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _repository;
        public MovieController(MovieRepository movieRepository) { _repository = movieRepository; }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var listMovies = await _repository.GetAll();
            return Ok(listMovies);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MovieDto movieDto)
        {

            await _repository.Add(movieDto.Title,
                            movieDto.Author,
                            movieDto.Rating,
                            movieDto.Description,
                            movieDto.Duration);

            return Ok($"Film {movieDto.Title} successfully created");

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {

            return Ok(await _repository.GetByPage(page, pageSize));

        }

        [HttpGet("[action]/{title}")]
        public async Task<IActionResult> GetByName(string title)
        {

            var movies = await _repository.GetFilterTitle(title);

            if (movies.Count == 0)
            {
                throw new Exception("No found");
            }

            return Ok(movies);

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var movies = await _repository.GetById(id);

            if (movies == null)
            {
                throw new Exception("No found");
            }

            return Ok(movies);
        }
    }
}
