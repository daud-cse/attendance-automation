using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
  
    public interface IAddressTypeService : IService<AddressType>
    {
        IEnumerable<AddressType> GetAddressTypes(int instituteId);
        IEnumerable<AddressType> GetAddressTypes(bool isActive);
        IEnumerable<AddressType> GetActiveAddressType();
        AddressType GetAddressTypeById(int id);
        IEnumerable<AddressType> GetAddressTypeByInstituteId(int instituteId);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType);

    }
    public class AddressTypeService : Service<AddressType>, IAddressTypeService
    {


        private readonly IRepositoryAsync<AddressType> _repository;


        public AddressTypeService(IRepositoryAsync<AddressType> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<AddressType> GetAddressTypes(int instituteId)
        {

            return _repository.Query(a=>a.InstituteId==instituteId).Select();
        }

        public IEnumerable<AddressType> GetAddressTypes(bool isActive)
        {
            if (isActive)
            {
                return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _repository.Query().Select();
        }

        public IEnumerable<AddressType> GetActiveAddressType()
        {
            return _repository.Query().Select().Where(d => d.IsActive == true);
        }
        public AddressType GetAddressTypeById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AddressType> GetAddressTypeByInstituteId(int instituteId)
        {
            return _repository.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType)
        {
            addressType.LastUpdateTime = DateTime.Now;
            _repository.Insert(addressType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType)
        {
            addressType.LastUpdateTime = DateTime.Now;
            _repository.Update(addressType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
