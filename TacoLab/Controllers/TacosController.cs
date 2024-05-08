using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoLab.DAL;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        private readonly TacoRepository _tacoRepository;

        public TacosController(TacoRepository tacos)
        {
            _tacoRepository = tacos;
        }

        [HttpGet]
        public IActionResult GetAllTacos(bool? category = null) 
        { 
            List<Taco> tacos = _tacoRepository.GetAllTacos();
            if (category != null)
            {
                tacos = tacos.Where(tacos => tacos.SoftShell == category).ToList();
            }
            return Ok(tacos);

        }

        [HttpGet("{id}")]
        public IActionResult GetTacoById(int id)
        {
            Taco taco = _tacoRepository.GetTacoById(id);
            if (taco != null)
            {
                return Ok(taco);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult AddTaco([FromBody] TacoCreationDto tacoDto)
        {
            var taco = new Taco
            {
                Name = tacoDto.Name,
                Cost = tacoDto.Cost,
                SoftShell = tacoDto.SoftShell,
                Chips = tacoDto.Chips,
            };

            _tacoRepository.AddTaco(taco);
            return CreatedAtAction(nameof(GetTacoById), new {id = taco.Id }, taco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTacoById(int id)
        {
            Taco taco = _tacoRepository.GetTacoById(id);
            if (taco == null)
            {
                return NotFound();
            }
            _tacoRepository.DeleteTaco(id);
            return NoContent();
        }
    }
}
