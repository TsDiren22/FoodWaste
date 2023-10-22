using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.Infrastructure.Repositories
{
    public class PackageRepository : IRepository<Package>
    {
        private readonly WebAppContext _context;

        public PackageRepository(WebAppContext context)
        {
            _context = context;
        }

        public void Add(Package entity)
        {
            _context.Packages.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Package entity)
        {
            _context.Packages.Remove(entity);
            _context.SaveChanges();
        }
        
        public void Update(Package entity)
        {
            Package foundPackage = GetById(entity.Id);
            if (foundPackage != entity)
            {
                // If new object ref was submitted, change values, otherwise only save changes
                _context.Entry(foundPackage).CurrentValues.SetValues(entity);
                foundPackage.Products.Clear();
                foundPackage.Products.AddRange(entity.Products);
            }
            _context.SaveChanges();
        }

        public Package? FindByCondition(Expression<Func<Package, bool>> predicate)
        {
            return _context.Packages
                .Include(x => x.Cafeteria)
                .Include(x => x.Products)
                .Include(x => x.Student)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages
                .Include(x => x.Cafeteria)
                .Include(x => x.Products)
                .Include(x => x.Student)
                .AsEnumerable();
        }
        
        public Package? GetById(int id)
        {
            return _context.Packages
                .Include(x => x.Cafeteria)
                .Include(x => x.Products)
                .Include(x => x.Student)
                .FirstOrDefault(x => x.Id == id);
        }

    }
}
