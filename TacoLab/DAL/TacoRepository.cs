using Microsoft.AspNetCore.Mvc;
using TacoLab.Models;

namespace TacoLab.DAL
{
    public class TacoRepository
    {
        private readonly FastFoodTacoDbContext _context;

        public TacoRepository(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        public List<Taco> GetAllTacos()
        {
            return _context.Tacos.ToList();
        }

        public Taco GetTacoById(int id)
        {
            Taco taco = _context.Tacos.FirstOrDefault(t => t.Id == id);
            return taco;
        }

        public void AddTaco(Taco taco)
        {
            _context.Tacos.Add(taco);
            _context.SaveChanges();
        }

        public void DeleteTaco(int id)
        {
            Taco taco = _context.Tacos.FirstOrDefault(x => x.Id == id);
            if (taco != null)
            {
                _context.Tacos.Remove(taco);
                _context.SaveChanges();
            }
        }
    }
}
