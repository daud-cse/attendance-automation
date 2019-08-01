using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IFeesGenerateService : IService<FeesGenerate>
    {
        IEnumerable<FeesGenerate> GetAll(int instituteId, int monthId, int year, bool? isActive);
    }
    public class FeesGenerateService : Service<FeesGenerate>, IFeesGenerateService
    {
        private readonly IRepositoryAsync<FeesGenerate> _repository;

        public FeesGenerateService(IRepositoryAsync<FeesGenerate> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeesGenerate> GetAll(int instituteId, int monthId, int year, bool? isActive)
        {
            if (isActive != null)
            {
                return _repository.Query(c => c.InstituteId == instituteId 
                    && c.ForTheMonth == monthId 
                    && c.ForTheYear== year
                    && c.IsActive == isActive)
                    .Select();
            }
            return  _repository.Query(c => c.InstituteId == instituteId 
                    && c.ForTheMonth == monthId 
                    && c.ForTheYear== year)
                    .Select();
        }

    }
}
