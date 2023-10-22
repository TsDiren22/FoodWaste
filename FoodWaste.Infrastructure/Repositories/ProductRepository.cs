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
    public class ProductRepository : IRepository<Product>
    {
        private readonly WebAppContext _context;

        public ProductRepository(WebAppContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
        }

        public Product? FindByCondition(Expression<Func<Product, bool>> predicate)
        {
            return _context.Products
                .Include(x => x.Packages)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(x => x.Packages)
                .AsEnumerable();
        }

        public Product? GetById(int id)
        {
            return _context.Products
                .Include(x => x.Packages)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
