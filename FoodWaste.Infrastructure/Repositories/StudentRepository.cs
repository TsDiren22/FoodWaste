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
    public class StudentRepository : IRepository<Student>
    {
        private readonly WebAppContext _context;

        public StudentRepository(WebAppContext context)
        {
            _context = context;
        }

        public void Add(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
            _context.SaveChanges();
        }

        public Student? FindByCondition(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.FirstOrDefault(predicate);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.AsEnumerable();
        }

        public Student? GetById(int id)
        {
            return  _context.Students.FirstOrDefault(x => x.Id == id);
        }

    }
}
