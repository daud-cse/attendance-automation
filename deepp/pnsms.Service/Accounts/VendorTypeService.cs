using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Accounts
{
    public interface IVendorTypeService
    {
        VendorType Find(params object[] keyValues);
        void Insert(VendorType entity);
        void Update(VendorType entity);
        void Delete(object id);
        void Delete(VendorType entity);
        IEnumerable<VendorType> GetAllVendorType();
        IEnumerable<VendorType> GetActiveVendorType(bool IsActive);
        VendorType GetVendorTypeById(int Id);

    }
    public class VendorTypeService : Service<VendorType>, IVendorTypeService
    {
        private readonly IRepositoryAsync<VendorType> _repository;

        public VendorTypeService(

              IRepositoryAsync<VendorType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<VendorType> GetAllVendorType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<VendorType> GetActiveVendorType(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public VendorType GetVendorTypeById(int Id)
        {
            return _repository
                .Query(x => x.VendorTypeId == Id)
                .Select().FirstOrDefault();
        }

    }
}
