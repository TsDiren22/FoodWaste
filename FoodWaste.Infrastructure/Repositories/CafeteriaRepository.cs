using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Infrastructure.Repositories
{
    public class CafeteriaRepository : IRepository<Cafeteria>
    {
        private readonly WebAppContext _context;

        public CafeteriaRepository(WebAppContext context)
        {
            _context = context;
        }

        public void Add(Cafeteria entity)
        {
            _context.Cafeterias.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Cafeteria entity)
        {
            _context.Cafeterias.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Cafeteria entity)
        {
            _context.Cafeterias.Update(entity);
            _context.SaveChanges();
        }

        public Cafeteria? FindByCondition(Expression<Func<Cafeteria, bool>> predicate)
        {
            return _context.Cafeterias.FirstOrDefault(predicate);
        }

        public IEnumerable<Cafeteria> GetAll()
        {
            return _context.Cafeterias.AsEnumerable();
        }

        public Cafeteria? GetById(int id)
        {
            return _context.Cafeterias.FirstOrDefault(x => x.Id == id);
        }

    }
}
