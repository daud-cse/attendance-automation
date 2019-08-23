using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
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


            private readonly IRepositoryAsync<Package> _redeeppitory;


            public PackageService(IRepositoryAsync<Package> redeeppitory)
                : base(redeeppitory)
            {
                _redeeppitory = redeeppitory;
            }


            public IEnumerable<Package> GetPackages()
            {

                return _redeeppitory.Query().Select();
            }

            public IEnumerable<Package> GetPackages(bool isActive)
            {
                if (isActive)
                {
                    return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
                }

                return _redeeppitory.Query().Select();
            }

            public IEnumerable<Package> GetActivePackage()
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
            }
            public Package GetPackageById(int id)
            {
                return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
            }
        }
}
