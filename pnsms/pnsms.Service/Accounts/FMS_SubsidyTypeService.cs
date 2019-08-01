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

    public interface IFMS_SubsidyTypeService
    {
        FMS_SubsidyType Find(params object[] keyValues);
        void Insert(FMS_SubsidyType entity);
        void Update(FMS_SubsidyType entity);
        void Delete(object id);
        void Delete(FMS_SubsidyType entity);
        IEnumerable<FMS_SubsidyType> GetAllFMS_SubsidyType();
        IEnumerable<FMS_SubsidyType> GetActiveFMS_SubsidyType(bool IsActive);
        FMS_SubsidyType GetFMS_SubsidyTypeById(int Id);
        List<KeyValuePair<int, string>> GetKvpSubsidyType();
    }
    public class FMS_SubsidyTypeService : Service<FMS_SubsidyType>, IFMS_SubsidyTypeService
    {
        private readonly IRepositoryAsync<FMS_SubsidyType> _repository;

        public FMS_SubsidyTypeService(

              IRepositoryAsync<FMS_SubsidyType> repository

            )
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<FMS_SubsidyType> GetAllFMS_SubsidyType()
        {
            return _repository.Query().Select();
        }
        public IEnumerable<FMS_SubsidyType> GetActiveFMS_SubsidyType(bool IsActive)
        {
            return _repository
                   .Query(x => x.IsActive == IsActive)
                   .Select();
        }

        public FMS_SubsidyType GetFMS_SubsidyTypeById(int Id)
        {
            return _repository
                .Query(x => x.SubsidyTypeId == Id)
                .Select().FirstOrDefault();
        }
        public List<KeyValuePair<int, string>> GetKvpSubsidyType()
        {
            var kpvsubsidyType = new List<KeyValuePair<int, string>>();

            _repository.Query().Select().ToList().ForEach(delegate (FMS_SubsidyType item)
            {

                kpvsubsidyType.Add(new KeyValuePair<int, string>(item.SubsidyTypeId, item.SubsidyTypeName));
            });

            return kpvsubsidyType;


        }
    }
}
