using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{
    public interface IGoverningbodyService : IService<Governingbody>
    {
        IEnumerable<Governingbody> GetGoverningbodyByIinstituteId(int instituteId);
    }
    public class GoverningbodyService : Service<Governingbody>, IGoverningbodyService
    {
        private readonly IRepositoryAsync<Governingbody> _redeeppitory;
        public GoverningbodyService(IRepositoryAsync<Governingbody> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

        public IEnumerable<Governingbody> GetGoverningbodyByIinstituteId(int instituteId)
        {
            return _redeeppitory.Query(x => x.UserInfo.InstituteId == instituteId)
                .Include(x => x.UserInfo)
                .Select();
        }
    }
}
