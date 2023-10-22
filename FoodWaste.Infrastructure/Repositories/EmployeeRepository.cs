using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Infrastructure.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly WebAppContext _context;

        public EmployeeRepository(WebAppContext context)
        {
            _context = context;
        }

        public void Add(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Employee entity)
        {
            _context.Employees.Update(entity);
            _context.SaveChanges();
        }

        public Employee? FindByCondition(Expression<Func<Employee, bool>> predicate)
        {
            return _context.Employees
                .Include(x => x.Cafeteria)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees
                .Include(x => x.Cafeteria)
                .AsEnumerable();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .Include(x => x.Cafeteria)
                .FirstOrDefault(x => x.Id == id);
        }

    }
}
