using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.UnitOfWork;

namespace pnsms.Service.Accounts
{
    public interface IVendorInfoService
    {
        VendorInfo Find(params object[] keyValues);
        void Insert(VendorInfo entity);
        void Update(VendorInfo entity);
        void Delete(object id);
        void Delete(VendorInfo entity);
        IEnumerable<VendorInfo> GetAllVendorInfo(int InstituteId);
        IEnumerable<VendorInfo> GetActiveVendorInfo(bool IsActive);
        VendorInfo GetVendorInfoById(int Id);
        VendorInfo newVendorInfo(int InstituteId);
        void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, VendorInfo vendorInfo);
        void Update(IUnitOfWorkAsync _UnitOfWorkAsync, VendorInfo vendorInfo);
    }
    public class VendorInfoService : Service<VendorInfo>, IVendorInfoService
    {
        private readonly IRepositoryAsync<VendorInfo> _repository;
        private readonly IVendorTypeService _iVendorTypeService;
        public VendorInfoService(

              IRepositoryAsync<VendorInfo> repository,
            IVendorTypeService iVendorTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iVendorTypeService = iVendorTypeService;

        }
        public IEnumerable<VendorInfo> GetAllVendorInfo(int InstituteId)
        {
            return _repository.Query(x => x.InstituteId == InstituteId)
                              .Include(x => x.VendorType)
                              .Select();
        }
        public VendorInfo newVendorInfo(int InstituteId)
        {

            VendorInfo objVendorInfo = new VendorInfo();


            var lstVendorType = new List<KeyValuePair<int, string>>();
            _iVendorTypeService.GetAllVendorType().ToList().ForEach(delegate (VendorType item)
            {
                lstVendorType.Add(new KeyValuePair<int, string>(item.VendorTypeId, item.VendorTypeName));
            });
            objVendorInfo.kvpVendorType = lstVendorType;
            return objVendorInfo;
        }
        public IEnumerable<VendorInfo> GetActiveVendorInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public VendorInfo GetVendorInfoById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id )
                .Select().FirstOrDefault();
        }

       

        public void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, VendorInfo vendorInfo)
        {
            vendorInfo.VendorAutoId = "sddfdf";
            vendorInfo.SetDate = DateTime.Now;
            vendorInfo.SetBy = "abc";

            try
            {
                _repository.Insert(vendorInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void Update(IUnitOfWorkAsync _UnitOfWorkAsync, VendorInfo vendorInfo)
        {
            try
            {

                vendorInfo.SetDate = DateTime.Now;
                _repository.Update(vendorInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

      
       

       

       
    }
}
