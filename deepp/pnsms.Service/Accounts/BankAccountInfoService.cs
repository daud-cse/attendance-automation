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
    public interface IBankAccountInfoService
    {
        BankAccountInfo Find(params object[] keyValues);
        void Insert(BankAccountInfo entity);
        void Update(BankAccountInfo entity);
        void Delete(object id);
        void Delete(BankAccountInfo entity);
        IEnumerable<BankAccountInfo> GetAllBankAccountInfo(int InstituteId);
        IEnumerable<BankAccountInfo> GetActiveBankAccountInfo(bool IsActive);
        BankAccountInfo GetBankAccountInfoById(int Id);
        BankAccountInfo GetBankAccountInfoByAutoId(string BankAccountAutoId);
        BankAccountInfo newBankAccountInfo(int InstituteId);
        void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, BankAccountInfo bankAccountInfo);
        void Update(IUnitOfWorkAsync _UnitOfWorkAsync, BankAccountInfo bankAccountInfo);
    }
    public class BankAccountInfoService : Service<BankAccountInfo>, IBankAccountInfoService
    {
        private readonly IRepositoryAsync<BankAccountInfo> _repository;
        private readonly IBankAccountTypeService _iBankAccountTypeService;
        public BankAccountInfoService(

              IRepositoryAsync<BankAccountInfo> repository,
            IBankAccountTypeService iBankAccountTypeService

            )
            : base(repository)
        {
            _repository = repository;
            _iBankAccountTypeService = iBankAccountTypeService;

        }

        public BankAccountInfo newBankAccountInfo(int InstituteId)
        {

            BankAccountInfo objBankAccountInfo = new BankAccountInfo();


            var lstBankAccountType = new List<KeyValuePair<int, string>>();
            _iBankAccountTypeService.GetActiveBankAccountType(InstituteId, true).ToList().ForEach(delegate(BankAccountType item)
            {
                lstBankAccountType.Add(new KeyValuePair<int, string>(item.BankAccountTypeId, item.BankAccountTypeName));
            });
            objBankAccountInfo.kvpBankAccountType = lstBankAccountType;
            return objBankAccountInfo;
        }
        public IEnumerable<BankAccountInfo> GetAllBankAccountInfo(int InstituteId)
        {
            return _repository.Query(x => x.InstituteId == InstituteId)
                .Include(x => x.BankAccountType)
                .Select();
        }
        public BankAccountInfo GetBankAccountInfoByAutoId(string BankAccountAutoId)
        {
            return _repository
                  .Query(x => x.BankAccountAutoId == BankAccountAutoId)
                  .Select().FirstOrDefault();
        }
        public IEnumerable<BankAccountInfo> GetActiveBankAccountInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public BankAccountInfo GetBankAccountInfoById(int Id)
        {
            return _repository
                .Query(x => x.ID == Id)
                .Select().FirstOrDefault();
        }

        public void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, BankAccountInfo bankAccountInfo)
        {
            bankAccountInfo.BankAccountAutoId = "sddfdf";

            try {
                _repository.Insert(bankAccountInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch (Exception e)
            {

            }
           

        }

        public void Update(IUnitOfWorkAsync _UnitOfWorkAsync, BankAccountInfo bankAccountInfo)
        {
            try
            {
               
                bankAccountInfo.SetDate = DateTime.Now;
                _repository.Update(bankAccountInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch(Exception e)
            {

            }
          
        }

     
    }
}
