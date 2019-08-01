using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Accounts
{
    public interface IFMS_RelatedAccountService
    {
        FMS_RelatedAccount Find(params object[] keyValues);
        void Insert(FMS_RelatedAccount entity);
        void Update(FMS_RelatedAccount entity);
        void Delete(object id);
        void Delete(FMS_RelatedAccount entity);
        IEnumerable<FMS_RelatedAccount> GetAllFMS_RelatedAccountInfo(int InstituteId);
        IEnumerable<FMS_RelatedAccount> GetActiveFMS_RelatedAccountInfo(bool IsActive);
        FMS_RelatedAccount GetFMS_RelatedAccountInfoById(int Id);
      
        FMS_RelatedAccount newFMS_RelatedAccountInfo(int InstituteId);
        void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, FMS_RelatedAccount FMS_RelatedAccountInfo);
        void Update(IUnitOfWorkAsync _UnitOfWorkAsync, FMS_RelatedAccount FMS_RelatedAccountInfo);
    }
    public class FMS_RelatedAccountService : Service<FMS_RelatedAccount>, IFMS_RelatedAccountService
    {
        private readonly IRepositoryAsync<FMS_RelatedAccount> _repository;
        private readonly IFMS_TransactionTypeService _iIFMS_TransactionTypeService;
        public FMS_RelatedAccountService(

           IRepositoryAsync<FMS_RelatedAccount> repository,
         IFMS_TransactionTypeService iIFMS_TransactionTypeService

         )
            : base(repository)
        {
            _repository = repository;
            _iIFMS_TransactionTypeService = iIFMS_TransactionTypeService;

        }


        public FMS_RelatedAccount newFMS_RelatedAccountInfo(int InstituteId)
        {

            FMS_RelatedAccount objFMS_RelatedAccountInfo = new FMS_RelatedAccount();


            var lstFMS_RelatedAccountType = new List<KeyValuePair<int, string>>();
            _iIFMS_TransactionTypeService.GetActiveFMS_TransactionType(InstituteId, true).ToList().ForEach(delegate (FMS_TransactionType item)
            {
                lstFMS_RelatedAccountType.Add(new KeyValuePair<int, string>(item.TransactionTypeId, item.Name));
            });
            objFMS_RelatedAccountInfo.kvpFMS_RelatedAccountType = lstFMS_RelatedAccountType;
            return objFMS_RelatedAccountInfo;
        }
        public IEnumerable<FMS_RelatedAccount> GetAllFMS_RelatedAccountInfo(int InstituteId)
        {
            return _repository.Query(x => x.InstituteId == InstituteId)
                .Include(x => x.TransactionTypeId)
                .Select();
        }
       
        public IEnumerable<FMS_RelatedAccount> GetActiveFMS_RelatedAccountInfo(bool IsActive)
        {
            return _repository
                   .Query()
                   .Select();
        }

        public FMS_RelatedAccount GetFMS_RelatedAccountInfoById(int Id)
        {
            return _repository
                .Query(x => x.RelatedAccountId == Id)
                .Select().FirstOrDefault();
        }

        public void Insert(IUnitOfWorkAsync _UnitOfWorkAsync, FMS_RelatedAccount FMS_RelatedAccountInfo)
        {
           // bankAccountInfo.BankAccountAutoId = "sddfdf";

            try
            {
                _repository.Insert(FMS_RelatedAccountInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch (Exception e)
            {

            }


        }

        public void Update(IUnitOfWorkAsync _UnitOfWorkAsync, FMS_RelatedAccount FMS_RelatedAccountInfoInfo)
        {
            try
            {

               // FMS_RelatedAccountInfoInfo.SetDate = DateTime.Now;
                _repository.Update(FMS_RelatedAccountInfoInfo);
                _UnitOfWorkAsync.SaveChanges();
            }
            catch (Exception e)
            {

            }

        }
    }
}
