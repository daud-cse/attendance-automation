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
    public interface IFeesHeadService : IService<FeesHead>
    {
        IEnumerable<FeesHead> GetFeesHeads(int instituteId, bool? isActive);
    }
    public class FeesHeadService : Service<FeesHead>, IFeesHeadService
    {
        private readonly IRepositoryAsync<FeesHead> _repository;

        public FeesHeadService(IRepositoryAsync<FeesHead> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeesHead> GetFeesHeads(int instituteId, bool? isActive)
        {
            if (isActive != null)
            {
                return _repository.Query(c => c.InstituteId == instituteId && c.IsActive==isActive).Select();
            }
            return _repository.Query(c => c.InstituteId == instituteId).Select();
        }
    }
}
