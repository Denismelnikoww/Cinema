using Microsoft.AspNetCore.Mvc;
using Cinema.Contracts;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<MovieDto> _validator;
        public MovieController(IMovieRepository movieRepository,
            IValidator<MovieDto> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAll(cancellationToken);

            var moviesDto = new List<MovieDto>();

            foreach (var movie in movies)
            {
                moviesDto.Add(Mapper.MapToDto(movie));
            }

            return Ok(moviesDto);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPage(int page, int pageSize, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetByPage(page, pageSize, cancellationToken);

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

            var movies = await _movieRepository.GetFilterTitle(title, cancellationToken);

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

            var movie = await _movieRepository.GetById(id, cancellationToken);

            if (movie == null)
            {
                return NotFound("Movie with this id does not exist");
            }

            var movieDto = Mapper.MapToDto(movie);

            return Ok(movieDto);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MovieDto movieDto,
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(movieDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return BadRequest(validateResult.ToDictionary());
            }

           // await _movieRepository.Add(movieDto, cancellationToken);

            return Ok($"Film {movieDto.Title} successfully created");

        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(int id,
            CancellationToken cancellationToken)
        {
            var delete = await GetById(id, cancellationToken);

            if (delete == null)
            {
                return BadRequest("Incorrect ID");
            }

            await _movieRepository.DeleteById(id, cancellationToken);
            return Ok("The movie has been deleted");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> SuperDeleteById(int id,
    CancellationToken cancellationToken)
        {
            var delete = await GetById(id, cancellationToken);

            if (delete == null)
            {
                return BadRequest("Incorrect ID");
            }

            await _movieRepository.SuperDeleteById(id, cancellationToken);
            return Ok("The movie has been deleted");
        }
    }
}
