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
    public interface IGoverningbodyService : IService<Governingbody>
    {
        IEnumerable<Governingbody> GetGoverningbodyByIinstituteId(int instituteId);
    }
    public class GoverningbodyService : Service<Governingbody>, IGoverningbodyService
    {
        private readonly IRepositoryAsync<Governingbody> _repository;
        public GoverningbodyService(IRepositoryAsync<Governingbody> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Governingbody> GetGoverningbodyByIinstituteId(int instituteId)
        {
            return _repository.Query(x => x.UserInfo.InstituteId == instituteId)
                .Include(x => x.UserInfo)
                .Select();
        }
    }
}
