using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Fees
{

    public interface _IFeesAutoGenerateConfigService : IService<FeesAutoGenerateConfig>
    {
        IEnumerable<FeesAutoGenerateConfig> GetAll(int instituteId, bool? isActive);
        IEnumerable<FeesAutoGenerateConfig> GetAllByType(int instituteId, int configTypeId);
        FeesAutoGenerateConfig GetById(int id);
        IEnumerable<FeesAutoGenerateConfig> GetAll(int instituteId, List<int> ids);
    }
    public class _FeesAutoGenerateConfigService : Service<FeesAutoGenerateConfig>, _IFeesAutoGenerateConfigService
    {
        private readonly IRepositoryAsync<FeesAutoGenerateConfig> _repository;

        public _FeesAutoGenerateConfigService(IRepositoryAsync<FeesAutoGenerateConfig> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeesAutoGenerateConfig> GetAll(int instituteId, bool? isActive)
        {

            if (isActive != null)
            {

                return _repository.Query(c => c.IsActive == isActive && c.InstituteId == instituteId).Select();
            }
            else
            {

                return _repository.Query(c => c.InstituteId == instituteId).Select();
            }

        }

        public IEnumerable<FeesAutoGenerateConfig> GetAllByType(int instituteId, int configTypeId)
        {

            return _repository.Query(c => c.FeesAutoGenerateConfigTypeId == configTypeId && c.InstituteId == instituteId).Select();
        }

        public FeesAutoGenerateConfig GetById(int id)
        {
            return _repository.Query(c => c.Id == id).Select().FirstOrDefault();

        }

        public IEnumerable<FeesAutoGenerateConfig> GetAll(int instituteId, List<int> ids)
        {
            return _repository.Query(c => c.InstituteId == instituteId && ids.Contains(c.Id)).Select();
        }

    }
}
