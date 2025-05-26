using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var listMovies = await _repository.GetAll();
                return Ok(listMovies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MovieDto movieDto)
        {
            try
            {
                await _repository.Add(movieDto.Title,
                                movieDto.Author,
                                movieDto.Rating,
                                movieDto.Description,
                                movieDto.Duration);

                return Ok($"Film {movieDto.Title} successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {
            try
            {
                return Ok(await _repository.GetByPage(page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }

        }

        [HttpGet("[action]/{title}")]
        public async Task<IActionResult> GetByName(string title)
        {
            try
            {
                var movies = await _repository.GetFilterTitle(title);

                if (movies.Count == 0)
                {
                    throw new ArgumentException("No found");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var movies = await _repository.GetById(id);

                if (movies == null)
                {
                    throw new ArgumentException("No found");
                }

                return Ok(movies);

            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }


    }

    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Author { get; set; }
        public float Rating { get; set; }
    }
}
