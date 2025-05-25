using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pet.Repositories;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pet.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HallController : ControllerBase
    {
        private readonly HallRepository repository;

        public HallController(HallRepository hallRepository)
        {
            repository = hallRepository;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] HallDto hall)
        {
            try
            {
                await repository.Add(hall.CountSeats, hall.Name, hall.IsWorking);
                return Ok("Зал успешно создан");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при создании зала: {ex.Message}");
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var halls = await repository.GetAll();

                var dtoList = new List<HallDto>();

                foreach (var item in halls)
                {
                    var dto = new HallDto
                    {
                        Name = item.Name,
                        IsWorking = item.IsWorking,
                        CountSeats = item.CountSeats,
                    };

                    dtoList.Add(dto);
                }

                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " + ex.Message);
            }

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var hall = await repository.GetById(id);

                if (hall == null)
                {
                    throw new Exception("Зала с таким айди не существует");
                }

                var dto = new HallDto
                {
                    Name = hall.Name,
                    IsWorking = hall.IsWorking,
                    CountSeats = hall.CountSeats,
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " + ex.Message);
            }
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(int id) {
            try
            {
                await repository.DeleteById(id);
                return Ok("Зал удален");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " +ex.Message);
            }
        }

        [HttpDelete("[action]/{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            try
            {
                await repository.DeleteByName(name);
                return Ok("Зал удален");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " + ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateById([FromBody]HallDto hall,int id)
        {
            try
            {
                await repository.UpdateById(id,
                    hall.CountSeats,
                    hall.Name,
                    hall.IsWorking);

                return Ok("Информация обновлена");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " + ex.Message);
            }
        }

        [HttpPut("[action]/{name}")]
        public async Task<IActionResult> UpdateByName([FromBody] HallDto hall, string name)
        {
            try
            {
                await repository.UpdateByName(name,
                    hall.CountSeats,
                    hall.Name,
                    hall.IsWorking);

                return Ok("Информация обновлена");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка: " + ex.Message);
            }
        }
    }
    public class HallDto
    {
        public int CountSeats { get; set; }
        public string Name { get; set; }
        public bool IsWorking { get; set; }
    }
}
