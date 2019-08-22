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
    public interface IFMS_TransactionTypeService
    {
        FMS_TransactionType Find(params object[] keyValues);
        void Insert(FMS_TransactionType entity);
        void Update(FMS_TransactionType entity);
        void Delete(object id);
        void Delete(FMS_TransactionType entity);
        IEnumerable<FMS_TransactionType> GetAllFMS_TransactionType();
        IEnumerable<FMS_TransactionType> GetAllFMS_TransactionType(int InstituteId);
        IEnumerable<FMS_TransactionType> GetActiveFMS_TransactionType(int InstituteId, bool IsActive);
        FMS_TransactionType GetFMS_TransactionTypeById(int Id, int InstituteId);

    }
    public class FMS_TransactionTypeService: Service<FMS_TransactionType>, IFMS_TransactionTypeService
    {
        private readonly IRepositoryAsync<FMS_TransactionType> _repository;

        public FMS_TransactionTypeService(

              IRepositoryAsync<FMS_TransactionType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_TransactionType> GetAllFMS_TransactionType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_TransactionType> GetAllFMS_TransactionType(int InstituteId)
        {
            return _repository.Query(x => x.InstituteId == InstituteId).Select();
        }
        public IEnumerable<FMS_TransactionType> GetActiveFMS_TransactionType(int InstituteId, bool IsActive)
        {
            return _repository
                   .Query(x => x.InstituteId == InstituteId)
                   .Select();
        }

        public FMS_TransactionType GetFMS_TransactionTypeById(int Id, int InstituteId)
        {
            return _repository
                .Query(x => x.TransactionTypeId == Id && x.InstituteId == InstituteId)
                .Select().FirstOrDefault();
        }

    }
}
