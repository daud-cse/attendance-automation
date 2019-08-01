using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{


        public interface IPackageService : IService<Package>
        {
            IEnumerable<Package> GetPackages();
            IEnumerable<Package> GetPackages(bool isActive);
            IEnumerable<Package> GetActivePackage();
            Package GetPackageById(int id);

        }
        public class PackageService : Service<Package>, IPackageService
        {


            private readonly IRepositoryAsync<Package> _repository;


            public PackageService(IRepositoryAsync<Package> repository)
                : base(repository)
            {
                _repository = repository;
            }


            public IEnumerable<Package> GetPackages()
            {

                return _repository.Query().Select();
            }

            public IEnumerable<Package> GetPackages(bool isActive)
            {
                if (isActive)
                {
                    return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
                }

                return _repository.Query().Select();
            }

            public IEnumerable<Package> GetActivePackage()
            {
                return _repository.Query().Select().Where(d => d.IsActive == true);
            }
            public Package GetPackageById(int id)
            {
                return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
            }
        }
}
