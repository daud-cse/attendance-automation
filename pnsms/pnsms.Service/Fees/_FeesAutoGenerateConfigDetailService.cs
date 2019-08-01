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
    public interface _IFeesAutoGenerateConfigDetailService : IService<FeesAutoGenerateConfigDetail>
    {
        IEnumerable<FeesAutoGenerateConfigDetail> GetByConfId(int confId);
    }
    public class _FeesAutoGenerateConfigDetailService : Service<FeesAutoGenerateConfigDetail>, _IFeesAutoGenerateConfigDetailService
    {
        private readonly IRepositoryAsync<FeesAutoGenerateConfigDetail> _repository;

        public _FeesAutoGenerateConfigDetailService(IRepositoryAsync<FeesAutoGenerateConfigDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeesAutoGenerateConfigDetail> GetByConfId(int confId)
        {
            return _repository.Query(x => x.FeesAutoGenerateConfigId == confId).Select();
        }

    }
}
