using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodWaste.DomainServices.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        T? FindByCondition(Expression<Func<T, bool>> predicate);
    }
}
