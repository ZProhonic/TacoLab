using TacoLab.Models;

namespace TacoLab.DAL
{
    public class DrinkRepository
    {
        private readonly FastFoodTacoDbContext _context;

        public DrinkRepository(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        public List<Drink> GetAllDrinks()
        {
            return _context.Drinks.ToList();
        }

        public Drink GetDrinkById(int id)
        {
            Drink drink = _context.Drinks.FirstOrDefault(t => t.Id == id);
            return drink;
        }

        public void AddDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            _context.SaveChanges();
        }

        public void UpdateDrink(Drink drink)
        {
            _context.Drinks.Update(drink);
            _context.SaveChanges();
        }

        public void DeleteDrink(int id)
        {
            Drink drink = _context.Drinks.FirstOrDefault(x => x.Id == id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
                _context.SaveChanges();
            }
        }
    }
}
