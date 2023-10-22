using FoodWaste.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.DomainServices.IRepositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<Package> FindByConditionAsync(Expression<Func<Package, bool>> predicate);
        void Add(Package entity);
        void Update(Package entity);
        void Delete(Package entity);
        Task<bool> SaveChangesAsync();
    }
}
