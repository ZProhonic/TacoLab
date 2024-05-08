using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TacoLab.DAL;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly DrinkRepository _drinkRepository;

        public DrinksController(DrinkRepository drinks)
        {
            _drinkRepository = drinks;
        }

        [HttpGet]
        public IActionResult GetAllDrinks(string? SortByCost = null)
        {
            List<Drink> drinks = _drinkRepository.GetAllDrinks();
            if (SortByCost == "ascending")
            {
                drinks = drinks.OrderBy(d => d.Cost).ToList();
            }
            else if (SortByCost == "descending")
            {
                drinks = drinks.OrderByDescending(d => d.Cost).ToList();
            }

            return Ok(drinks);

        }

        [HttpGet("{id}")]
        public IActionResult GetDrinkById(int id)
        {
            Drink drink = _drinkRepository.GetDrinkById(id);
            if (drink != null)
            {
                return Ok(drink);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult AddDrink([FromBody] DrinkCreationDto drinkDto)
        {
            var drink = new Drink
            {
                Name = drinkDto.Name,
                Cost = drinkDto.Cost, 
            };

            _drinkRepository.AddDrink(drink);
            return CreatedAtAction(nameof(GetDrinkById), new { id = drink.Id }, drink);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrink(int id, [FromBody] UpdateDrinkDto updateDrinkDto)
        {
            Drink drink = _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                return BadRequest();
            }
            drink.Name = updateDrinkDto.Name;
            drink.Cost = updateDrinkDto.Cost;

            _drinkRepository.UpdateDrink(drink);
            return NoContent();

        }


    }
}
