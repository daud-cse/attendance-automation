using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IFeesTypeService : IService<FeesType>
    {
        IEnumerable<FeesType> GetFeesTypes(int instituteId, bool? isActive);
        List<KeyValuePair<int, string>> GetKVP(int instituteId);
    }
    public class FeesTypeService : Service<FeesType>, IFeesTypeService
    {
        private readonly IRepositoryAsync<FeesType> _repository;

        public FeesTypeService(IRepositoryAsync<FeesType> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeesType> GetFeesTypes(int instituteId, bool? isActive)
        {
            if (isActive != null)
            {
                return _repository.Query(c => c.InstituteId == instituteId && c.IsActive == isActive).Select();
            }
            return _repository.Query(c => c.InstituteId == instituteId).Select();
        }
        public List<KeyValuePair<int, string>> GetKVP(int instituteId)
        {
            var data = _repository.Query(c => c.IsActive && c.InstituteId == instituteId).Select().ToList();
            var kvpFeesType = new List<KeyValuePair<int, string>>();
            data.ForEach(c => kvpFeesType.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return kvpFeesType;
        }
    }
}
