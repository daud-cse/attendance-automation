using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace pnsms.Service
{
    public interface IAddressService
    {
        Address Find(params object[] keyValues);
        IQueryable<Address> SelectQuery(string query, params object[] parameters);
        void Insert(Address entity);
        void InsertRange(IEnumerable<Address> entities);
        void InsertOrUpdateGraph(Address entity);
        void InsertGraphRange(IEnumerable<Address> entities);
        void Update(Address entity);
        void Delete(object id);
        void Delete(Address entity);
        IQueryFluent<Address> Query();
        IQueryFluent<Address> Query(IQueryObject<Address> queryObject);
        IQueryFluent<Address> Query(Expression<Func<Address, bool>> query);
        Task<Address> FindAsync(params object[] keyValues);
        Task<Address> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<Address> Queryable();
        Address NewAddress(int instituteId);
        IEnumerable<Address> GetAddressesByUserId(int userId);
    }

    public class AddressService:Service<Address>, IAddressService
    {
        private readonly IRepositoryAsync<Address> _repository;
        private readonly IAddressTypeService _addressTypeService;
        private readonly IDistrictOrStateService _districtOrStateService;

        public AddressService(IRepositoryAsync<Address> repository,IAddressTypeService addressTypeService,IDistrictOrStateService districtOrStateService) : base(repository)
        {
            _repository = repository;
            _addressTypeService = addressTypeService;
            _districtOrStateService = districtOrStateService;
        }

        public Address NewAddress(int instituteId)
        {
            var address = new Address();
            address.IsActive = true;

            var lstAddressTypeListKv = new List<KeyValuePair<int, string>>();
            _addressTypeService.GetAddressTypeByInstituteId(instituteId).ToList().ForEach(item => lstAddressTypeListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));

            var lstDistrictListKv = new List<KeyValuePair<int, string>>();
            _districtOrStateService.GetActiveDistrictOrStateByinstituteId(instituteId).ToList().ForEach(item => lstDistrictListKv.Add(new KeyValuePair<int, string>(item.Id, item.Name)));



            address.AddressTypeList = lstAddressTypeListKv;
            address.DistrictList = lstDistrictListKv;
            return address;
        }

        public IEnumerable<Address> GetAddressesByUserId(int userId)
        {
            var addresses =_repository.Query(a => a.RefPrimaryKey.Equals(userId)).Select();
            return addresses;
        }
    }
}
