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


    public interface ICoCurricularActivityService : IService<CoCurricularActivity>
    {

        IEnumerable<CoCurricularActivity> GetCoCurricularActivityByInstituteId(int instituteId, bool? isActive = null);
        void Insert(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity);
        void Update(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity);

    }
    public class CoCurricularActivityService : Service<CoCurricularActivity>, ICoCurricularActivityService
    {


        private readonly IRepositoryAsync<CoCurricularActivity> _repository;


        public CoCurricularActivityService(IRepositoryAsync<CoCurricularActivity> repository)
            : base(repository)
        {
            _repository = repository;
        }

 

        public IEnumerable<CoCurricularActivity> GetCoCurricularActivityByInstituteId(int instituteId, bool? isActive=null)
        {
            return isActive != null ? _repository.Query(d => d.InstituteId.Equals(instituteId) && d.IsActive == isActive).Select() : _repository.Query(d => d.InstituteId == instituteId).Select();
        }
        public void Insert(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.LastUpdateTime = DateTime.Now;
            _repository.Insert(coCurricularActivity);
            unitOfWorkAsync.SaveChanges();

        }
        public void Update(IUnitOfWorkAsync unitOfWorkAsync, CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.LastUpdateTime = DateTime.Now;
            _repository.Update(coCurricularActivity);
            unitOfWorkAsync.SaveChanges();

        }
    }
}
