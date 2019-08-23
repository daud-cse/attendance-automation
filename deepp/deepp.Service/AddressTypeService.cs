using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
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


        private readonly IRepositoryAsync<AddressType> _redeeppitory;


        public AddressTypeService(IRepositoryAsync<AddressType> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<AddressType> GetAddressTypes(int instituteId)
        {

            return _redeeppitory.Query(a=>a.InstituteId==instituteId).Select();
        }

        public IEnumerable<AddressType> GetAddressTypes(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<AddressType> GetActiveAddressType()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public AddressType GetAddressTypeById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AddressType> GetAddressTypeByInstituteId(int instituteId)
        {
            return _redeeppitory.Query(d => d.IsActive == true && d.InstituteId == instituteId).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType)
        {
            addressType.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(addressType);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, AddressType addressType)
        {
            addressType.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(addressType);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
