using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{


    public interface ICoCurricularActivityService : IService<CoCurricularActivity>
    {

        IEnumerable<CoCurricularActivity> GetCoCurricularActivityByInstituteId(int instituteId, bool? isActive = null);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity);

    }
    public class CoCurricularActivityService : Service<CoCurricularActivity>, ICoCurricularActivityService
    {


        private readonly IRepositoryAsync<CoCurricularActivity> _redeeppitory;


        public CoCurricularActivityService(IRepositoryAsync<CoCurricularActivity> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }

 

        public IEnumerable<CoCurricularActivity> GetCoCurricularActivityByInstituteId(int instituteId, bool? isActive=null)
        {
            return isActive != null ? _redeeppitory.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : _redeeppitory.Query(d => d.InstituteId == instituteId).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.LastUpdateTime = DateTime.Now;
            _redeeppitory.Insert(coCurricularActivity);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.LastUpdateTime = DateTime.Now;
            _redeeppitory.Update(coCurricularActivity);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
