using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pet.Contracts;
using Pet.Repositories;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pet.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class HallController : ControllerBase
    {
        private readonly HallRepository repository;

        public HallController(HallRepository hallRepository)
        {
            repository = hallRepository;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] HallDto hall,
            CancellationToken cancellationToken)
        {
            await repository.Add(hall.CountSeats, hall.Name, hall.IsWorking, cancellationToken);
            return Ok("Зал успешно создан");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {

            var halls = await repository.GetAll(cancellationToken);

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

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {

            var hall = await repository.GetById(id, cancellationToken);

            if (hall == null)
            {
                throw new BadHttpRequestException("Зала с таким айди не существует");
            }

            var dto = new HallDto
            {
                Name = hall.Name,
                IsWorking = hall.IsWorking,
                CountSeats = hall.CountSeats,
            };

            return Ok(dto);

        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {

            await repository.DeleteById(id, cancellationToken);
            return Ok("Зал удален");

        }

        [HttpDelete("[action]/{name}")]
        public async Task<IActionResult> DeleteByName(string name, CancellationToken cancellationToken)
        {

            await repository.DeleteByName(name, cancellationToken);
            return Ok("Зал удален");


        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateById([FromBody] HallDto hall, int id,
            CancellationToken cancellationToken)
        {

            await repository.UpdateById(id,
                hall.CountSeats,
                hall.Name,
                hall.IsWorking,
                cancellationToken);

            return Ok("Информация обновлена");

        }

        [HttpPut("[action]/{name}")]
        public async Task<IActionResult> UpdateByName([FromBody] HallDto hall, string name,
            CancellationToken cancellationToken)
        {

            await repository.UpdateByName(name,
                hall.CountSeats,
                hall.Name,
                hall.IsWorking,
                cancellationToken);

            return Ok("Информация обновлена");
        }
    }
}



