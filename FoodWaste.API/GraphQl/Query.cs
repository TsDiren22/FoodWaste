using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using GraphQL.Types;
using System.Collections.Generic;

namespace FoodWaste.API.GraphQl
{
    public class Query
    {
        private readonly IRepository<Package> _packageRepository;
        public Query(IRepository<Package> packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public IEnumerable<Package> GetPackages()
        {
            return _packageRepository.GetAll();
        }

        public  Package GetPackageById(int id)
        {
            return _packageRepository.GetById(id);
        }
    }
}